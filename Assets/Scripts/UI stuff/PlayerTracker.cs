using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public string Player1 = " ";
    public string Player2 = " ";
    public string Player3 = " ";
    public string Player4 = " ";
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Player1 = " ";
        Player2 = " ";
        Player3 = " ";
        Player4 = " ";
    }
}
