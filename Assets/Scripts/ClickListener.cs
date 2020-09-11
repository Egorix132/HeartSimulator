using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ClickListener : MonoBehaviour
{
    [SerializeField] private CustomRenderTexture screenHeightMap;
    [SerializeField] private Material screenHeightMaterial;
    private new Camera camera;
    private Collider2D heartCollider;

    void Start()
    {
        screenHeightMap.Initialize();
        camera = Camera.main;
        heartCollider = GetComponent<Collider2D>();
        Debug.Log(camera);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider == heartCollider)
            {
                Vector3 screenPoint = camera.WorldToViewportPoint(hit.point);
                Debug.Log(screenPoint);

                screenHeightMaterial.SetVector("_DrawPosition", screenPoint);
            }
        }
    }
}
