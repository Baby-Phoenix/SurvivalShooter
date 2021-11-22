using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitting : MonoBehaviour
{
    public Animator EnemyAnim;
    private GameObject playerObj;


    private void Start()
    {
        playerObj = GameObject.Find("PlayerControllerFPS");
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            if (EnemyAnim.GetCurrentAnimatorStateInfo(1).IsName("Attack"))
            {
                    playerObj.GetComponent<PlayerMovement>().health -= 5;
                    GameObject.Find("HealthBar").GetComponent<HealthBar>().SetHealth(playerObj.GetComponent<PlayerMovement>().health);

                
            }
        }
    }
       
    
}
