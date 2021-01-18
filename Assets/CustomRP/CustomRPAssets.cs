using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace CustomRP
{
    [CreateAssetMenu(menuName = "Rendering/CustomRP", fileName = "CustomRP")]
    public class CustomRPAssets : RenderPipelineAsset
    {
        [SerializeField] private bool enableInstancing;
        [SerializeField] private bool enableBatch;

        protected override RenderPipeline CreatePipeline()
        {
            return new CustomRP();
        }
    }
}
