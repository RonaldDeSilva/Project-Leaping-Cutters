using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public bool Counted = false;
    public bool Counting = false;

    void Update()
    {
        if (!Counting)
        {
            GetComponent<Text>().text = "";
        }
    }

    IEnumerator CountingDown()
    {
        Counting = true;
        GetComponent<Text>().text = "3";
        yield return new WaitForSeconds(1f);
        GetComponent<Text>().text = "2";
        yield return new WaitForSeconds(1f);
        GetComponent<Text>().text = "1";
        yield return new WaitForSeconds(1f);
        Counted = true;
    }
}
