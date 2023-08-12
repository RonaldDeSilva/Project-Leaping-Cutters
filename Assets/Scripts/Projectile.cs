using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Projectile script which makes the projectiles fly through the air in the proper direction
    //and speed and also delete after hitting an object

    private Vector2 Dir;
    private Rigidbody2D rb;
    public float Pushtime;
    private bool flying = true;
    private bool Destroying = false;
    public float Life;

    public void Awaken(Vector2 Direction)
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        Dir = Direction;
    }
    void FixedUpdate()
    {
        if (flying)
        {
            rb.velocity = Dir;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!Destroying)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine("Destroy");
                Destroying = true;
            }
            else
            {
                StartCoroutine("Destroy2");
                Destroying = true;
            }
        }
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

        yield return new WaitForSeconds(Pushtime);
        flying = false;
        if (this.gameObject.GetComponent<CapsuleCollider2D>() != null)
        {
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        yield return new WaitForSeconds(Life);
        Destroy(this.gameObject);
    }

    IEnumerator Destroy2()
    {
        flying = false; 
        if (this.gameObject.GetComponent<CapsuleCollider2D>() != null)
        {
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
        yield return new WaitForSeconds(Life);
        Destroy(this.gameObject);
    }

}
