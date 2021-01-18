using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace CustomRP
{
    public class CustomRP : RenderPipeline
    {
        private readonly CameraRender _cameraRender = new CameraRender();

        protected override void Render(ScriptableRenderContext context, Camera[] cameras)
        {
            foreach (var cam in cameras)
            {
                _cameraRender.Render(context, cam);
            }
        }
    }
}
