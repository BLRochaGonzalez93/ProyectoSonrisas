using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject aliade;
    public GameObject aliade_ref;

    public bool entered = false;

    private void Start()
    {
        aliade = GameObject.FindGameObjectWithTag("Aliade");
        aliade_ref = GameObject.FindGameObjectWithTag("AliadeRef");
    }

    void Update()
    {
        if (entered)
        {
            //aliade.transform.position = Vector3.MoveTowards(aliade.transform.position, transform.position, GameObject.FindWithTag("Respawn").GetComponent<RailPositionerManager>().speed * Time.deltaTime);
            aliade.transform.position = Vector3.MoveTowards(aliade.transform.position, transform.position, 18f* Time.deltaTime);
        }
        else
        {
            aliade.transform.position = Vector3.MoveTowards(aliade.transform.position, aliade_ref.transform.position, 15f * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            entered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            entered = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

