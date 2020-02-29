using UnityEngine;

[ExecuteInEditMode]
public class ToonPostRender : MonoBehaviour
{
    [SerializeField] Material effectMaterial;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, effectMaterial);
    }
}
