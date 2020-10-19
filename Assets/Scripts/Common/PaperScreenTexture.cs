using UnityEngine;

class PaperScreenTexture : MonoBehaviour
{
    private void Awake()
    {
        var screenTexture = new RenderTexture(Screen.width, Screen.height, 24);
        GetComponent<Camera>().targetTexture = screenTexture;
        PaperScreenMesh.Instance
                       .gameObject
                       .GetComponent<MeshRenderer>()
                       .material
                       .mainTexture = screenTexture;
    }
}

