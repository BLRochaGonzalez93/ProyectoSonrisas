using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDirectionsActivated : MonoBehaviour
{

    public GameObject[] selectors; 

    private Transform player;

    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        ControlActivation();
    }



    private void ControlActivation()
    {

        // Calcular distancias y mantener los dos selectors más cercanos
        List<GameObject> objetosMasCercanos = new List<GameObject>();
        List<float> distancias = new List<float>();

        foreach (GameObject obj in selectors)
        {
            float distancia = Vector3.Distance(obj.transform.position, player.position);
            distancias.Add(distancia);
        }

        distancias.Sort();

        // Obtener los dos selectors más cercanos
        for (int i = 0; i < selectors.Length; i++)
        {
            if (Vector3.Distance(selectors[i].transform.position, player.position) <= distancias[1])
            {
                objetosMasCercanos.Add(selectors[i]);
            }
        }

        // Activar los dos selectors más cercanos y desactivar los demás
        foreach (GameObject obj in selectors)
        {
            if (objetosMasCercanos.Contains(obj))
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
    }
}
