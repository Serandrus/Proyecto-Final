using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Ally;

namespace NPC
{
    namespace Enemy
    {
        /// <summary>
        /// This clas Sets all the parameters to our Zombies
        /// </summary>
        public class Zombie : Npc
        {
            Global global;

            Color[] zColor; //Array that defines the zombie color.

            public Zombie() //Sets a random speed.
            {
                info.speed = Initializer.speed;
            }

            void Start()
            {
                zColor = new Color[] { Color.cyan, Color.magenta, Color.green }; //Array of colors
                gameObject.GetComponent<Renderer>().material.color = zColor[Random.Range(0, 3)]; //Set a random color
                info.zGusto = (Taste)Random.Range(0, 5); //Selecting a body part to eat (Randomly)
                info.val = Random.Range(1, 3);
                global = FindObjectOfType<Global>().GetComponent<Global>(); //Find the global component
            }

            public override void React() //Override method that shows how to react (Zombie case)
            {
                foreach (GameObject obj in Global.listOfNpc)
                {
                    if(obj.GetComponent<Hero>() || obj.GetComponent<Villager>())
                    {
                        float distance = Vector3.Distance(obj.transform.position, transform.position);
                        if (distance <= 5f)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, obj.transform.position, info.reactionSpeed); //Run toward the target (Hero or Villager)
                        }
                    }
                }
            }

            public Parameters ZombieGusto() //Gets the body part that the zombie wants to eat
            {
                return info; //returning the struct
            }

            private void OnCollisionEnter(Collision collision) //Giveing information to convert a Villager in a new Zombie
            {
                if (collision.gameObject.GetComponent<Villager>()) //If collides with it
                {
                    Villager c = collision.gameObject.GetComponent<Villager>(); //Getting the component
                    Zombie z = (Zombie)c; //Casting (Convert a villager in a zombie)
                    print("From zombie " + z.info.age); //Prints the new zombie age
                    global.villagerCount--; //Substract a villager
                    global.villagerText.text = "Villagers = " + global.villagerCount.ToString(); //Actualize the text
                    global.zombieCount++; // Add a zombie
                    global.zombieText.text = "Zombies = " + global.zombieCount.ToString();
                }
            }
        }
    }
}

public enum Taste //This enum gives to the ombie, body parts to eat.
{
    Brains,
    RightArm,
    LeftArm,
    rightLeg,
    LeftLeg
}

