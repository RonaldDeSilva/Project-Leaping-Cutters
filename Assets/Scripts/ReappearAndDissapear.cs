using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReappearAndDissapear : MonoBehaviour
{
    public float Timer;
    private Collider2D col;
    private SpriteRenderer sprite;
    void Start()
    {
        col = this.gameObject.GetComponent<Collider2D>();
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine("Death");
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(Timer);
        col.enabled = false;
        sprite.enabled = false;
        yield return new WaitForSeconds(Timer);
        col.enabled = true;
        sprite.enabled = true;
        StartCoroutine("Death");
    }
}
