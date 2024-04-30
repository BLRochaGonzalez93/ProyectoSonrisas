using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

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
        
        GameObject propSpline = Instantiate(rail.gameObject, plantilla.transform);
        Debug.Log(propSpline.name);
        for (int i = 0; i < numPropsCerca; i++)
        {
            int randomPoint = Random.Range(0, plantilla.transform.GetChild(plantilla.transform.childCount-(i+1)).GetComponent<SplineAdvanced>().GetPointList().Count);
            Vector3 pos = propSpline.GetComponent<SplineAdvanced>().GetPointByIndex(randomPoint).position;
            GameObject prop = Instantiate(cerca[Random.Range(0, cerca.Count-1)], plantilla.transform);
            prop.transform.position = pos;

            Debug.DrawRay(pos, propSpline.GetComponent<SplineAdvanced>().GetPointByIndex(randomPoint).forward, Color.yellow, 10f);

            //prop.transform.position = propSpline.GetComponent<SplineAdvanced>().GetPositionAtUnits(propSpline.GetComponent<SplineAdvanced>().GetPointByIndex(randomPoint).t);
            //prop.transform.forward = propSpline.GetComponent<SplineAdvanced>().GetForwardAtUnits(propSpline.GetComponent<SplineAdvanced>().GetPointByIndex(randomPoint).t);
        }
    }
}
