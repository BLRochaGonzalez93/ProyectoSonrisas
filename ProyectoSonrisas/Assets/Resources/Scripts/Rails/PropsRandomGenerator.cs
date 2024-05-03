using System.Collections.Generic;
using UnityEngine;
using static SplineAdvanced;

public class PropsRandomGenerator : MonoBehaviour
{
    public List<GameObject> cerca;
    public List<GameObject> medio;
    public List<GameObject> lejos;
    public int numPropsCerca;
    public int numPropsMedio;
    public int numPropsLejos;
    private float distanciaCerca = 13.8f;
    private float distanciaMedio = 35f;
    private float distanciaLejos = 47.7f;
    private float distanciaNada = 4.15f;

    public void GenerateProps(SplineAdvanced spline, Rail rail, GameObject meshRail)
    {
        for (int i = 0; i < numPropsCerca; i++)
        {
            int randomPoint = Random.Range(spline.GetPointList().Count - 360, spline.GetPointList().Count);
            Debug.Log(randomPoint);
            Vector3 pos = spline.GetPointByIndex(randomPoint).position;
            GameObject prop = Instantiate(cerca[Random.Range(0, cerca.Count-1)], meshRail.transform);
            prop.transform.position = pos;
            prop.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            
            bool rngDir = Random.Range(0, 2) == 1 ? true : false;
            if (rngDir)
            {
                prop.transform.localPosition += Vector3.forward * Random.Range(distanciaNada, distanciaCerca);
            }
            else
            {
                prop.transform.localPosition += Vector3.back * Random.Range(distanciaNada, distanciaCerca);
            }
        }

        for (int i = 0; i < numPropsMedio; i++)
        {
            int randomPoint = Random.Range(spline.GetPointList().Count - 360, spline.GetPointList().Count);
            Debug.Log(randomPoint);
            Vector3 pos = spline.GetPointByIndex(randomPoint).position;
            GameObject prop = Instantiate(medio[Random.Range(0, medio.Count - 1)], meshRail.transform);
            prop.transform.position = pos;
            bool rngDir = Random.Range(0, 2) == 1 ? true : false;
            if (rngDir)
            {
                prop.transform.localPosition += Vector3.forward * Random.Range(distanciaCerca, distanciaMedio);
            }
            else
            {
                prop.transform.localPosition += Vector3.back * Random.Range(distanciaCerca, distanciaMedio);
            }
        }

        for (int i = 0; i < numPropsLejos; i++)
        {
            int randomPoint = Random.Range(spline.GetPointList().Count - 360, spline.GetPointList().Count);
            Debug.Log(randomPoint);
            Vector3 pos = spline.GetPointByIndex(randomPoint).position;
            GameObject prop = Instantiate(lejos[Random.Range(0, lejos.Count - 1)], meshRail.transform);
            prop.transform.position = pos;
            bool rngDir = Random.Range(0, 2) == 1 ? true : false;
            if (rngDir)
            {
                prop.transform.localPosition += Vector3.forward * Random.Range(distanciaMedio, distanciaLejos);
            }
            else
            {
                prop.transform.localPosition += Vector3.back * Random.Range(distanciaMedio, distanciaLejos);
            }
        }
    }
}
