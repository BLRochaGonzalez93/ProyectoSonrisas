using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
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

    private Dictionary<GameObject, Transform> selectorTransformMap;

    public int num_keys;

    private int keys_collected;

    [SerializeField] KeyHUD keyHUD;

    public GameObject door;


   
    void Start()
    {
        selectorTransformMap = new Dictionary<GameObject, Transform>();
        foreach (var pair in selectorTransformPairs)
        {
            selectorTransformMap.Add(pair.selector, pair.targetTransform);
        }
    }

    async void Update()
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
                    hitObject.SetActive(false);
                    if (hitObject.CompareTag("Key")){
                        keys_collected += 1;
                        print(keys_collected);
                        keyHUD.Keys += 1;
                    }
                    selectionTime = 3f;
                }
                

            }
            if (keys_collected >= num_keys){
                door.SetActive(false);
                print("The door is open");
            }
        }
    }
}

