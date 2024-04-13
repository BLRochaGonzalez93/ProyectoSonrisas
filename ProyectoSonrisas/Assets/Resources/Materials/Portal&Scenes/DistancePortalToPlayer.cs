using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DistancePortalToPlayer : MonoBehaviour
{
    public float dist;
    Transform player;
    Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(transform.position, player.position);

        if (dist <= 30f && !anim.isPlaying)
        {
            anim.Play("OpenPortal");
        }
    }
}
