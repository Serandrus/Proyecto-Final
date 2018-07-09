using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Global global;
    float projectileSpeed = 5f;

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Fire1") && Time.timeScale == 1.0f)
        {
            Rigidbody clone;
            clone = Instantiate(global.projectile, transform.position, transform.rotation) as Rigidbody;
            Physics.IgnoreCollision(clone.GetComponent<Collider>(), GetComponent<Collider>());
            clone.velocity = transform.TransformDirection(Vector3.forward * projectileSpeed);
        }
	}
}
