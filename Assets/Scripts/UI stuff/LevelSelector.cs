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

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "CharacterSelect")
        {

            if (PlayerTracker.GetComponent<PlayerTracker>().Player1 != " " &&
                PlayerTracker.GetComponent<PlayerTracker>().Player2 != " " &&
                PlayerTracker.GetComponent<PlayerTracker>().Player3 != " " && PlayerTracker.GetComponent<PlayerTracker>().Player4 != " ")
            {
                SceneManager.LoadScene("LevelSelect");
            }
        }
        else if (SceneManager.GetActiveScene().name == "LevelSelect")
        {
            var Selector = GameObject.Find("Selector");
            if (Selector.GetComponent<SelectorScript>().collided != null)
            {
                if (Selector.GetComponent<SelectorScript>().collided == "ThePit")
                {
                    SceneManager.LoadScene("ThePit-4Player");
                }
                else if (Selector.GetComponent<SelectorScript>().collided == "SpinningMoon")
                {
                    SceneManager.LoadScene("SpinningMoon");
                }
            }


        }
        else if (SceneManager.GetActiveScene().name == "StartScreen")
        {
            SceneManager.LoadScene("CharacterSelect");
        }
        else
        {
            if (Can == null)
            {
                Can = GameObject.Find("Canvas").GetComponent<Canvas>();
            }
            var dead = 0;
            int[] list = new int[4];
            for (int i = 0; i < 4; i++)
            {
                list[i] = int.Parse(Can.gameObject.transform.GetChild(i).gameObject.GetComponent<Text>().text);
                if (int.Parse(Can.gameObject.transform.GetChild(i).gameObject.GetComponent<Text>().text) < 1)
                {
                    dead += 1;
                }
            }

            if (dead >= 3)
            {
                StartCoroutine("Menu");
            }
        }
        
        
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
    }
}
