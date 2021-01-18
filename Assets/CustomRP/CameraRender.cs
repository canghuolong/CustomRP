using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace CustomRP
{
    public class CameraRender
    {
        private ScriptableRenderContext _context;
        private Camera _camera;

        public void Render(ScriptableRenderContext context,Camera camera)
        {
            _context = context;
            _camera = camera;
        
            Setup();
            DrawVisibleGeometry();
            Submit();
        }

        private void DrawVisibleGeometry()
        {
            _context.DrawSkybox(_camera);
        }

        private void Setup()
        {
            _context.SetupCameraProperties(_camera);
        }

        private void Submit()
        {
            _context.Submit();
        }
    }
}

