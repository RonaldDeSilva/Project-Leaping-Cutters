using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproximatelyFunction : MonoBehaviour
{
    //This is an approximately function that allows a threshold which I needed for the project
    //so that the angles can line up better when turning the arm around the player.
    public static bool FastApproximately(float a, float b, float threshold)
    {
        if (threshold > 0f)
        {
            return Mathf.Abs(a - b) <= threshold;
        }
        else
        {
            return Mathf.Approximately(a, b);
        }
    }
}
