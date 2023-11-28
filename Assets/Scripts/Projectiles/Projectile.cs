using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Projectile script which makes the projectiles fly through the air in the proper direction
    //and speed and also delete after hitting an object

    private Vector2 Dir;
    private Rigidbody2D rb;
    public float Impact;
    private bool flying = true;
    public GameObject AudioPlayer;
    public AudioClip hitWall;
    public AudioClip hitPlayer;
    public AudioClip hitWeapon;
    private GameObject Player;
    private bool Attached;
    private GameObject AttachedPlayer;
    private string SpecialButton;

    //Types of Proj
    public bool Bubble;
    private float buoyancy = 0f;
    public bool Orb;



    //On Awaken the projectile is given a direction to fly in
    public void Awaken(Vector2 Direction, GameObject Play)
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        Player = Play;
        Dir = Direction;
    }

    //Sends the projectile in the proper direction by setting its velocity
    void FixedUpdate()
    {
        if (flying)
        {
            if (Bubble)
            {
                if (Dir.x > 0)
                {
                    rb.velocity = new Vector2(Dir.x - Mathf.Clamp(buoyancy, 0, Dir.x), Dir.y + buoyancy);
                }
                else if (Dir.x < 0)
                {
                    rb.velocity = new Vector2(Dir.x + Mathf.Clamp(buoyancy, 0, -Dir.x), Dir.y + buoyancy);
                }
                else
                {
                    rb.velocity = new Vector2(Dir.x, Dir.y + buoyancy);
                }
                buoyancy = buoyancy + 0.1f;
                buoyancy = Mathf.Clamp(buoyancy, 0f, 10f);
                if (Attached)
                {
                    AttachedPlayer.GetComponent<Rigidbody2D>().velocity = rb.velocity;
                    transform.position = AttachedPlayer.transform.position;
                    if (Input.GetAxis(SpecialButton) > 0)
                    {
                        Destroy(this.gameObject);
                    }
                }
            } 
            else if (Orb)
            {
                var angle = ((Player.transform.localEulerAngles.z + 90) * Mathf.Deg2Rad);
                var newX = Mathf.Cos(angle);
                var newY = Mathf.Sin(angle);
                Dir = new Vector2(newX * Player.transform.parent.gameObject.GetComponent<ShooterScript>().ProjSpd, newY * Player.transform.parent.gameObject.GetComponent<ShooterScript>().ProjSpd);
                rb.velocity = new Vector2(Dir.x, Dir.y);
            }
            else
            {
                rb.velocity = Dir;
            }
            
        }
    }

    //This collision checker decides whether the projectile will keep flying or fall through the floor, based on whether it hits a player or the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Bubble)
        {
            if (!Attached)
            {
                if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Weapon") && collision.gameObject != Player)
                {
                    if (collision.gameObject.CompareTag("Player"))
                    {
                        var inst = Instantiate(AudioPlayer);
                        inst.GetComponent<SoundPlayer>().Awaken(hitPlayer, 1f);
                        GetComponent<CircleCollider2D>().isTrigger = true;
                        transform.position = collision.gameObject.transform.position;
                        AttachedPlayer = collision.gameObject;
                        var PlayerNum = AttachedPlayer.GetComponent<MovementBase>().playerNum;
                        if (PlayerNum == 1)
                        {
                            SpecialButton = "Special";
                        }
                        else if (PlayerNum == 2)
                        {
                            SpecialButton = "Special2";
                        }
                        else if (PlayerNum == 3)
                        {
                            SpecialButton = "Special3";
                        }
                        else if (PlayerNum == 4)
                        {
                            SpecialButton = "Special4";
                        }
                    }
                    else
                    {
                        var inst = Instantiate(AudioPlayer);
                        inst.GetComponent<SoundPlayer>().Awaken(hitWeapon, 1f);
                        GetComponent<CircleCollider2D>().isTrigger = true;
                        transform.position = collision.gameObject.transform.parent.position;
                        AttachedPlayer = collision.gameObject.transform.parent.gameObject;
                    }
                    StartCoroutine("BubbleDestroy");
                    Attached = true;
                }
                else
                {
                    if (collision.gameObject != Player)
                    {
                        var inst = Instantiate(AudioPlayer);
                        inst.GetComponent<SoundPlayer>().Awaken(hitWall, 1f);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Weapon") && collision.gameObject != Player)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    var inst = Instantiate(AudioPlayer);
                    inst.GetComponent<SoundPlayer>().Awaken(hitPlayer, 1f);
                }
                else
                {
                    var inst = Instantiate(AudioPlayer);
                    inst.GetComponent<SoundPlayer>().Awaken(hitWeapon, 1f);
                }
                StartCoroutine("Destroy2");
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Dir.x * Impact, Dir.y * Impact), ForceMode2D.Impulse);
            }
            else
            {
                if (collision.gameObject != Player)
                {
                    var inst = Instantiate(AudioPlayer);
                    inst.GetComponent<SoundPlayer>().Awaken(hitWall, 1f);
                    StartCoroutine("Destroy2");
                }
            }
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
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Bubble && AttachedPlayer != null)
        {
            if (!collision.gameObject.CompareTag("Deathbox") && !collision.gameObject.CompareTag("Platform")
                && collision.gameObject != AttachedPlayer
                && collision.gameObject != AttachedPlayer.transform.GetChild(0).gameObject
                && collision.gameObject != AttachedPlayer.transform.GetChild(1).gameObject)
            {
                Destroy(this.gameObject);
            }
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

    IEnumerator BubbleDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
    /*
    IEnumerator OrbCooldown()
    {
        OrbStart = true;
        yield return new WaitForSeconds(2f);
        OrbTurned = true;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
    */
}
