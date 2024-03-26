using System.Linq;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UIElements;
using static SplineAdvanced;

public class RailPositionerManager : MonoBehaviour
{
    public float spawnTimer;
    public SplineAdvanced spline;
    public GameObject manager;
    public Rail currentRail, rail = null, previousRail = null;
    public GameObject currentMeshRail, meshRail, previousMeshRail, previous2MRail;
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
            speed += 2f;
            Destroy(previous2MRail);
            int rng = UnityEngine.Random.Range(0, 5);

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

            for (int i = 1; i < rail.splinePrefab.GetAnchorList().Count; i++)
            {
                
                Anchor newAnchor = new Anchor();
                Vector3 newPosition, newHandleAPosition, newHandleBPosition;

                newPosition = Quaternion.AngleAxis(exitRotation * 45, Vector3.up) * rail.splinePrefab.GetComponent<SplineAdvanced>().GetAnchorAtIndex(i).position;
                newHandleAPosition = Quaternion.AngleAxis(exitRotation * 45, Vector3.up) * rail.splinePrefab.GetComponent<SplineAdvanced>().GetAnchorAtIndex(i).handleAPosition;
                newHandleBPosition = Quaternion.AngleAxis(exitRotation * 45, Vector3.up) * rail.splinePrefab.GetComponent<SplineAdvanced>().GetAnchorAtIndex(i).handleBPosition;

                spline.SetAnchorValues(newAnchor,
                            spline.GetComponent<SplineAdvanced>().GetAnchorAtIndex(spline.GetComponent<SplineAdvanced>().GetAnchorList().Count - 1).position + newPosition,
                            spline.GetComponent<SplineAdvanced>().GetAnchorAtIndex(spline.GetComponent<SplineAdvanced>().GetAnchorList().Count - 1).position + newHandleAPosition,
                            spline.GetComponent<SplineAdvanced>().GetAnchorAtIndex(spline.GetComponent<SplineAdvanced>().GetAnchorList().Count - 1).position + newHandleBPosition);
            
                spline.AddAnchor(newAnchor);
                
                if (i == 1)
                {
                    spline.SetAnchorValues(spline.GetAnchorAtIndex(spline.GetAnchorList().Count - 2),
                                            spline.GetAnchorList().ToArray()[spline.GetAnchorList().Count - 2].position,
                                            spline.GetAnchorList().ToArray()[spline.GetAnchorList().Count - 2].handleAPosition,
                                            spline.GetAnchorList().ToArray()[spline.GetAnchorList().Count - 2].handleBPosition + (Quaternion.AngleAxis(exitRotation * 45, Vector3.up) * rail.splinePrefab.GetAnchorAtIndex(0).handleBPosition));
                }
            }



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
            previous2MRail = previousMeshRail;
            previousMeshRail = currentMeshRail;
            currentMeshRail = meshRail;
            currentRail = rail;

            spawnTimer = 0;
            spline.SetDirty();
        }
    }
}
