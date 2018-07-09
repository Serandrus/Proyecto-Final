using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Ally;
[RequireComponent(typeof(Rigidbody))]
public class Npc : MonoBehaviour
{
    public Parameters info; //Calls the struct that contains Zombie info.
    bool isReacting = false; //See if the npc is reacting.
    

    void StartMoving() //Define what to do if the zombie is moving
    {
        switch (info.zBehavior)
        {
            case Behavior.Moving:
                info.type = Random.Range(1, 7); //Choose a random case of movement
                StartCoroutine(Movement()); //Starts the coroutine
                break;
            case Behavior.Rotating:
                info.type = 6;
                StartCoroutine(Movement());
                break;
            case Behavior.Idle:
                info.type = 5; //Select the movement case 5
                StartCoroutine(Movement());
                break;
            case Behavior.React:
                if(isReacting == false)
                {
                    StartCoroutine(Movement());
                }
                break;
        }
    }
    
    void Awake()
    {
        info.age = Random.Range(15, 100); //Add a random age to the villager
        StartCoroutine(Movement()); //Begins the coroutine that create the zombie movement.
        info.zGusto = (Taste)Random.Range(0, 5); //Selecting a body part to eat (Randomly)
        info.val = Random.Range(1, 3);
        info.reactionSpeed = (100f / info.age) * Time.deltaTime; //Speed for reacting mode
    }

    void Update()
    {
        switch (info.type) //Define the differents directions that the zombie can move.
        {
            case 1:
                transform.position += transform.forward * 3f * Time.deltaTime;
                break;
            case 2:
                transform.position -= transform.forward * 3f * Time.deltaTime;
                break;
            case 3:
                transform.position += transform.right * 3f * Time.deltaTime;
                break;
            case 4:
                transform.position -= transform.right * 3f * Time.deltaTime;
                break;
            case 5:
                transform.position += new Vector3(0, 0, 0); //Stop the zombies movement
                break;
            case 6:  //Sets the rotating direction
                if (info.val == 1)
                {
                    info.rotSpeed += info.reactionSpeed * Time.deltaTime;
                    transform.rotation = Quaternion.Euler(0, info.rotSpeed, 0);
                }
                if (info.val == 2)
                {
                    info.rotSpeed -= info.reactionSpeed * Time.deltaTime;
                    transform.rotation = Quaternion.Euler(0, info.rotSpeed, 0);
                }
                break;
        }
        
        foreach(GameObject obj in Global.listOfNpc) //searching in all the gameObjects inside the list
        {
            if(obj.GetComponent<Hero>() || obj.GetComponent<Villager>()) //those who are Villager and hero
            {
                float distance = Vector3.Distance(obj.transform.position, transform.position); //Sets the distance between the object and itself.
                if(distance <= 5f) 
                {
                    isReacting = true; //Actives reacting mode
                    React(); //Calls the method
                    StopCoroutine(Movement()); // Stops the coroutine
                    info.zBehavior = Behavior.React; //Get acces to reacting case
                }
                else
                {
                    isReacting = false;
                }
            }
        }
    }

    public Parameters Information()
    {
        return info;
    }

    public IEnumerator Movement() //Start every "X" seconds the movement of the zombie
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            info.zBehavior = (Behavior)Random.Range(0, 3); //Randomly selects if it's moving, rotating or idleing.
            info.val = Random.Range(1, 3);
            StartMoving(); //calls the method.
            yield return new WaitForSeconds(3);
        }
    }

    public virtual void React() //Virtual function for react method
    {
        foreach(GameObject obj in Global.listOfNpc)
        {
            float distance = Vector3.Distance(obj.transform.position, transform.position);
            if(distance <= 5f)
            {

            }
        }
    }
}

public struct Parameters   //This struct pile up the information of the zombie's behavior.
{
    public Taste zGusto; //Calls an enum that set a body part that the zombie could eat.
    public Behavior zBehavior; //Calls an enum that set a behavior to our zombies.
    public float speed; //Npc Speed
    public int type; //Used for switch
    public int val;
    public float rotSpeed;
    public int age; //Give the information of the NPC's age
    public float reactionSpeed;
}

public enum Behavior //Gives a behaviour to our zombies
{
  Idle,
  Moving,
  Rotating,
  React
}