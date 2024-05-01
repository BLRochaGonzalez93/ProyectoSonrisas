using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;




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

    public int num_keys;

    private int keys_collected;

    [SerializeField] KeyHUD keyHUD;

    [SerializeField] GameObject tomato;

    [SerializeField] GameObject weapon;

    private Animator fall;

    private GameObject target;

    public GameObject door;


    public InputActionReference shoot;

    public InputActionReference grab;
    
    public Camera cam;
   
    private int enemies_killed;
    public int n_enemies;
    public GameObject boss;

    private int counter;
    void Start()
    {
        tomato.SetActive(false);
    }

    void Update()
    {
        float shootValue = shoot.action.ReadValue<float>();
    
        RaycastHit hit;
        //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;

                selectionTime -= Time.deltaTime;
                Debug.Log("Objeto detectado: " + hitObject.name);
                if (selectionTime <= 0 && (hit.collider.gameObject.tag == "Key" || hit.collider.gameObject.tag == "Enemy" || hit.collider.gameObject.tag == "Boss")){
                    tomato.SetActive(true);
                    tomato.transform.position = weapon.transform.position;   
                    target = hitObject;
                    selectionTime = 3f;

                    

            }
            
        }
        if (tomato.activeSelf)
        {
            tomato.transform.position = Vector3.MoveTowards(tomato.transform.position, target.transform.position, speed*Time.deltaTime);

            if (Vector3.Distance(tomato.transform.position, target.transform.position) < 1)
            {
                tomato.SetActive(false);
                

                if (target.CompareTag("Key"))
                {
                    target.SetActive(false);
                    keys_collected += 1;
                    print(keys_collected);
                    keyHUD.Keys += 1;
                }
                
                if (target.CompareTag("Enemy")){
                    if (counter < 1)
                    {
                        counter ++;
                    }
                    else 
                    {
                        fall = target.GetComponent<Animator>();
                        fall.SetBool("fall",true);
                        target.transform.position = new Vector3(target.transform.position.x,0,target.transform.position.z); 
                        //target.SetActive(false);
                        enemies_killed += 1;
                        counter = 0;
                    }
                    
                }

                if (target.CompareTag("Boss")){
                    if (counter < 9)
                    {
                        counter ++;
                    }
                    else 
                    {
                        fall = target.GetComponent<Animator>();
                        fall.SetBool("fall",true);
                        target.transform.position = new Vector3(target.transform.position.x,0,target.transform.position.z); 
                        enemies_killed += 1;
                        counter = 0;
                    }
                    
                }

                if (enemies_killed == n_enemies)
                {
                    BoxCollider box = boss.GetComponent<BoxCollider>();
                    box.enabled = true;
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

