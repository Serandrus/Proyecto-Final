using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Ally;
namespace NPC
{
    namespace Enemy
    {
        /// <summary>
        /// This class sets all parameters to our Over Powered Zombie
        /// </summary>
        public class OPZombie : Npc
        {
            Global global;

            Color[] opzColor;

            public OPZombie()
            {
                info.speed = Initializer.speed;
            }

            private void Start()
            {
                opzColor = new Color[] { Color.red, Color.black, Color.blue };
                gameObject.GetComponent<Renderer>().material.color = opzColor[Random.Range(0, 3)];
                info.zGusto = (Taste)Random.Range(0, 5);
                info.val = Random.Range(1, 3);
                global = FindObjectOfType<Global>().GetComponent<Global>();
            }

            public override void React()
            {
                foreach (GameObject obj in Global.listOfNpc)
                {
                    if (obj.GetComponent<Hero>() || obj.GetComponent<Villager>())
                    {
                        float dist = Vector3.Distance(obj.transform.position, transform.position);
                        if (dist <= 5f)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, obj.transform.position, info.reactionSpeed);
                        }
                    }
                }
            }

            public Parameters OPZombieGusto()
            {
                return info;
            }

            private void OnCollisionEnter(Collision collision)
            {
                if (collision.gameObject.GetComponent<Villager>())
                {
                    Villager citizen = collision.gameObject.GetComponent<Villager>();
                    OPZombie zombie = (OPZombie)citizen;
                    print("From OPzombie " + zombie.info.age);
                    
                    global.villagerCount --; //Substract a villager
                    global.villagerText.text = "Villagers = " + global.villagerCount.ToString(); //Actualize the text
                    global.oPZombieCount += 1; // Add a zombie
                    global.oPZombieText.text = "OPZombies = " + global.oPZombieCount.ToString();
                    //Debug.Log(global.oPZombieCount);
                }
            }
        }
    }
}

