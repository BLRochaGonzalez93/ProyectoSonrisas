using UnityEngine;

public class SpawnRandom : MonoBehaviour
{
    Mesh mesh;
    GameObject pitPrefab;
    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, mesh.vertexCount);
        Vector3 someRandomlySelectedVertexPosition = mesh.vertices[index];
        Vector3 instancePos = transform.TransformPoint(someRandomlySelectedVertexPosition);
        Instantiate(pitPrefab, instancePos, Quaternion.Euler(mesh.normals[index]));
    }
}
