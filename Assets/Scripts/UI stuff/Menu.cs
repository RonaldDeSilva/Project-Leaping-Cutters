using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // This script controls menus and also level management and stuff like that
    private bool paused = false;
    public GameObject PauseMenuButton1;
    public GameObject PauseMenuButton2;
    public GameObject PauseMenuButton3;
    public GameObject PauseMenuButton4;

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
        PauseMenuButton3.SetActive(true);
        PauseMenuButton4.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        StartCoroutine("Wait2");
        PauseMenuButton1.SetActive(false);
        PauseMenuButton2.SetActive(false);
        PauseMenuButton3.SetActive(false);
        PauseMenuButton4.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
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
