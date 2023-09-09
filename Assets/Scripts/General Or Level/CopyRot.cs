using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRot : MonoBehaviour
{
    private Transform thing;
    public bool reverse;
    private Transform rot;
    void Start()
    {
        thing = transform.parent.GetChild(0).transform.GetChild(1);
        rot = transform;
    }

    void Update()
    {
        //transform.LookAt(thing, );
        Vector3 diff = thing.position - rot.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
