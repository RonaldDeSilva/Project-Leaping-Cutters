using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Projectile script which makes the projectiles fly through the air in the proper direction
    //and speed and also delete after hitting an object

    private Vector2 Dir;
    private Rigidbody2D rb;
    public void Awaken(Vector2 Direction)
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        Dir = Direction;
    }
    void FixedUpdate()
    {
        rb.velocity = Dir;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine("Destroy");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deathbox"))
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

}
