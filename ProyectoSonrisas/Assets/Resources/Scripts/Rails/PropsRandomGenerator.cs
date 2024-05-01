using System.Collections.Generic;
using UnityEngine;
using static SplineAdvanced;

public class PropsRandomGenerator : MonoBehaviour
{
    public SplineAdvanced rail;
    public GameObject plantilla;
    public List<GameObject> cerca;
    public List<GameObject> medio;
    public List<GameObject> lejos;
    public int numPropsCerca;
    public int numPropsMedio;
    public int numPropsLejos;
    public float timer = 0;
    private float distanciaCerca = 13.8f;
    private float distanciaMedio = 35f;
    private float distanciaLejos = 47.7f;
    private float distanciaNada = 3.15f;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        rail = GetComponent<RailPositionerManager>().rail.splinePrefab;
        plantilla = GetComponent<RailPositionerManager>().meshRail;
        timer = GetComponent<RailPositionerManager>().spawnTimer;
        
        if (timer == 0f) 
        {
            timer++;
            GenerateProps();
        }
    }

    private void GenerateProps()
    {
        GameObject propSpline = Instantiate(rail.gameObject, plantilla.transform.position, plantilla.transform.rotation, plantilla.transform);
        for (int i = 1; i < rail.GetAnchorList().Count; i++)
        {

            Anchor newAnchor = new();
            Vector3 newPosition, newHandleAPosition, newHandleBPosition;

            newPosition = Quaternion.AngleAxis(GetComponent<RailPositionerManager>().exitRotation * 45, Vector3.up) * rail.GetComponent<SplineAdvanced>().GetAnchorAtIndex(i).position;
            newHandleAPosition = Quaternion.AngleAxis(GetComponent<RailPositionerManager>().exitRotation * 45, Vector3.up) * rail.GetComponent<SplineAdvanced>().GetAnchorAtIndex(i).handleAPosition;
            newHandleBPosition = Quaternion.AngleAxis(GetComponent<RailPositionerManager>().exitRotation * 45, Vector3.up) * rail.GetComponent<SplineAdvanced>().GetAnchorAtIndex(i).handleBPosition;

            if (i == 1)
            {
                propSpline.GetComponent<SplineAdvanced>().SetAnchorValues(newAnchor,
                propSpline.GetComponent<SplineAdvanced>().GetAnchorAtIndex(propSpline.GetComponent<SplineAdvanced>().GetAnchorList().Count - 1).position + newPosition,
                propSpline.GetComponent<SplineAdvanced>().GetAnchorAtIndex(propSpline.GetComponent<SplineAdvanced>().GetAnchorList().Count - 1).position + newHandleAPosition,
                propSpline.GetComponent<SplineAdvanced>().GetAnchorAtIndex(propSpline.GetComponent<SplineAdvanced>().GetAnchorList().Count - 1).position + newHandleBPosition);
            }
            else
            {
                propSpline.GetComponent<SplineAdvanced>().SetAnchorValues(newAnchor,
                propSpline.GetComponent<SplineAdvanced>().GetAnchorAtIndex(propSpline.GetComponent<SplineAdvanced>().GetAnchorList().Count - i).position + newPosition,
                propSpline.GetComponent<SplineAdvanced>().GetAnchorAtIndex(propSpline.GetComponent<SplineAdvanced>().GetAnchorList().Count - i).position + newHandleAPosition,
                propSpline.GetComponent<SplineAdvanced>().GetAnchorAtIndex(propSpline.GetComponent<SplineAdvanced>().GetAnchorList().Count - i).position + newHandleBPosition);
            }
            propSpline.GetComponent<SplineAdvanced>().AddAnchor(newAnchor);

            if (i == 1)
            {
                propSpline.GetComponent<SplineAdvanced>().SetAnchorValues(propSpline.GetComponent<SplineAdvanced>().GetAnchorAtIndex(propSpline.GetComponent<SplineAdvanced>().GetAnchorList().Count - 2),
                propSpline.GetComponent<SplineAdvanced>().GetAnchorList().ToArray()[propSpline.GetComponent<SplineAdvanced>().GetAnchorList().Count - 2].position,
                propSpline.GetComponent<SplineAdvanced>().GetAnchorList().ToArray()[propSpline.GetComponent<SplineAdvanced>().GetAnchorList().Count - 2].handleAPosition,
                                        propSpline.GetComponent<SplineAdvanced>().GetAnchorList().ToArray()[propSpline.GetComponent<SplineAdvanced>().GetAnchorList().Count - 2].handleBPosition + (Quaternion.AngleAxis(GetComponent<RailPositionerManager>().exitRotation * 45, Vector3.up) * rail.GetAnchorAtIndex(0).handleBPosition));
            }
        }

        for (int i = 0; i < numPropsCerca; i++)
        {
            int randomPoint = Random.Range(0, plantilla.transform.GetChild(plantilla.transform.childCount-(i+1)).GetComponent<SplineAdvanced>().GetPointList().Count);
            Vector3 pos = propSpline.GetComponent<SplineAdvanced>().GetPointByIndex(randomPoint).position;
            GameObject prop = Instantiate(cerca[Random.Range(0, cerca.Count-1)], plantilla.transform);
            prop.transform.position = pos;
            prop.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            
            Debug.DrawRay(pos, propSpline.GetComponent<SplineAdvanced>().GetPointByIndex(randomPoint).forward, Color.yellow, 10f);

            bool rngDir = Random.Range(0, 2) == 1 ? true : false;
            if (rngDir)
            {
                prop.transform.localPosition += Vector3.forward * Random.Range(distanciaNada, distanciaCerca);
            }
            else
            {
                prop.transform.localPosition += Vector3.back * Random.Range(distanciaNada, distanciaCerca);
            }

            //prop.transform.position = propSpline.GetComponent<SplineAdvanced>().GetPositionAtUnits(propSpline.GetComponent<SplineAdvanced>().GetPointByIndex(randomPoint).t);
            //prop.transform.forward = propSpline.GetComponent<SplineAdvanced>().GetForwardAtUnits(propSpline.GetComponent<SplineAdvanced>().GetPointByIndex(randomPoint).t);
        }
    }
}
