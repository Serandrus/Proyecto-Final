using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using NPC.Ally;
using NPC.Enemy;
/// <summary>
/// This clas Sets all the parameters to our Hero
/// </summary>
public class Hero : MonoBehaviour
{
    public Global global; //Calling global 
    public Citizen citizen; //Calling villager's struct
    Parameters info; //Calling zombie's struct
    bool isActive = false;
    float timer = 2f;
    float distance;
    readonly float velocity;
    bool isDead = false;
    public int lives = 0;

    void Start()
    {
        global = FindObjectOfType<Global>().GetComponent<Global>(); //Gives the global component.
    }

    public Hero()  //Sets a random speed
    {
        velocity = Initializer.speed;
    }

    public void Init(GameObject camera) //Gives parameters to our hero.
    {
        gameObject.AddComponent<FPSAim>(); //Add an script that contains the movement for the camera.
        gameObject.AddComponent<FPSMove>(); //Add an script that contain the movement of the hero.
        camera.AddComponent<FPSAim>(); //Add an script that contains the movement for the camera.
        gameObject.AddComponent<Rigidbody>().freezeRotation = enabled; //Gives a rigidbody to our hero and freeze his rotation.
        camera.transform.SetParent(gameObject.transform); //Set parent the cameta to the hero's transform.
        gameObject.transform.position = new Vector3(0, 1, 0); //Set a position to the camera
        camera.transform.position = new Vector3(0.1f, 2.0f, -3.27f); //Set a position to the camera.
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow); //Gives a color to our hero.
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Villager>()) //Search and find if the gameObject get contact with villager.
        {
            timer = 2f;
            isActive = true;
            global.conversationPanel.SetActive(true);
            citizen = collision.gameObject.GetComponent<Villager>().CitizenName(); //Get acces to the villager's names enum
            info = collision.gameObject.GetComponent<Npc>().Information(); //Get acces to the Npc information.
            global.conversationText.text = "Hi I'm " + citizen.villagerName + " and I'm " + info.age + " years old."; //Villager message
        }

        if (collision.gameObject.GetComponent<Zombie>()) 
        {
            global.healthBar.value -= 15f;

            if(global.healthBar.value <= 0 && lives < global.hearths.Length)
            {
                global.Valor(lives);
                lives = lives + 1;
                global.healthBar.value = 100f;

                if (lives > 2)
                {
                    isDead = true;
                    global.gameOverText.SetActive(true); //Shows the game over text
                    gameObject.GetComponent<FPSAim>().enabled = false; //Unable the component
                    Camera.main.gameObject.GetComponent<FPSAim>().enabled = false;
                    global.healthBar.value = 0.0f;
                }

            }

        }

        if (collision.gameObject.GetComponent<OPZombie>())
        {
            global.healthBar.value -= 30f;

            if (global.healthBar.value <= 0 && lives < global.hearths.Length)
            {
                global.Valor(lives);
                lives = lives + 1;
                global.healthBar.value = 100f;

                if (lives > 2)
                {
                    isDead = true;
                    global.gameOverText.SetActive(true); //Shows the game over text
                    gameObject.GetComponent<FPSAim>().enabled = false; //Unable the component
                    Camera.main.gameObject.GetComponent<FPSAim>().enabled = false;
                    global.healthBar.value = 0.0f;
                }

            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SupportItem>())
        {
            if (global.healthBar.value < 100f || global.ammo < global.totalAmmo)
            {
                global.healthBar.value += 30f;
                global.ammo = global.totalAmmo;
                global.ammoText.text = "15/" + global.ammo.ToString();
            }
        }
    }

    void Update()
    {
        global.conversationPanelZombie.GetComponent<Transform>().LookAt(transform); //Zombie panel is always looking at player.
        
        if(isActive == true)
        {
            timer -= Time.deltaTime; //Timer (Spawn) of the conversation panel
        }
        if(timer < 0)
        {
            global.conversationPanelZombie.SetActive(false);
            global.conversationPanelOPZombie.SetActive(false);
            global.conversationPanel.SetActive(false);
        }

        foreach(GameObject obj in Global.listOfNpc) //Get all the gameobjects in the list
        {
            distance = Vector3.Distance(obj.transform.position, transform.position); 
            if (obj.GetComponent<Zombie>())
            {
                if(distance <= 5)
                {
                    timer = 2f;
                    isActive = true;
                    global.conversationPanelZombie.transform.SetParent(obj.gameObject.GetComponent<Zombie>().transform); //Setting parent
                    global.conversationPanelZombie.transform.localPosition = obj.GetComponent<Zombie>().transform.up;
                    info = obj.GetComponent<Zombie>().ZombieGusto(); //Get acces to the "Taste" enum.
                    global.conversationPanelZombie.SetActive(true); //Active the panel
                    global.conversationTextZombie.text = "Waaaaaaar I want to eat " + info.zGusto; //Show the zombie text
                }
                
            }

            if (obj.GetComponent<OPZombie>())
            {
                if (distance <= 5)
                {
                    timer = 2f;
                    isActive = true;
                    global.conversationPanelOPZombie.transform.SetParent(obj.gameObject.GetComponent<OPZombie>().transform);//Set a position to the panel
                    global.conversationPanelOPZombie.transform.localPosition = obj.GetComponent<OPZombie>().transform.up;
                    info = obj.GetComponent<OPZombie>().OPZombieGusto();
                    global.conversationPanelOPZombie.SetActive(true);
                    global.conversationTextOPZombie.text = "I'm Gonna eat your " + info.zGusto;
                }
            }

        }

        if (!isDead) //This condition ends the hero's movement is bool is true.
        {
            gameObject.GetComponent<FPSMove>().Move(velocity); //Calls and gets the movement component.
        }
    }
}
