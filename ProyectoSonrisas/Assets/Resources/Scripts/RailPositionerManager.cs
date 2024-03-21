using System.Linq;
using UnityEngine;
using UnityEngine.Splines;
using static SplineAdvanced;

public class RailPositionerManager : MonoBehaviour
{
    public float spawnTimer;
    public SplineAdvanced spline;
    public GameObject manager;
    public Rail currentRail, rail = null, previousRail = null;
    public GameObject currentMeshRail, meshRail, previousMeshRail;
    public int exitRotation = 0;
    public float speed;
    public float timeValue;


    void Start()
    {
        spline.transform.position = Vector3.zero;
    }

    void Update()
    {
        //speed = transform.GetComponent<SplineFollower>().speed;

        //timeValue = transform.GetComponent<SplineAnimate>().NormalizedTime;
        //transform.GetComponent<SplineAnimate>().MaxSpeed += 0.001f;

        spawnTimer += Time.deltaTime;
        if (spawnTimer > 5f)
        {
            

            //Pruebas
            /*if (previousRail != null)
            {
                spline.Spline.RemoveAt(0);
            }*/
            //Destroy(previousMeshRail);
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

            //Anchor newAnchor = spline.GetAnchorAtIndex(spline.GetAnchorList().Count-1);


            Anchor newAnchor = new Anchor();
            Vector3 newPosition, newHandleAPosition, newHandleBPosition;
            
            newPosition = Quaternion.AngleAxis(exitRotation * 45, Vector3.up) * rail.splinePrefab.GetComponent<SplineAdvanced>().GetAnchorAtIndex(1).position;
            newHandleAPosition = Quaternion.AngleAxis(exitRotation * 45, Vector3.up) * rail.splinePrefab.GetComponent<SplineAdvanced>().GetAnchorAtIndex(1).handleAPosition;
            newHandleBPosition = Quaternion.AngleAxis(exitRotation * 45, Vector3.up) * rail.splinePrefab.GetComponent<SplineAdvanced>().GetAnchorAtIndex(1).handleBPosition;

            spline.SetAnchorValues(newAnchor,
                        spline.GetComponent<SplineAdvanced>().GetAnchorAtIndex(spline.GetComponent<SplineAdvanced>().GetAnchorList().Count - 1).position + newPosition,
                        spline.GetComponent<SplineAdvanced>().GetAnchorAtIndex(spline.GetComponent<SplineAdvanced>().GetAnchorList().Count - 1).position + newHandleAPosition,
                        spline.GetComponent<SplineAdvanced>().GetAnchorAtIndex(spline.GetComponent<SplineAdvanced>().GetAnchorList().Count - 1).position + newHandleBPosition);
            
            spline.AddAnchor(newAnchor);
            spline.SetDirty();



           /* BezierKnot kn = new BezierKnot(rail.splinePrefab.Splines[exitRotation].ToArray()[1].Position + spline.Spline.ToArray()[spline.Spline.Count - 1].Position,
                                        rail.splinePrefab.Splines[exitRotation].ToArray()[1].TangentIn,
                                        rail.splinePrefab.Splines[exitRotation].ToArray()[1].TangentOut,
                                        rail.splinePrefab.Splines[exitRotation].ToArray()[1].Rotation);
            spline.Splines.ToArray()[0].Add(kn);
           */

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
