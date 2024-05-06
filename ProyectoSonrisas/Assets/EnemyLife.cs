using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    public int life;
    private Animator anim;
    void Start()
    {
        anim= GetComponent<Animator>();
    }

    void Update()
    {
        
        EnemyDie();
    }

    public void EnemyDie()
    {
        if (life<=0)
        {
            //enemigo muere, se genera animación
            Debug.Log("Enemig muerto");
            anim.SetBool("isDeath", true);
           // anim.Play("EnemyDown");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tomato"))
        {
            life--;
            
        }
    }
    
}
