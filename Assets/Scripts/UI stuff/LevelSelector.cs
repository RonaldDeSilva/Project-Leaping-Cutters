using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public GameObject PlayerTracker;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "CharacterSelect")
        {
            if (PlayerTracker.GetComponent<PlayerTracker>().Player1 != " " && PlayerTracker.GetComponent<PlayerTracker>().Player2 != " " && PlayerTracker.GetComponent<PlayerTracker>().Player3 != " " && PlayerTracker.GetComponent<PlayerTracker>().Player4 != " ")
            {
                SceneManager.LoadScene("ThePit-4Player");
            }
        }
    }
}
