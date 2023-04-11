using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTracker : MonoBehaviour
{
    public string Player1;
    public string Player2;
    public string Player3;
    public string Player4;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
