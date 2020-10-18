using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PaperScreenMesh : MonoBehaviour
{
    [SerializeField] private new Camera camera;

    [SerializeField] private int height;
    [SerializeField] private int width;
    [SerializeField] private int density;
    [SerializeField] private float sharpness;

    private Mesh mesh;
    Vector3[] vertices;
    Vector2[] uv;
    int[] triangles;
    float seed;

    private void Awake()
    {
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        vertices = new Vector3[(1 + height * density) * (1 + width * density)];
        uv = new Vector2[vertices.Length];
        triangles = new int[height * width * (int)Math.Pow(density, 2) * 6];
        seed = UnityEngine.Random.Range(-1000, 1000);

        GenerateVertices();
        GenerateUV();
        GenerateTriangles();

        mesh = new Mesh
        {
            name = "Screen",
            vertices = vertices,
            uv = uv,
            triangles = triangles
        };
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private void GenerateVertices()
    {
        for (int i = 0; i < (1 + height * density); i++)
        {
            for (int j = 0; j < (1 + width * density); j++)
            {
                vertices[i * (1 + width * density) + j] = new Vector3(
                    j / (float)density - width / 2,
                    i / (float)density - height / 2,
                    UnityEngine.Random.Range(-0.5f, 0.5f) * sharpness
                    /*Mathf.PerlinNoise((j / (float)density + seed) * 3f, (i / (float)density + seed) * 3f) * 0.1f*/);
            }
        }
    }

    private void GenerateUV()
    {
        for (int i = 0; i < (1 + height * density); i++)
        {
            for (int j = 0; j < (1 + width * density); j++)
            {
                Vector3 screenPoint = camera.WorldToViewportPoint(transform.TransformPoint(vertices[i * (1 + width * density) + j]));
                uv[i * (1 + width * density) + j] = new Vector3(screenPoint.x, screenPoint.y, 0);
            }
        }
    }

    private void GenerateTriangles()
    {
        for (int i = 0; i < height * density; i++)
        {
            for (int j = 0; j < width * density; j++)
            {
                triangles[(i * width * density + j) * 6] = i * (1 + width * density) + j;
                triangles[(i * width * density + j) * 6 + 1] = (i + 1) * (1 + width * density) + j;
                triangles[(i * width * density + j) * 6 + 2] = i * (1 + width * density) + j + 1;
                triangles[(i * width * density + j) * 6 + 3] = i * (1 + width * density) + j + 1;
                triangles[(i * width * density + j) * 6 + 4] = (i + 1) * (1 + width * density) + j;
                triangles[(i * width * density + j) * 6 + 5] = (i + 1) * (1 + width * density) + j + 1;
            }
        }
    }
}
