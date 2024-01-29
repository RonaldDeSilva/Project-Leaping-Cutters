using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTracker : MonoBehaviour
{
    public string Player1 = " ";
    public string Player2 = " ";
    public string Player3 = " ";
    public string Player4 = " ";

    private bool P3 = false;
    private bool P4 = false;
    private GameObject select3;
    private GameObject select4;
    private bool found;

    public int numPlayers = 2;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Player1 = " ";
        Player2 = " ";
        Player3 = " ";
        Player4 = " ";
        numPlayers = 2;
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "CharacterSelect")
        {
            if (numPlayers == 2)
            {
                
                if (!found)
                {
                    select3 = GameObject.Find("CharacterSelector3");
                    select4 = GameObject.Find("CharacterSelector4");
                    found = true;
                }

                if (!P3)
                {
                    select3.SetActive(false);
                    if (Input.GetAxis("Horizontal3") != 0 || Input.GetAxis("Vertical3") != 0)
                    {
                        P3 = true;
                        select3.SetActive(true);
                        numPlayers++;
                    }
                }

                if (!P4)
                {
                    select4.SetActive(false);
                    if (Input.GetAxis("Horizontal4") != 0 || Input.GetAxis("Vertical4") != 0)
                    {
                        P4 = true;
                        select4.SetActive(true);
                        numPlayers++;
                    }
                }
            }
            else if (numPlayers == 3)
            {
                if (!found)
                {
                    select4 = GameObject.Find("CharacterSelector4");
                    found = true;
                }

                if (!P4)
                {
                    select4.SetActive(false);
                    if (Input.GetAxis("Horizontal4") != 0 || Input.GetAxis("Vertical4") != 0)
                    {
                        P4 = true;
                        select4.SetActive(true);
                        numPlayers++;
                    }
                }
            }
        }
        else
        {
            found = false;
            P3 = false;
            P4 = false;
        }
    }

}
