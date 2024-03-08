using UnityEngine;
using System.Collections;

public class TitleSelector : MonoBehaviour
{
    public Transform Start;
    public Transform Settings;
    public Transform Quit;

    public string CurrentSpot = "Start";
    private bool Up = false;
    private Color col;
    private bool Wait = false;

    private void Awake()
    {
        transform.position = Start.position;
        col = GetComponent<SpriteRenderer>().color;
    }

    void FixedUpdate()
    {
        if (Up)
        {
            col.a = col.a + 0.1f;
            if (col.a >= 1f)
            {
                Up = false;
            }
        }
        else if (!Up)
        {
            col.a = col.a - 0.1f;
            if (col.a <= 0f)
            {
                Up = true;
            }
        }
        GetComponent<SpriteRenderer>().color = col;

        if (!Wait)
        {
            if (Input.GetAxis("VerticalAll") < -0.3)
            {
                if (CurrentSpot == "Start")
                {
                    CurrentSpot = "Quit";
                    transform.position = Quit.position;
                    StartCoroutine("Cooldown");
                }
                else if (CurrentSpot == "Settings")
                {
                    CurrentSpot = "Start";
                    transform.position = Start.position;
                    StartCoroutine("Cooldown");
                }
                else if (CurrentSpot == "Quit")
                {
                    CurrentSpot = "Settings";
                    transform.position = Settings.position;
                    StartCoroutine("Cooldown");
                }
            }
            else if (Input.GetAxis("VerticalAll") > 0.3)
            {
                if (CurrentSpot == "Start")
                {
                    CurrentSpot = "Settings";
                    transform.position = Settings.position;
                    StartCoroutine("Cooldown");
                }
                else if (CurrentSpot == "Settings")
                {
                    CurrentSpot = "Quit";
                    transform.position = Quit.position;
                    StartCoroutine("Cooldown");
                }
                else if (CurrentSpot == "Quit")
                {
                    CurrentSpot = "Start";
                    transform.position = Start.position;
                    StartCoroutine("Cooldown");
                }
            }
        }
    }

    IEnumerator Cooldown()
    {
        Wait = true;
        yield return new WaitForSeconds(0.3f);
        Wait = false;
    }
}
