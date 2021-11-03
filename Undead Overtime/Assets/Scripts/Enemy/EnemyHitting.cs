using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitting : MonoBehaviour
{
    public Animator EnemyAnim;
    public Transform target;




    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hand touching player");
            if (EnemyAnim.GetCurrentAnimatorStateInfo(1).IsName("Attack"))
            {
                    Debug.Log("Hand hitting body");
                    int tempHealth = GameObject.Find("PlayerControllerFPS").GetComponent<PlayerMovement>().health--;
                    GameObject.Find("HealthBar").GetComponent<HealthBar>().SetHealth(tempHealth);

                    Debug.Log(GameObject.Find("HealthBar").GetComponent<HealthBar>().slider.value);
                
            }
        }
    }
       
    
}
