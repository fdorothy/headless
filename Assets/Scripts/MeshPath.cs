using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshPath : MonoBehaviour
{
    public float width = 1.0f;
    public float height = 20f;
    int resolutionWidth = 100;
    int resolutionHeight = 5;
    float heightScale = 0.1f;
    public float offset = 0f;

    public Color pathColor = Color.red;
    public Color edgeColor = Color.green;
    public float falloff = 1.0f;

    Vector3[] vertices;
    Color[] colors;

    public Vector3[] getVertices() { return vertices; }

    MeshFilter meshFilter;
    Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        GeneratePath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GeneratePath()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;

        vertices = new Vector3[resolutionWidth * resolutionHeight];
        colors = new Color[vertices.Length];

        RecalculateVertexPositions();

        int[] triangles = new int[3 * 2 * resolutionWidth * resolutionHeight];
        int index = 0;
        for (int i=0; i<resolutionWidth-1; i++)
        {
            for (int j=0; j<resolutionHeight-1; j++)
            {
                triangles[index++] = i + j * resolutionWidth;
                triangles[index++] = i + 1 + (j + 1) * resolutionWidth;
                triangles[index++] = i + 1 + j * resolutionWidth;

                triangles[index++] = i + j * resolutionWidth;
                triangles[index++] = i + (j + 1) * resolutionWidth;
                triangles[index++] = i + 1 + (j + 1) * resolutionWidth;
            }
        }

        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }

    public void RecalculateVertexPositions(bool setMesh = false)
    {
        for (int i = 0; i < resolutionWidth; i++)
        {
            float t = (float)i / (resolutionWidth - 1);
            float x = t * height;
            float h = GetHeight(x + offset);
            for (int j = 0; j < resolutionHeight; j++)
            {
                float s = (float)j / (resolutionHeight - 1);
                vertices[i + j * resolutionWidth] = new Vector3(x, 0f, s * width) + Vector3.forward * h;
                colors[i + j * resolutionWidth] = Color.Lerp(edgeColor, pathColor, TriangleWave(s));
            }
        }

        mesh.vertices = vertices;
        mesh.colors = colors;
        if (setMesh)
            meshFilter.mesh = mesh;
    }

    // Gets the heigh of terrain at current position.
    // Modify this fuction to get different terrain configuration.
    private float GetHeight(float position)
    {
        return (Mathf.Sin(position) + 1.5f + Mathf.Sin(position * 1.75f) + 1f) / 2f * heightScale;
    }

    public float TriangleWave(float t)
    {
        if (t > 0.5f) return 2.0f - 2 * t;
        else return 2 * t;
    }
}
