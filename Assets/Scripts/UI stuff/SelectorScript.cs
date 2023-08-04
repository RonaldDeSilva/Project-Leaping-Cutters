using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public string collided;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    
}
