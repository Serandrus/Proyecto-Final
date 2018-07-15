using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Enemy;

public class Projectile : MonoBehaviour
{
    //Rigidbody particleExplosion;
    int destructionTime = 2;
    Global global;
    Hero hero;
    Vector3 resetEnemy;

    // Use this for initialization
    void Start ()
    {
        global = FindObjectOfType<Global>().GetComponent<Global>();
        hero = FindObjectOfType<Hero>().GetComponent<Hero>();
        Destroy(gameObject, destructionTime);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Zombie>())
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
            global.zombieCount--;
            global.zombieText.text = "Zombies = " + global.zombieCount.ToString();
        }
        if (other.gameObject.GetComponent<OPZombie>())
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
            global.oPZombieCount--;
            global.oPZombieText.text = "OPZombies = " + global.oPZombieCount.ToString();
        }

        if (global.zombieCount == 0 && global.oPZombieCount == 0)
        {
            global.youWinText.SetActive(true);
            global.exitButton.SetActive(true);
            Time.timeScale = 0;
            hero.gameObject.GetComponent<FPSAim>().enabled = false;
            Camera.main.gameObject.GetComponent<FPSAim>().enabled = false;
        }
    }
}
