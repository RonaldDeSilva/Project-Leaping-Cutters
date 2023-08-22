using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Bobbing : MonoBehaviour
{
    private float dir;
    void Start()
    {
        StartCoroutine("Up");
    }

    void Update()
    {
        transform.Translate(new Vector3(0, dir * Time.deltaTime));
    }

    IEnumerator Up()
    {
        dir = 0.95f;
        yield return new WaitForSeconds(0.5f);
        dir = 0.45f;
        yield return new WaitForSeconds(0.35f);
        dir = 0.2f;
        yield return new WaitForSeconds(0.15f);
        StartCoroutine("Down");
    }

    IEnumerator Down()
    {
        dir = -0.95f;
        yield return new WaitForSeconds(0.5f);
        dir = -0.45f;
        yield return new WaitForSeconds(0.35f);
        dir = -0.2f;
        yield return new WaitForSeconds(0.15f);
        StartCoroutine("Up");
    }
}
