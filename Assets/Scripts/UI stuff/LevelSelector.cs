using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public GameObject PlayerTracker;
    private Canvas Can;
    private bool cont = false;
    //private GameObject Selector;

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
                cont = true;
                SceneManager.LoadScene("LevelSelect");
            }
        }
        else
        {
            if (cont)
            {
                if (SceneManager.GetActiveScene().name == "ThePit-4Player" || SceneManager.GetActiveScene().name == "NoGround-4Player" || SceneManager.GetActiveScene().name == "Spinner" || SceneManager.GetActiveScene().name == "SpinningMoon")
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
                        cont = false;
                    }
                }
            }else if (SceneManager.GetActiveScene().name == "StartScreen")
            {
                SceneManager.LoadScene("CharacterSelect");
            }
        }
        /*
        else if (SceneManager.GetActiveScene().name == "LevelSelect")
        {
            Selector = GameObject.Find("Selector");
            var contact = new ContactFilter2D();
            var list = new Collider2D[10];
            var x = Selector.GetComponent<CircleCollider2D>().OverlapCollider(contact, list);
            for (int i = 0; i < 9; i++)
            {
                if (list[i].gameObject != null)
                {
                    if (list[i].gameObject.CompareTag("ThePit"))
                    {
                        SceneManager.LoadScene("ThePit-4Player");
                    }
                    else if (list[i].gameObject.CompareTag("NoGround"))
                    {
                        SceneManager.LoadScene("NoGround-4Player");
                    }
                }
            }
        }
        */
    }

    IEnumerator Menu()
    {
        PlayerTracker.GetComponent<PlayerTracker>().Player1 = " ";
        PlayerTracker.GetComponent<PlayerTracker>().Player2 = " ";
        PlayerTracker.GetComponent<PlayerTracker>().Player3 = " ";
        PlayerTracker.GetComponent<PlayerTracker>().Player4 = " ";

        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("CharacterSelect");
    }
}
