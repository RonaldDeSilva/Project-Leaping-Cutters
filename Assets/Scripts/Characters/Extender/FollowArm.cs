using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowArm : MonoBehaviour
{
    public GameObject arm;
    private float MaxX = 0;
    private float MaxY = 0;

    private void Start()
    {
        StartCoroutine("test");
    }

    void Update()
    {
        /*
        if (!arm.transform.parent.GetComponent<Player1Extender>().Extending && !arm.transform.parent.GetComponent<Player1Extender>().Retracting)
        {
            transform.position = arm.transform.position;
        }
        */

        if (arm.transform.position.x > MaxX)
        {
            MaxX = arm.transform.position.x;
        }

        if (arm.transform.position.y > MaxY)
        {
            MaxY = arm.transform.position.y;
        }

    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("X: " + MaxX + " Y: " + MaxY);
    }
}
