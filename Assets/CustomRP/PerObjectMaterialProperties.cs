using UnityEngine;

public class PerObjectMaterialProperties : MonoBehaviour
{
    private static int baseColorId = Shader.PropertyToID("_BaseColor");
    [SerializeField] private Color baseColor = Color.white;
    private static MaterialPropertyBlock block;

    private void Awake()
    {
        baseColor = Random.ColorHSV();
        OnValidate();
    }

    private void OnValidate()
    {
        if (block == null)
        {
            block = new MaterialPropertyBlock();
        }
        block.SetColor(baseColorId,baseColor);
        GetComponent<Renderer>().SetPropertyBlock(block);
    }
}
