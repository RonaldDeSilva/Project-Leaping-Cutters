using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // This script controls menus and also level management and stuff like that
    private bool paused = false;
    public GameObject PauseMenuButton1;
    public GameObject PauseMenuButton2;

    void Update()
    {
        if (paused)
        {
            if (Input.GetAxisRaw("Pause") > 0)
            {
                Resume();
            }
        }
        else
        {
            if (Input.GetAxisRaw("Pause") > 0)
            {
                Pause();
            }
        }

        
    }

    void Pause()
    {
        StartCoroutine("Wait1");
        PauseMenuButton1.SetActive(true);
        PauseMenuButton2.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        StartCoroutine("Wait2");
        PauseMenuButton1.SetActive(false);
        PauseMenuButton2.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator Wait1()
    {
        yield return new WaitForSecondsRealtime(1f);
        paused = true;
    }

    IEnumerator Wait2()
    {
        yield return new WaitForSecondsRealtime(1f);
        paused = false;
    }
}
