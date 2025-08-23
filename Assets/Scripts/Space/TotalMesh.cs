using System.Collections.Generic;
using UnityEngine;

public class TotalMesh : MonoBehaviour
{
    private const int TOP_INDEX = 0;

    [SerializeField] private float height = 1f;
    [SerializeField] private float radius = 6.78f;
    [SerializeField] private int segments = 32;
    
    [Space(15)]
    [SerializeField] private Material material;

    private List<Vector3> vertices = new();
    private List<int> triangles = new();

    private Vector3 pos;
    private float angle;
    private float angleAmount;
    
    private Mesh mesh;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        InitializeVertices();
        InitializeTriangles();
        InitializeMesh();
    }

    public void SetCrossPos(Vector3 crossPos)
    {
        vertices[TOP_INDEX] = transform.InverseTransformPoint(crossPos);
        mesh.vertices = vertices.ToArray();
    }
    
    private void InitializeVertices()
    {
        var crossPos = new Vector3(-height, 0, 0);
        vertices.Add(crossPos);

        var basePos = new Vector3(0, 0, 0);
        vertices.Add(basePos);

        pos = Vector3.zero;
        angle = 0f;
        angleAmount = 2f * Mathf.PI / segments;
        for (int i = 0; i < segments; i++)
        {
            pos.y = radius * Mathf.Sin(angle);
            pos.z = radius * Mathf.Cos(angle);

            var circlePointPos = new Vector3(pos.x, pos.y, pos.z);
            vertices.Add(circlePointPos);

            angle -= angleAmount;
        }
    }

    private void InitializeTriangles()
    {
        for (int i = 2; i < segments + 1; i ++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i);
        }
        
        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(segments + 1);
    }
    
    private void InitializeMesh()
    {
        var meshFilter = gameObject.AddComponent<MeshFilter>();
        mesh = new Mesh
        {
            vertices = vertices.ToArray(),
            triangles = triangles.ToArray()
        };
        meshFilter.mesh = mesh;
        
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = material;
    }
}
