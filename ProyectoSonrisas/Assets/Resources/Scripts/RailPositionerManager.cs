using UnityEngine;
using UnityEngine.Splines;

public class RailPositionerManager : MonoBehaviour
{
    public float spawnTimer;
    public SplineContainer spline;
    public GameObject manager;
    public Rail currentRail, rail = null, previousRail = null;
    public GameObject currentMeshRail, meshRail, previousMeshRail;

    // Start is called before the first frame update
    void Start()
    {
        spline.transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > 2)
        {
            //Definitivo
            //rail = manager.GetComponent<RailSelectorManagement>().RailSelector();

            //Pruebas
            Destroy(previousMeshRail);
            int rng = UnityEngine.Random.Range(0, 3);
            rail = manager.GetComponent<RailSelectorManagement>().railPrefabs[rng];


            meshRail = Instantiate(rail.MeshPrefab,
                                new Vector3(currentMeshRail.transform.GetChild(1).transform.position.x,
                                            currentMeshRail.transform.GetChild(1).transform.position.y,
                                            currentMeshRail.transform.GetChild(1).transform.position.z),
                                new Quaternion(currentMeshRail.transform.GetChild(1).transform.rotation.x,
                                                currentMeshRail.transform.GetChild(1).transform.rotation.y,
                                                currentMeshRail.transform.GetChild(1).transform.rotation.z,
                                                currentMeshRail.transform.GetChild(1).transform.rotation.w));
            /*
            Spline tempSpline = Instantiate(rail.splinePrefab.spl,
                                new Vector3(currentMeshRail.transform.GetChild(1).transform.position.x,
                                            currentMeshRail.transform.GetChild(1).transform.position.y,
                                            currentMeshRail.transform.GetChild(1).transform.position.z),
                                new Quaternion(currentMeshRail.transform.GetChild(1).transform.rotation.x,
                                                currentMeshRail.transform.GetChild(1).transform.rotation.y,
                                                currentMeshRail.transform.GetChild(1).transform.rotation.z,
                                                currentMeshRail.transform.GetChild(1).transform.rotation.w));
            */

            //BezierKnot kn = new BezierKnot(new float3(-70f, 0f, -70f), );

            previousMeshRail = currentMeshRail;
            previousRail = currentRail;
            currentMeshRail = meshRail;
            currentRail = rail;

            spawnTimer = 0;
        }
    }
}
