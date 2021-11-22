using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    EnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        Destroy (gameObject, 1.5f);
        spawner = GameObject.Find("Spawners").GetComponent<EnemySpawner>();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            
            Destroy(col.gameObject);

            spawner.enemiesKilled++;

            Destroy(gameObject);
        }
    }
}
