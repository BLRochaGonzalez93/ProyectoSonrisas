using UnityEngine;

public class CreateTiledTerrain : MonoBehaviour
{
    Mesh mesh;
    GameObject pitPrefab;


    private void Start()
    {
        int index = Random.Range(0, mesh.vertexCount);
        Vector3 someRandomlySelectedVertexPosition = mesh.vertices[index];
        Vector3 instancePos = transform.TransformPoint(someRandomlySelectedVertexPosition);
        Instantiate(pitPrefab, instancePos, Quaternion.Euler(mesh.normals[index]));
    }
}