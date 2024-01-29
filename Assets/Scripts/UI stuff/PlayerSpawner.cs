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
    public GameObject BabyBeard;
    public GameObject WizGuy;
    public GameObject ScubaSteve;


    void Start()
    {
        Spawn1 = GameObject.Find("Respawn").transform;
        Spawn2 = GameObject.Find("Respawn2").transform;
        Spawn3 = GameObject.Find("Respawn3").transform;
        Spawn4 = GameObject.Find("Respawn4").transform;
        PlayerTracker = GameObject.Find("PlayerTrackerThing");

        #region 4 Players

        if (PlayerTracker.GetComponent<PlayerTracker>().numPlayers == 4)
        {
            //Player1 Stuff ---------------------------------------------------------------------------------------

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
        #endregion
        #region 3 Players
        else if (PlayerTracker.GetComponent<PlayerTracker>().numPlayers == 3)
        {
            //Player1 Stuff ---------------------------------------------------------------------------------------

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
        }
        #endregion
        #region 2 Players
        else if (PlayerTracker.GetComponent<PlayerTracker>().numPlayers == 2)
        {
            //Player1 Stuff ---------------------------------------------------------------------------------------

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
        }
        #endregion
    }

}
