using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterSpining : MonoBehaviour
{
    private Rigidbody2D rb;
    public float thing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.Rotate(new Vector3(0,0,1), thing * Time.deltaTime, Space.Self);
        rb.isKinematic = true;
    }
}
