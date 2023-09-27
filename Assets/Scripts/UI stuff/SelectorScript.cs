using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectorScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public string collided;
    private SpriteRenderer sr;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine("Colors");
    }
    void FixedUpdate()
    {
        var hor = Input.GetAxis("HorizontalAll");
        var ver = Input.GetAxis("VerticalAll");
        rb.velocity = new Vector2(hor * 3f, -ver * 3f);
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collided = collision.gameObject.tag;
    }

    IEnumerator Colors()
    {
        var randR = Random.Range(0f, 255f);
        var randG = Random.Range(0f, 255f);
        var randB = Random.Range(0f, 255f);
        var col = new Color(randR, randG, randB, 1f);
        sr.color = col;
        yield return new WaitForSeconds(2f);
        StartCoroutine("Colors");
    }
    
}
