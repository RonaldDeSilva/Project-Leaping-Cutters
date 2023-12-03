using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject PlayerTracker;
    private Transform Spawn1;
    private Transform Spawn2;
    private Transform Spawn3;
    private Transform Spawn4;
    //--------------------------------------------------------------------------------------------------
    public GameObject MysteriousStranger;
    //--------------------------------------------------------------------------------------------------
    public GameObject BabyBeard;

    public GameObject WizGuy;

    public GameObject ScubaSteve;

    //-----------------------------------------------------------------------------
    /*
    public GameObject STUNMysteriousStranger1;
    public GameObject STUNMysteriousStranger2;
    public GameObject STUNMysteriousStranger3;
    public GameObject STUNMysteriousStranger4;
    //--------------------------------------------------------------------------------------------------
    public GameObject STUNBabyBeard1;
    public GameObject STUNBabyBeard2;
    public GameObject STUNBabyBeard3;
    public GameObject STUNBabyBeard4;
    */

    void Start()
    {
        Spawn1 = GameObject.Find("Respawn").transform;
        Spawn2 = GameObject.Find("Respawn2").transform;
        Spawn3 = GameObject.Find("Respawn3").transform;
        Spawn4 = GameObject.Find("Respawn4").transform;
        PlayerTracker = GameObject.Find("PlayerTrackerThing");

        //Player1 Stuff ---------------------------------------------------------------------------------------

        /*
        if (PlayerTracker.GetComponent<PlayerTracker>().Stun)
        {
            if (PlayerTracker.GetComponent<PlayerTracker>().Player1 == "MysteriousStranger")
            {
                Instantiate(STUNMysteriousStranger1, new Vector3(Spawn1.position.x, Spawn1.position.y, 0), this.transform.rotation);
            }
            else if (PlayerTracker.GetComponent<PlayerTracker>().Player1 == "BabyBeard")
            {
                Instantiate(STUNBabyBeard1, new Vector3(Spawn1.position.x, Spawn1.position.y, 0), this.transform.rotation);
            }
        }
        else 
        */
        if (PlayerTracker.GetComponent<PlayerTracker>().Player1 == "MysteriousStranger")
        {
            GameObject p = Instantiate(MysteriousStranger, new Vector3(Spawn1.position.x, Spawn1.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(1);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player1 == "BabyBeard")
        {
            GameObject p = Instantiate(BabyBeard, new Vector3(Spawn1.position.x, Spawn1.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(1);
        } 
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player1 == "WizGuy")
        {
            GameObject p = Instantiate(WizGuy, new Vector3(Spawn1.position.x, Spawn1.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(1);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player1 == "ScubaSteve")
        {
            GameObject p = Instantiate(ScubaSteve, new Vector3(Spawn1.position.x, Spawn1.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(1);
        }

        //Player 2 Stuff --------------------------------------------------------------------------------------------------

        /*
        if (PlayerTracker.GetComponent<PlayerTracker>().Stun)
        {
            if (PlayerTracker.GetComponent<PlayerTracker>().Player2 == "MysteriousStranger")
            {
                Instantiate(STUNMysteriousStranger2, new Vector3(Spawn2.position.x, Spawn2.position.y, 0), this.transform.rotation);
            }
            else if (PlayerTracker.GetComponent<PlayerTracker>().Player2 == "BabyBeard")
            {
                Instantiate(STUNBabyBeard2, new Vector3(Spawn2.position.x, Spawn2.position.y, 0), this.transform.rotation);
            }
        }
        else 
        */
        if (PlayerTracker.GetComponent<PlayerTracker>().Player2 == "MysteriousStranger")
        {
            GameObject p = Instantiate(MysteriousStranger, new Vector3(Spawn2.position.x, Spawn2.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(2);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player2 == "BabyBeard")
        {
            GameObject p = Instantiate(BabyBeard, new Vector3(Spawn2.position.x, Spawn2.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(2);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player2 == "WizGuy")
        {
            GameObject p = Instantiate(WizGuy, new Vector3(Spawn2.position.x, Spawn2.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(2);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player2 == "ScubaSteve")
        {
            GameObject p = Instantiate(ScubaSteve, new Vector3(Spawn2.position.x, Spawn2.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(2);
        }

        //Player 3 Stuff --------------------------------------------------------------------------------------------------

        /*
        if (PlayerTracker.GetComponent<PlayerTracker>().Stun)
        {
            if (PlayerTracker.GetComponent<PlayerTracker>().Player3 == "MysteriousStranger")
            {
                Instantiate(STUNMysteriousStranger3, new Vector3(Spawn3.position.x, Spawn3.position.y, 0), this.transform.rotation);
            }
            else if (PlayerTracker.GetComponent<PlayerTracker>().Player3 == "BabyBeard")
            {
                Instantiate(STUNBabyBeard3, new Vector3(Spawn3.position.x, Spawn3.position.y, 0), this.transform.rotation);
            }
        }
        else 
        */
        if (PlayerTracker.GetComponent<PlayerTracker>().Player3 == "MysteriousStranger")
        {
            GameObject p = Instantiate(MysteriousStranger, new Vector3(Spawn3.position.x, Spawn3.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(3);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player3 == "BabyBeard")
        {
            GameObject p = Instantiate(BabyBeard, new Vector3(Spawn3.position.x, Spawn3.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(3);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player3 == "WizGuy")
        {
            GameObject p = Instantiate(WizGuy, new Vector3(Spawn3.position.x, Spawn3.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(3);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player3 == "ScubaSteve")
        {
            GameObject p = Instantiate(ScubaSteve, new Vector3(Spawn3.position.x, Spawn3.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(3);
        }

        //Player 4 Stuff --------------------------------------------------------------------------------------------------

        /*
        if (PlayerTracker.GetComponent<PlayerTracker>().Stun)
        {
            if (PlayerTracker.GetComponent<PlayerTracker>().Player4 == "MysteriousStranger")
            {
                Instantiate(STUNMysteriousStranger4, new Vector3(Spawn4.position.x, Spawn4.position.y, 0), this.transform.rotation);
            }
            else if (PlayerTracker.GetComponent<PlayerTracker>().Player4 == "BabyBeard")
            {
                Instantiate(STUNBabyBeard4, new Vector3(Spawn4.position.x, Spawn4.position.y, 0), this.transform.rotation);
            }
        }
        else 
        */
        if (PlayerTracker.GetComponent<PlayerTracker>().Player4 == "MysteriousStranger")
        {
            GameObject p = Instantiate(MysteriousStranger, new Vector3(Spawn4.position.x, Spawn4.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(4);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player4 == "BabyBeard")
        {
            GameObject p = Instantiate(BabyBeard, new Vector3(Spawn4.position.x, Spawn4.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(4);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player4 == "WizGuy")
        {
            GameObject p = Instantiate(WizGuy, new Vector3(Spawn4.position.x, Spawn4.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(4);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player4 == "ScubaSteve")
        {
            GameObject p = Instantiate(ScubaSteve, new Vector3(Spawn4.position.x, Spawn4.position.y, 0), this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(4);
        }
    }

}
