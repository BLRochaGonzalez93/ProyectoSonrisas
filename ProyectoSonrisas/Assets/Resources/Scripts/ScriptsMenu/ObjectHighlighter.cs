using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectHighlighter : MonoBehaviour
{
    public float maxDistance = 10f;
    private Camera cam;
    private GameObject currentHighlightedObject;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        SelectLevelWithCam();
        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); // Rayo desde el centro de la pantalla

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("Selectable"))
            {
                if (hitObject != currentHighlightedObject)
                {
                    if (currentHighlightedObject != null)
                    {
                        currentHighlightedObject.GetComponent<SelectableObject>().ResetColor();
                    }

                    currentHighlightedObject = hitObject;
                    currentHighlightedObject.GetComponent<SelectableObject>().HighlightObject();
                }
            }
            else
            {
                if (currentHighlightedObject != null)
                {
                    currentHighlightedObject.GetComponent<SelectableObject>().ResetColor();
                    currentHighlightedObject = null;
                }
            }
        }
        else
        {
            if (currentHighlightedObject != null)
            {
                currentHighlightedObject.GetComponent<SelectableObject>().ResetColor();
                currentHighlightedObject = null;
            }
        }
    }

    private void SelectLevelWithCam()
    {
        RaycastHit hit;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); 

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("Level1"))
            {

                SceneManager.LoadScene("BasicScene");

            }
        }
    }
}