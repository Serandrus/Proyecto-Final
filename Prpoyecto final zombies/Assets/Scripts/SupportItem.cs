using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportItem : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    void Appear()
    {
        Vector3 pos = new Vector3(Random.Range(-50f, 50f), 0.3f, Random.Range(-50f, 50f));
        transform.position = pos;
        gameObject.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Hero>())
        {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        Invoke("Appear", 5);
    }
}
