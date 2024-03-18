using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class RailPositionerManager : MonoBehaviour
{
    public float spawnTimer;
    public SplineContainer spline;
    public GameObject manager;
    public Rail currentRail, rail = null, previousRail = null;
    public GameObject currentMeshRail, meshRail, previousMeshRail;
    public int exitRotation = 0;

    void Start()
    {
        spline.transform.position = Vector3.zero;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > 2)
        {
            //Pruebas
            /*if (previousRail != null)
            {
                spline.Spline.RemoveAt(0);
            }*/
            Destroy(previousMeshRail);
            int rng = UnityEngine.Random.Range(0, 3);

            //Definitivo
            //rail = manager.GetComponent<RailSelectorManagement>().RailSelector();
            rail = manager.GetComponent<RailSelectorManagement>().railPrefabs[rng];

            meshRail = Instantiate(rail.MeshPrefab,
                                new Vector3(currentMeshRail.transform.GetChild(1).transform.position.x,
                                            currentMeshRail.transform.GetChild(1).transform.position.y,
                                            currentMeshRail.transform.GetChild(1).transform.position.z),
                                new Quaternion(currentMeshRail.transform.GetChild(1).transform.rotation.x,
                                                currentMeshRail.transform.GetChild(1).transform.rotation.y,
                                                currentMeshRail.transform.GetChild(1).transform.rotation.z,
                                                currentMeshRail.transform.GetChild(1).transform.rotation.w));

            BezierKnot kn = new BezierKnot(rail.splinePrefab.Splines[exitRotation].ToArray()[1].Position + spline.Spline.ToArray()[spline.Spline.Count - 1].Position,
                                        rail.splinePrefab.Splines[exitRotation].ToArray()[1].TangentIn,
                                        rail.splinePrefab.Splines[exitRotation].ToArray()[1].TangentOut,
                                        rail.splinePrefab.Splines[exitRotation].ToArray()[1].Rotation);
            spline.Splines.ToArray()[0].Add(kn);

            int fRot = exitRotation + rail.finalRotation;
            if (fRot > 7)
            {
                fRot -= 8;
            }
            else if (fRot < 0)
            {
                fRot += 8;
            }

            exitRotation = fRot;

            previousRail = currentRail;
            previousMeshRail = currentMeshRail;
            currentMeshRail = meshRail;
            currentRail = rail;

            spawnTimer = 0;
        }
    }
}
