using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMove : MonoBehaviour
{
	public void Move (float speed)
    {
		if (Input.GetKey(KeyCode.W)) //Función que establece una entrada para la tecla W
        {
            transform.position += transform.forward * speed * Time.deltaTime; //Procede a desplazar al objeto hacia el frente.
        }

        if (Input.GetKey(KeyCode.S)) //Función que establece una entrada para la tecla S
        {
            transform.position -= transform.forward * speed * Time.deltaTime; //Procede a desplazar al objeto negativamente hacia el frente.
        }

        if (Input.GetKey(KeyCode.A)) //Función que establece una entrada para la tecla A
        {
            transform.position -= transform.right * speed * Time.deltaTime; //Procede a desplazar al objeto negativamente hacia la derecha.
        }

        if (Input.GetKey(KeyCode.D)) //Función que establece una entrada para la tecla D
        {
            transform.position += transform.right * speed * Time.deltaTime; //Procede a desplazar al objeto hacia la derecha.
        }
    }
}
