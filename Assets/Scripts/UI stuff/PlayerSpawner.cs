using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private GameObject PlayerTracker;
    private Transform Spawn1;
    private Transform Spawn2;
    private Transform Spawn3;
    private Transform Spawn4;
    //--------------------------------------------------------------------------------------------------
    public GameObject MysteriousStranger1;
    public GameObject MysteriousStranger2;
    public GameObject MysteriousStranger3;
    public GameObject MysteriousStranger4;
    //--------------------------------------------------------------------------------------------------
    public GameObject BabyBeard1;
    public GameObject BabyBeard2;
    public GameObject BabyBeard3;
    public GameObject BabyBeard4;

    //-----------------------------------------------------------------------------
    public GameObject STUNMysteriousStranger1;
    public GameObject STUNMysteriousStranger2;
    public GameObject STUNMysteriousStranger3;
    public GameObject STUNMysteriousStranger4;
    //--------------------------------------------------------------------------------------------------
    public GameObject STUNBabyBeard1;
    public GameObject STUNBabyBeard2;
    public GameObject STUNBabyBeard3;
    public GameObject STUNBabyBeard4;

    void Start()
    {
        Spawn1 = GameObject.Find("Respawn").transform;
        Spawn2 = GameObject.Find("Respawn2").transform;
        Spawn3 = GameObject.Find("Respawn3").transform;
        Spawn4 = GameObject.Find("Respawn4").transform;
        PlayerTracker = GameObject.Find("PlayerTrackerThing");

        //Player1 Stuff ---------------------------------------------------------------------------------------

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
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player1 == "MysteriousStranger")
        {
            Instantiate(MysteriousStranger1, new Vector3(Spawn1.position.x, Spawn1.position.y, 0), this.transform.rotation);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player1 == "BabyBeard")
        {
            Instantiate(BabyBeard1, new Vector3(Spawn1.position.x, Spawn1.position.y, 0), this.transform.rotation);
        }

        //Player 2 Stuff --------------------------------------------------------------------------------------------------

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
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player2 == "MysteriousStranger")
        {
            Instantiate(MysteriousStranger2, new Vector3(Spawn2.position.x, Spawn2.position.y, 0), this.transform.rotation);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player2 == "BabyBeard")
        {
            Instantiate(BabyBeard2, new Vector3(Spawn2.position.x, Spawn2.position.y, 0), this.transform.rotation);
        }

        //Player 3 Stuff --------------------------------------------------------------------------------------------------

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
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player3 == "MysteriousStranger")
        {
            Instantiate(MysteriousStranger3, new Vector3(Spawn3.position.x, Spawn3.position.y, 0), this.transform.rotation);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player3 == "BabyBeard")
        {
            Instantiate(BabyBeard3, new Vector3(Spawn3.position.x, Spawn3.position.y, 0), this.transform.rotation);
        }

        //Player 4 Stuff --------------------------------------------------------------------------------------------------

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
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player4 == "MysteriousStranger")
        {
            Instantiate(MysteriousStranger4, new Vector3(Spawn4.position.x, Spawn4.position.y, 0), this.transform.rotation);
        }
        else if (PlayerTracker.GetComponent<PlayerTracker>().Player4 == "BabyBeard")
        {
            Instantiate(BabyBeard4, new Vector3(Spawn4.position.x, Spawn4.position.y, 0), this.transform.rotation);
        }
    }

}
