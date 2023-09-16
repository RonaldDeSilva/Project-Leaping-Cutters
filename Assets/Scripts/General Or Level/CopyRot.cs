using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRot : MonoBehaviour
{
    private Transform thing;
    public bool Off;

    void Start()
    {
        thing = transform.parent.GetChild(0);
    }

    void Update()
    {
        if (!Off)
        {
            transform.rotation = thing.transform.rotation;
        }
    }

}
