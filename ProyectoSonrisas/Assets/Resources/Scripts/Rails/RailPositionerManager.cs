using System.Collections.Generic;
using UnityEngine;
using static SplineAdvanced;

public class RailPositionerManager : MonoBehaviour
{
    public float spawnTimer;
    public List<SplineAdvanced> splines;
    public SplineAdvanced spline;
    public GameObject manager;
    public Rail currentRail, rail = null, previousRail = null;
    public GameObject currentMeshRail, meshRail, previousMeshRail, previous2MRail;
    public int exitRotation = 0;
    public float speed;
    public float timeValue;
    public float pathSpawnerFactor;
    public int railCounter = 0;
    public int railResets = 0;
    public GameObject newSplinePrefab;
    public SplineAdvanced newSpline = null;


    void Start()
    {
        for (int i = 0; i < splines.Count; i++)
        {
            splines[i].transform.position = Vector3.zero;
        }
        spline = splines[0];
    }

    void FixedUpdate()
    {
        /*if (railCounter == 7 && railResets == 3)
        {
            GetComponent<SplineFollower>().NewCycle();
        }*/

        pathSpawnerFactor = 200 / speed;
        //speed = transform.GetComponent<SplineFollower>().speed;

        //timeValue = transform.GetComponent<SplineAnimate>().NormalizedTime;
        //transform.GetComponent<SplineAnimate>().MaxSpeed += 0.001f;

        spawnTimer += Time.deltaTime;

        if (railResets > 0)
        {
            spline = newSpline;
        }

        if (railCounter < 7)
        {
            if (spawnTimer > pathSpawnerFactor)
            {

                //Pruebas
                //speed += 1f;
                Destroy(previous2MRail);
                int rng = UnityEngine.Random.Range(0, 23);

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


                for (int i = 1; i < rail.splinePrefab.GetAnchorList().Count; i++)
                {

                    Anchor newAnchor = new();
                    Vector3 newPosition, newHandleAPosition, newHandleBPosition;

                    newPosition = Quaternion.AngleAxis(exitRotation * 45, Vector3.up) * rail.splinePrefab.GetComponent<SplineAdvanced>().GetAnchorAtIndex(i).position;
                    newHandleAPosition = Quaternion.AngleAxis(exitRotation * 45, Vector3.up) * rail.splinePrefab.GetComponent<SplineAdvanced>().GetAnchorAtIndex(i).handleAPosition;
                    newHandleBPosition = Quaternion.AngleAxis(exitRotation * 45, Vector3.up) * rail.splinePrefab.GetComponent<SplineAdvanced>().GetAnchorAtIndex(i).handleBPosition;

                    if (i == 1)
                    {
                        spline.SetAnchorValues(newAnchor,
                                    spline.GetAnchorAtIndex(spline.GetAnchorList().Count - 1).position + newPosition,
                                    spline.GetAnchorAtIndex(spline.GetAnchorList().Count - 1).position + newHandleAPosition,
                                    spline.GetAnchorAtIndex(spline.GetAnchorList().Count - 1).position + newHandleBPosition);
                    }
                    else
                    {
                        spline.SetAnchorValues(newAnchor,
                                    spline.GetAnchorAtIndex(spline.GetAnchorList().Count - i).position + newPosition,
                                    spline.GetAnchorAtIndex(spline.GetAnchorList().Count - i).position + newHandleAPosition,
                                    spline.GetAnchorAtIndex(spline.GetAnchorList().Count - i).position + newHandleBPosition);
                    }

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
                railCounter++;
                spawnTimer = 0;
                spline.SetDirty();
            }
        }
        else
        {
            if (spawnTimer > pathSpawnerFactor)
            {
                Destroy(previous2MRail);
                newSpline = splines[splines.IndexOf(spline) + 1];

                rail = manager.GetComponent<RailSelectorManagement>().railPrefabs[0];
                meshRail = Instantiate(rail.MeshPrefab, 
                                new Vector3(currentMeshRail.transform.GetChild(1).transform.position.x,
                                                currentMeshRail.transform.GetChild(1).transform.position.y,
                                                currentMeshRail.transform.GetChild(1).transform.position.z),
                                new Quaternion(currentMeshRail.transform.GetChild(1).transform.rotation.x,
                                                currentMeshRail.transform.GetChild(1).transform.rotation.y,
                                                currentMeshRail.transform.GetChild(1).transform.rotation.z,
                                                currentMeshRail.transform.GetChild(1).transform.rotation.w));

                for (int i = 0; i < rail.splinePrefab.GetAnchorList().Count; i++)
                {

                    Vector3 newPosition, newHandleAPosition, newHandleBPosition;

                    newPosition = Quaternion.AngleAxis(exitRotation * 45, Vector3.up) * rail.splinePrefab.GetComponent<SplineAdvanced>().GetAnchorAtIndex(i).position;
                    newHandleAPosition = Quaternion.AngleAxis(exitRotation * 45, Vector3.up) * rail.splinePrefab.GetComponent<SplineAdvanced>().GetAnchorAtIndex(i).handleAPosition;
                    newHandleBPosition = Quaternion.AngleAxis(exitRotation * 45, Vector3.up) * rail.splinePrefab.GetComponent<SplineAdvanced>().GetAnchorAtIndex(i).handleBPosition;

                    newSpline.SetAnchorValues(newSpline.GetAnchorAtIndex(i), newPosition, newHandleAPosition, newHandleBPosition);
                }

                newSpline.transform.position = meshRail.transform.position;

                previousRail = currentRail;
                previous2MRail = previousMeshRail;
                previousMeshRail = currentMeshRail;
                currentMeshRail = meshRail;
                currentRail = rail;
                railCounter = 0;
                spawnTimer = 0;
                railResets++;
                //spline = newSpline.GetComponent<SplineAdvanced>();
            }
        }
    }
}   
