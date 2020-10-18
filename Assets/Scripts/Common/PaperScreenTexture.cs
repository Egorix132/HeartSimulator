using UnityEngine;

class PaperScreenTexture : MonoBehaviour
{
    [SerializeField] private Camera renderCamera;
    private RenderTexture screenTexture;  

    private void Awake()
    {
        screenTexture = new RenderTexture(Screen.width, Screen.height, 24);
        renderCamera.targetTexture = screenTexture;
        GetComponent<MeshRenderer>().material.mainTexture = screenTexture;
    }
}

