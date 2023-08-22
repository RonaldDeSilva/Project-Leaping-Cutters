using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRot : MonoBehaviour
{
    private GameObject thing;
    void Start()
    {
        thing = transform.parent.GetChild(0).gameObject;
    }

    void Update()
    {
        transform.rotation = thing.transform.rotation;
    }
}
