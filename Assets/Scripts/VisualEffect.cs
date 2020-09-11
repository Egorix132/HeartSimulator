using UnityEngine;

[RequireComponent(typeof(Camera))]
public class VisualEffect : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float fadeTime = 1.5f;
    void Start()
    {
        if (null == material ||
           null == material.shader || !material.shader.isSupported)
        {
            enabled = false;
            return;
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        float transparency = material.GetFloat("_Transparency");
        if (transparency > 0)
            material.SetFloat("_Transparency", transparency - Time.deltaTime / fadeTime);
        if (transparency < 0)
            material.SetFloat("_Transparency", 0);
        Graphics.Blit(source, destination, material);
    }

    public void SetEffect(Color color, float transparency, float fadeTime = 1.5f)
    {
        this.fadeTime = fadeTime;
        material.SetColor("_Color", color);
        material.SetFloat("_Transparency", transparency);
    }
}
