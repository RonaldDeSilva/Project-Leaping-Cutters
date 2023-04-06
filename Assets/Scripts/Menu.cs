using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // This script controls menus and also level management and stuff like that
    private bool paused = false;

    void Update()
    {
        if (Input.GetKeyUp("Pause") && paused == false)
        {
            Pause();
        }
        
        if (Input.GetKeyUp("Pause") && paused == true)
        {
            Resume();
        }

    }

    void Pause()
    {
        paused = true;
        Time.timeScale = 0;
    }

    void Resume()
    {
        paused = false;
        Time.timeScale = 1;
    }
}
