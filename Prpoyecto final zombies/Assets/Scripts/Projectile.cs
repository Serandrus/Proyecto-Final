using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Rigidbody particleExplosion;
    int destructionTime = 5; 

	// Use this for initialization
	void Start ()
    {
        Destroy(gameObject, destructionTime);
	}
}
