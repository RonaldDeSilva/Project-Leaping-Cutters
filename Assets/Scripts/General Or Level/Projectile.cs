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


    //On Awaken the projectile is given a direction to fly in
    public void Awaken(Vector2 Direction)
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        Dir = Direction;
    }

    //Sends the projectile in the proper direction by setting its velocity
    void FixedUpdate()
    {
        if (flying)
        {
            rb.velocity = Dir;
        }
    }

    //This collision checker decides whether the projectile will keep flying or fall through the floor, based on whether it hits a player or the ground
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

        if (!collision.gameObject.CompareTag("Player") && Destroying)
        {
            StopAllCoroutines();
            StartCoroutine("Destroy2");
        }
    }

    //This Trigger exit check destroys bullets that are off screen to keep clutter in the level to a minimum and keep performance good
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deathbox"))
        {
            Destroy(this.gameObject);
        }
    }

    //This first coroutine is for hitting a player and keeps pushing the player for a specified amount of time
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(Pushtime);
        flying = false;
        if (this.gameObject.GetComponent<CapsuleCollider2D>() != null)
        {
            this.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
        else
        {
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    //This Coroutine is for hitting a wall and immediately makes the bullet fall instead of hitting the wall for a while
    IEnumerator Destroy2()
    {
        flying = false;
        if (this.gameObject.GetComponent<CapsuleCollider2D>() != null)
        {
            this.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
        else
        {
            this.gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        }
        yield return new WaitForSeconds(1f);
    }

}
