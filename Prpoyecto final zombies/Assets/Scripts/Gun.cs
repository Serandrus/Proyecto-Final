using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Global global;
    public Rigidbody projectile;
    float projectileSpeed = 25f;

    void Start()
    {
        global = FindObjectOfType<Global>().GetComponent<Global>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("Fire1") && global.ammo > 0 &&Time.timeScale == 1.0f)
        {
            global.ammo--;
            global.ammoText.text = global.ammo.ToString();
            Rigidbody clone;
            clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            Physics.IgnoreCollision(clone.GetComponent<Collider>(), GetComponent<Collider>());
            clone.velocity = transform.TransformDirection(Vector3.forward * projectileSpeed);
        }
	}
}
