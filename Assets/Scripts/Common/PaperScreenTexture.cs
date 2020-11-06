using UnityEngine;

class PaperScreenTexture : MonoBehaviour
{
    private void Awake()
    {
        var screenTexture = new RenderTexture((Screen.width / 2) * 2, (Screen.height) / 2 * 2, 24);
        GetComponent<Camera>().targetTexture = screenTexture;
        PaperScreenMesh.Instance
                       .gameObject
                       .GetComponent<MeshRenderer>()
                       .material
                       .mainTexture = screenTexture;
    }
}

