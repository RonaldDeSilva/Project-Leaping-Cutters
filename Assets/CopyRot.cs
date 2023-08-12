using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRot : MonoBehaviour
{
    private GameObject thing;
    void Start()
    {
        thing = transform.parent.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = thing.transform.rotation;
    }
}
