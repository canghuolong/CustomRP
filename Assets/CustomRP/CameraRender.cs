using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace CustomRP
{
    public partial class CameraRender
    {
        private ScriptableRenderContext _context;
        private Camera _camera;

        private const string bufferName = "Render Camera";
        private CommandBuffer _buffer = new CommandBuffer{name =  bufferName};
        private CullingResults _cullingResults;

        private static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");
        
        public void Render(ScriptableRenderContext context,Camera camera)
        {
            _context = context;
            _camera = camera;
            PrepareBuffer();
            PrepareForSceneWindow();
            if (!Cull())
            {
                return;
            }
            
            Setup();
            DrawVisibleGeometry();
            DrawUnsupportedShaders();
            DrawGizmos();
            Submit();
        }

        private void DrawVisibleGeometry()
        {
            var sortingSettings = new SortingSettings(_camera);
            sortingSettings.criteria = SortingCriteria.CommonOpaque;
            var drawingSetting = new DrawingSettings();
            drawingSetting.SetShaderPassName(0,unlitShaderTagId);
            drawingSetting.sortingSettings = sortingSettings;
            var filteringSetting = new FilteringSettings(RenderQueueRange.opaque);
            _context.DrawRenderers(_cullingResults,ref drawingSetting,ref filteringSetting);
            
            _context.DrawSkybox(_camera);

            sortingSettings.criteria = SortingCriteria.CommonTransparent;
            drawingSetting.sortingSettings = sortingSettings;
            filteringSetting.renderQueueRange = RenderQueueRange.transparent;
            _context.DrawRenderers(_cullingResults,ref drawingSetting,ref filteringSetting);

        }

        partial void DrawUnsupportedShaders();

        private void Setup()
        {
            _context.SetupCameraProperties(_camera);
            var flags = _camera.clearFlags;
            _buffer.ClearRenderTarget(flags <= CameraClearFlags.Depth,flags == CameraClearFlags.Color,flags == CameraClearFlags.Color ? _camera.backgroundColor.linear : Color.clear);
            _buffer.BeginSample(SampleName);
            ExecuteBuffer();
        }

        private void Submit()
        {
            _buffer.EndSample(SampleName);
            ExecuteBuffer();
            _context.Submit();
        }

        private void ExecuteBuffer()
        {
            _context.ExecuteCommandBuffer(_buffer);
            _buffer.Clear();
        }

        private bool Cull()
        {
            if(_camera.TryGetCullingParameters( out var p))
            {
                _cullingResults = _context.Cull(ref p);
                return true;
            }

            return false;
        }

        partial void DrawGizmos();
        partial void PrepareForSceneWindow();
        partial void PrepareBuffer();
    }
}

