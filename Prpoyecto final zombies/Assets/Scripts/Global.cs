using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NPC.Ally;
using NPC.Enemy;
/// <summary>
/// This claas is used to manage all the process to create all the elements in the scene.
/// </summary>
public class Global : MonoBehaviour
{
    GameObject obj;
    new GameObject camera;
    public GameObject[] pjs = new GameObject[4];
    public Image[] hearths = new Image[3];
    public Sprite fullHearth, emptyHearth;
    public GameObject gameOverText; //Game over text
    public GameObject youWinText; //You win text.
    public GameObject conversationPanel, conversationPanelZombie, conversationPanelOPZombie; //Villager conversation panel
    public Text conversationText, conversationTextZombie, conversationTextOPZombie; //NPC text (Conversation)
    public Text villagerText, zombieText, oPZombieText;  //Text that shows the cuantity of npcs
    public Text ammoText; //Shows the cuantity of ammo.
    public Slider healthBar;
    public GameObject exitButton;
    public int villagerCount = 0; // Pile up the cuantity of villagers
    public int zombieCount = 0; // Pile up the cuantity of zombies
    public int oPZombieCount = 0;
    public int ammo = 15;
    public int totalAmmo = 15;
    int type = -1;
    const int SPAWN = 25; // constant that define the maximum cuantity of cubes spawned

    public static List<GameObject> listOfNpc = new List<GameObject>(); //List of gameobjects

    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");  //Search and find the camera tag.
        conversationPanel.SetActive(false);
        conversationPanelOPZombie.SetActive(false);
        conversationPanelZombie.SetActive(false);
        conversationPanelOPZombie.SetActive(false);
        gameOverText.SetActive(false);
        youWinText.SetActive(false);
        exitButton.SetActive(false);
        

        for (int i = 0; i < hearths.Length; i++)
        {
            hearths[i].overrideSprite = fullHearth;
        }

        for (int i = 0; i < Random.Range(new Initializer().spawning, SPAWN); i++)  //Defines the number of possible instances.
        {
            Vector3 pos = new Vector3(Random.Range(-20, 20), 0.5f, Random.Range(-10, 10)); //Setting position of the Characters.
            //obj = Instantiate(pjs[Random.Range(1, 4)], pos, Quaternion.identity);
            //obj.transform.position = pos;

            switch (type) //Defines the type of character in the scene.
            {
                case 1: //Zombie type
                    obj = Instantiate(pjs[1], pos, Quaternion.identity);
                    obj.AddComponent<Zombie>(); //Ads the class components
                    obj.name = "Zombie"; //Give a name to the zombie's primitive at the inspector.
                    obj.tag = "Zombie"; //Gives a tag to our zombies at the inspector.
                    break;
                case 2: //Villager type
                    obj = Instantiate(pjs[2], pos, Quaternion.identity);
                    obj.AddComponent<Villager>();
                    obj.name = "Villager";
                    obj.tag = "Villager";
                    break;
                case 3:
                    obj = Instantiate(pjs[3], pos, Quaternion.identity);
                    obj.AddComponent<OPZombie>();
                    obj.name = "OPZombie";
                    obj.tag = "OPZombie";
                    break;
                default: // Hero type
                    obj = Instantiate(pjs[0], pos, Quaternion.identity);
                    obj.AddComponent<Hero>().Init(camera);
                    obj.name = "Hero";
                    obj.tag = "Hero";
                    break;
            }

            type = Random.Range(1, 4); //Chance the case

            if (obj.name != null) //Condition that adds gameobjects to the list
            {
                listOfNpc.Add(obj);

                if (obj.name == "Villager") //add one to the cuantity of villagers
                {
                    villagerCount ++;
                }
                if(obj.name == "Zombie") //add one to the cuantity of zombies
                {
                    zombieCount ++;
                }
                if (obj.name == "OPZombie")
                {
                    oPZombieCount++;
                }
            }

            foreach (GameObject obj in listOfNpc)  //Print a text with the cuantity of villagers and zombies spawned
            {
                if (obj.name == "Villager")
                {
                    villagerText.text = "Villagers = " + villagerCount.ToString(); //Adding the cuantity of villager spawned
                }
                if (obj.name == "Zombie")
                {
                    zombieText.text = "Zombies = " + zombieCount.ToString(); // Adding the cuantity of zombies spawned
                }
                if (obj.name == "OPZombie")
                {
                    oPZombieText.text = "OPZombies = " + oPZombieCount.ToString();
                }
            }
        }
    }

    public void Valor(int index)
    {
        hearths[index].overrideSprite = emptyHearth;
    }
}
/// <summary>
/// This class is just for create and use a read only variable.
/// </summary>
public class Initializer
{
    public static float speed;
    public readonly int spawning;  

    public Initializer()
    {
        spawning = Random.Range(5, 15);  //Sets the minimum cuantity of cubes spawned.
        speed = Random.Range(3, 10); //Sets a random speed for our hero and zombies behavior.
    }
}