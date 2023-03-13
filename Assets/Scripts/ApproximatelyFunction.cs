using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproximatelyFunction : MonoBehaviour
{
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
