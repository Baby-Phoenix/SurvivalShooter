using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitting : MonoBehaviour
{
    public Animator EnemyAnim;
    public Transform target;


    private void Update()
    {


        float distance = Vector3.Distance(transform.position, target.position);

        if (EnemyAnim.GetCurrentAnimatorStateInfo(1).IsName("Attack"))
        {
            if (distance < 3) 
            { 
                //Debug.Log("Hand hitting body");
                int tempHealth = GameObject.Find("PlayerControllerFPS").GetComponent<PlayerMovement>().health--;
                GameObject.Find("HealthBar").GetComponent<HealthBar>().SetHealth(tempHealth);

            }
           
            

        }
    }
       
    
}
