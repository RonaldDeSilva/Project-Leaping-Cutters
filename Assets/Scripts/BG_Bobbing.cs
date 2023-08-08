using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Bobbing : MonoBehaviour
{
    private Rigidbody2D rb;
    private int dir;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("Up");
    }

    void Update()
    {
        rb.velocity = new Vector2(0,dir);
    }

    IEnumerator Up()
    {
        dir = 1;
        yield return new WaitForSeconds(1f);
        StartCoroutine("Down");
    }

    IEnumerator Down()
    {
        dir = -1;
        yield return new WaitForSeconds(1f);
        StartCoroutine("Up");
    }
}
