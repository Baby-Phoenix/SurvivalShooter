using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitting : MonoBehaviour
{
    public Animator EnemyAnim;
    public Transform target;
    public GameObject PlayerHealth;


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hand touching player");
            if (EnemyAnim.GetCurrentAnimatorStateInfo(1).IsName("Attack"))
            {
                
                    Debug.Log("Hand hitting body");


                    PlayerHealth.GetComponent<PlayerMovement>().health -= 5;
                    GameObject.Find("HealthBar").GetComponent<HealthBar>().SetHealth(PlayerHealth.GetComponent<PlayerMovement>().health);

                    Debug.Log(GameObject.Find("HealthBar").GetComponent<HealthBar>().slider.value);
                
            }
        }
    }
       
    
}
