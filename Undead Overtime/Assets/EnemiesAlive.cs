using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesAlive : MonoBehaviour
{
    public int enemiesAlive = 0;
    public Text enemies;
    // Update is called once per frame
   public void Update()
    {
        enemies.text = "Enemies Alive: " + enemiesAlive.ToString();
    }
}
