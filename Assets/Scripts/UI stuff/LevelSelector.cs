using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject PlayerTracker;
    private Canvas Can;
    public GameObject AudioPlayer;
    public AudioClip WinSound;
    private bool menu = false;
    private GameObject Countdown;
    private GameObject Title;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        #region Character Select
        if (SceneManager.GetActiveScene().name == "CharacterSelect")
        {
            Countdown = GameObject.Find("CountDown");
            if (PlayerTracker.GetComponent<PlayerTracker>().numPlayers == 4)
            {
                if (PlayerTracker.GetComponent<PlayerTracker>().Player1 != " " &&
                    PlayerTracker.GetComponent<PlayerTracker>().Player2 != " " &&
                    PlayerTracker.GetComponent<PlayerTracker>().Player3 != " " && PlayerTracker.GetComponent<PlayerTracker>().Player4 != " ")
                {
                    if (!Countdown.GetComponent<Countdown>().Counting)
                    {
                        Countdown.GetComponent<Countdown>().StartCoroutine("CountingDown");
                    }

                    if (Countdown.GetComponent<Countdown>().Counted == true)
                    {
                        SceneManager.LoadScene("LevelSelect");
                    }
                }
                else
                {
                    Countdown.GetComponent<Countdown>().StopAllCoroutines();
                    Countdown.GetComponent<Countdown>().Counting = false;
                }
            } 
            else if (PlayerTracker.GetComponent<PlayerTracker>().numPlayers == 3)
            {
                if (PlayerTracker.GetComponent<PlayerTracker>().Player1 != " " &&
                    PlayerTracker.GetComponent<PlayerTracker>().Player2 != " " &&
                    PlayerTracker.GetComponent<PlayerTracker>().Player3 != " ")
                {
                    if (!Countdown.GetComponent<Countdown>().Counting)
                    {
                        Countdown.GetComponent<Countdown>().StartCoroutine("CountingDown");
                    }

                    if (Countdown.GetComponent<Countdown>().Counted == true)
                    {
                        SceneManager.LoadScene("LevelSelect");
                    }
                }
                else
                {
                    Countdown.GetComponent<Countdown>().StopAllCoroutines();
                    Countdown.GetComponent<Countdown>().Counting = false;
                }
            }
            else if (PlayerTracker.GetComponent<PlayerTracker>().numPlayers == 2)
            {
                if (PlayerTracker.GetComponent<PlayerTracker>().Player1 != " " &&
                    PlayerTracker.GetComponent<PlayerTracker>().Player2 != " ")
                {
                    if (!Countdown.GetComponent<Countdown>().Counting)
                    {
                        Countdown.GetComponent<Countdown>().StartCoroutine("CountingDown");
                    }

                    if (Countdown.GetComponent<Countdown>().Counted == true)
                    {
                        SceneManager.LoadScene("LevelSelect");
                    }
                }
                else
                {
                    Countdown.GetComponent<Countdown>().StopAllCoroutines();
                    Countdown.GetComponent<Countdown>().Counting = false;
                }
            }
        }
        #endregion

        #region Level Select
        else if (SceneManager.GetActiveScene().name == "LevelSelect")
        {
            var Selector = GameObject.Find("Selector");
            Countdown = GameObject.Find("CountDown");
            if (Selector.GetComponent<SelectorScript>().collided != "")
            {
                if (!Countdown.GetComponent<Countdown>().Counting)
                {
                    Countdown.GetComponent<Countdown>().StartCoroutine("CountingDown");
                }
                if (Countdown.GetComponent<Countdown>().Counted == true)
                {
                    if (Selector.GetComponent<SelectorScript>().collided == "ThePit")
                    {
                        SceneManager.LoadScene("ThePit-4Player");
                    }
                    else if (Selector.GetComponent<SelectorScript>().collided == "BabyBeardsShip")
                    {
                        SceneManager.LoadScene("BabyBeards_Ship");
                    }
                    else if (Selector.GetComponent<SelectorScript>().collided == "Peak")
                    {
                        SceneManager.LoadScene("Peak");
                    }
                }
            }
            else
            {
                Countdown.GetComponent<Countdown>().StopAllCoroutines();
                Countdown.GetComponent<Countdown>().Counting = false;
            }
        }
        #endregion

        #region Start Screen
        else if (SceneManager.GetActiveScene().name == "StartScreen" || SceneManager.GetActiveScene().name == "StartScreen1")
        {
            Title = GameObject.Find("Title Selector");
            if (Input.GetAxis("Dash") > 0 || Input.GetAxis("Dash2") > 0 || Input.GetAxis("Dash3") > 0 || Input.GetAxis("Dash4") > 0)
            {
                if (Title.GetComponent<TitleSelector>().CurrentSpot == "Start")
                {
                    SceneManager.LoadScene("CharacterSelect");
                }
                else if (Title.GetComponent<TitleSelector>().CurrentSpot == "Settings")
                {
                    SceneManager.LoadScene("Settings");
                }
                else if (Title.GetComponent<TitleSelector>().CurrentSpot == "Quit")
                {
                    Application.Quit();
                }
            }
        }
        #endregion

        #region Settings
        else if (SceneManager.GetActiveScene().name == "Settings")
        {
            
        }
        #endregion

        #region In A Level
        else
        {
            if (Can == null)
            {
                Can = GameObject.Find("Canvas").GetComponent<Canvas>();
            }

            if (PlayerTracker.GetComponent<PlayerTracker>().numPlayers == 4)
            {
                var dead = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (int.Parse(Can.gameObject.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text) <= 0)
                    {
                        dead += 1;
                    }
                }

                if (dead >= 3 && menu == false)
                {
                    StartCoroutine("Menu");
                    menu = true;
                }
            }
            else if (PlayerTracker.GetComponent<PlayerTracker>().numPlayers == 3)
            {
                var dead = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (int.Parse(Can.gameObject.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text) <= 0)
                    {
                        dead += 1;
                    }
                }

                if (dead >= 2 && menu == false)
                {
                    StartCoroutine("Menu");
                    menu = true;
                }
            }
            else if (PlayerTracker.GetComponent<PlayerTracker>().numPlayers == 2)
            {
                var dead = 0;
                for (int i = 0; i < 2; i++)
                {
                    if (int.Parse(Can.gameObject.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Text>().text) <= 0)
                    {
                        dead += 1;
                    }
                }

                if (dead >= 1 && menu == false)
                {
                    StartCoroutine("Menu");
                    menu = true;
                }
            }
        }
        #endregion
    }

    IEnumerator Menu()
    {
        PlayerTracker.GetComponent<PlayerTracker>().Player1 = " ";
        PlayerTracker.GetComponent<PlayerTracker>().Player2 = " ";
        PlayerTracker.GetComponent<PlayerTracker>().Player3 = " ";
        PlayerTracker.GetComponent<PlayerTracker>().Player4 = " ";
        var sound = Instantiate(AudioPlayer);
        sound.GetComponent<SoundPlayer>().Awaken(WinSound, 1f);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("CharacterSelect");
        menu = false;
    }
}
