using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private GameObject PlayerTrackerThing;
    private Rigidbody2D rb;
    public int PlayerNum;
    public Color ButtonCol;
    public Color OrigButtonCol;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        PlayerTrackerThing = GameObject.Find("PlayerTrackerThing");

        if (PlayerNum == 2)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.682353f, 0.8117647f, 0.1882353f);
        }
        else if (PlayerNum == 3)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.1607843f, 0.6156863f, 0.7254902f);
        }
        else if (PlayerNum == 3)
        {
            GetComponent<SpriteRenderer>().color = new Color(0.8862745f, 0.454902f, 0.8705882f);
        }
    }

    void Update()
    {
        if (PlayerNum == 1)
        {
            if (Input.GetAxis("Horizontal") > 0.15 || Input.GetAxis("Horizontal") < -0.15 || Input.GetAxis("Vertical") > 0.15 || Input.GetAxis("Vertical") < -0.15)
            {
                rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 10f, -Input.GetAxis("Vertical") * 10f);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        else if (PlayerNum == 2)
        {
            if (Input.GetAxis("Horizontal2") > 0.15 || Input.GetAxis("Horizontal2") < -0.15 || Input.GetAxis("Vertical2") > 0.15 || Input.GetAxis("Vertical2") < -0.15)
            {
                rb.velocity = new Vector2(Input.GetAxis("Horizontal2") * 10f, -Input.GetAxis("Vertical2") * 10f);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        else if (PlayerNum == 3)
        {
            if (Input.GetAxis("Horizontal3") > 0.15 || Input.GetAxis("Horizontal3") < -0.15 || Input.GetAxis("Vertical3") > 0.15 || Input.GetAxis("Vertical3") < -0.15)
            {
                rb.velocity = new Vector2(Input.GetAxis("Horizontal3") * 10f, -Input.GetAxis("Vertical3") * 10f);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        else if (PlayerNum == 4)
        {
            if (Input.GetAxis("Horizontal4") > 0.15 || Input.GetAxis("Horizontal4") < -0.15 || Input.GetAxis("Vertical4") > 0.15 || Input.GetAxis("Vertical4") < -0.15)
            {
                rb.velocity = new Vector2(Input.GetAxis("Horizontal4") * 10f, -Input.GetAxis("Vertical4") * 10f);
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<SpriteRenderer>().color = ButtonCol;
        if (PlayerNum == 1)
        {
            if (collision.gameObject.CompareTag("MysteriousStranger"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player1 = "MysteriousStranger";
            }
            else if (collision.gameObject.CompareTag("BabyBeard"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player1 = "BabyBeard";
            }
            else if (collision.gameObject.CompareTag("WizGuy"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player1 = "WizGuy";
            }
            else if (collision.gameObject.CompareTag("ScubaSteve"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player1 = "ScubaSteve";
            }
        }
        else if (PlayerNum == 2)
        {
            if (collision.gameObject.CompareTag("MysteriousStranger"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player2 = "MysteriousStranger";
            }
            else if (collision.gameObject.CompareTag("BabyBeard"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player2 = "BabyBeard";
            }
            else if (collision.gameObject.CompareTag("WizGuy"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player2 = "WizGuy";
            }
            else if (collision.gameObject.CompareTag("ScubaSteve"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player2 = "ScubaSteve";
            }
        }
        else if (PlayerNum == 3)
        {
            if (collision.gameObject.CompareTag("MysteriousStranger"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player3 = "MysteriousStranger";
            }
            else if (collision.gameObject.CompareTag("BabyBeard"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player3 = "BabyBeard";
            }
            else if (collision.gameObject.CompareTag("WizGuy"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player3 = "WizGuy";
            }
            else if (collision.gameObject.CompareTag("ScubaSteve"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player3 = "ScubaSteve";
            }
        }
        else if (PlayerNum == 4)
        {
            if (collision.gameObject.CompareTag("MysteriousStranger"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player4 = "MysteriousStranger";
            }
            else if (collision.gameObject.CompareTag("BabyBeard"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player4 = "BabyBeard";
            }
            else if (collision.gameObject.CompareTag("WizGuy"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player4 = "WizGuy";
            }
            else if (collision.gameObject.CompareTag("ScubaSteve"))
            {
                PlayerTrackerThing.GetComponent<PlayerTracker>().Player4 = "ScubaSteve";
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("fart");
        var n = new Collider2D[4];
        collision.gameObject.GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D(), n);
        var colliding = false;
        for (int i = 0; i < n.Length - 1; i++)
        {
            if (n[i] != null)
            {
                colliding = true;
            }
        }

        if (colliding == false)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = OrigButtonCol;
        }

        if (PlayerNum == 1)
        {
            PlayerTrackerThing.GetComponent<PlayerTracker>().Player1 = "";
        }
        else if (PlayerNum == 2)
        {
            PlayerTrackerThing.GetComponent<PlayerTracker>().Player2 = "";
        }
        else if (PlayerNum == 3)
        {
            PlayerTrackerThing.GetComponent<PlayerTracker>().Player3 = "";
        }
        else if (PlayerNum == 4)
        {
            PlayerTrackerThing.GetComponent<PlayerTracker>().Player4 = "";
        }
    }
}
