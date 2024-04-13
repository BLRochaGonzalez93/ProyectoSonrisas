using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;





public class Collect : MonoBehaviour
{
    [System.Serializable]
    public struct SelectorTransformPair
    {
        public GameObject selector;
        public Transform targetTransform;
    }

    public SelectorTransformPair[] selectorTransformPairs; 

    public float selectionTime = 3f; 

    private float speed = 15f;

    private Dictionary<GameObject, Transform> selectorTransformMap;

    public int num_keys;

    private int keys_collected;

    [SerializeField] KeyHUD keyHUD;

    [SerializeField] GameObject tomato;

    [SerializeField] GameObject wagon;

    private GameObject target;

    public GameObject door;



   
    void Start()
    {
        selectorTransformMap = new Dictionary<GameObject, Transform>();
        foreach (var pair in selectorTransformPairs)
        {
            selectorTransformMap.Add(pair.selector, pair.targetTransform);
        }
        tomato.SetActive(false);
    }

    void Update()
    {
    
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (selectorTransformMap.ContainsKey(hitObject))
            {
                selectionTime -= Time.deltaTime;
                Debug.Log("Objeto detectado: " + hitObject.name);
                if (selectionTime <= 0){
                    tomato.SetActive(true);
                    tomato.transform.position = wagon.transform.position;   
                    target = hitObject;
                    selectionTime = 3f;

                    
                }
            }
            
        }
        if (tomato.activeSelf)
        {
            tomato.transform.position = Vector3.MoveTowards(tomato.transform.position, target.transform.position, speed*Time.deltaTime);

            if (Vector3.Distance(tomato.transform.position, target.transform.position) < 1)
            {
                tomato.SetActive(false);
                target.SetActive(false);

                if (target.CompareTag("Key"))
                    {
                        keys_collected += 1;
                        print(keys_collected);
                        keyHUD.Keys += 1;
                    }

                if (keys_collected >= num_keys)
                {
                    door.SetActive(false);
                    print("The door is open");
                }
            }

        }
    }
}

