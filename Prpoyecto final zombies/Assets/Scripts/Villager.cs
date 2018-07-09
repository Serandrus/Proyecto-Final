using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPC.Enemy;

namespace NPC
{
    namespace Ally
    {
        /// <summary>
        /// This class sets all the parameters to our villagers
        /// </summary>
        public class Villager : Npc
        {
            public Citizen citizen; //Calling the struct

            public Villager()
            {
                info.speed = Initializer.speed;
            }

            void Start()
            {
                citizen.villagerName = (VillagerName)Random.Range(0, 20); //Add a random name to the village
                info.val = Random.Range(1, 3);
            }

            public Citizen CitizenName() //Get acces to the struct 
            {
                return citizen; //returning struct
            }

            public override void React() //Override method that shows how to react (Villager case)
            {
                foreach (GameObject obj in Global.listOfNpc)
                {
                    if (obj.GetComponent<Zombie>())
                    {
                        float distance = Vector3.Distance(obj.transform.position, transform.position);
                        if (distance <= 5f)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, obj.transform.position, -info.reactionSpeed); //Runs away fron the zombie
                        }
                    }
                }
            }

            public static implicit operator Zombie(Villager v) //Casting
            {
                Zombie z = v.gameObject.AddComponent<Zombie>(); //Add the zombie component
                print(z.info.age);
                z.info.age = v.info.age; //Giving the villager age to the new zombie.
                print(z.info.age);
                z.name = "Zombie"; //Changing the villager name at the inspector
                Destroy(v); //Destroying Villager component
                return z; //Returning Zombie.
            }

            public static implicit operator OPZombie(Villager villager)
            {
                OPZombie zombie = villager.gameObject.AddComponent<OPZombie>();
                print(zombie.info.age);
                zombie.info.age = villager.info.age; //Giving the villager age to the new zombie.
                print(zombie.info.age);
                zombie.name = "Zombie"; //Changing the villager name at the inspector
                Destroy(villager); //Destroying Villager component
                return zombie;
            }
        }
    }
}

public struct Citizen //This struct pile up the information of the villager.
{
    public VillagerName villagerName; //Calls the enum
}

public enum VillagerName //This enum contains some names for villagers
{
    Mario,
    Naked,
    Martin_Garrix,
    Charile_Foxtrot,
    Inquisitor,
    Master_Chief,
    MB14,
    Grunt,
    Link,
    WEED,
    Masterundead,
    Nie_Li,
    Oneg,
    Pada_Calentina,
    Poma_Rosa,
    Brimstone,
    Alexelcapo,
    Saro,
    D_Low,
    Napom
}