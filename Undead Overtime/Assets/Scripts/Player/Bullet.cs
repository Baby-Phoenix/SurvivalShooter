using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    public Transform barrelEnd;

    // Update is called once per frame
  public void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("GUN");
        Rigidbody bulletInstance;
            bulletInstance = Instantiate(bulletPrefab, barrelEnd.position, barrelEnd.rotation) as Rigidbody;
            bulletInstance.AddForce(barrelEnd.forward * 5000);
    }


}
