using UnityEngine;

public class CharacterSelector1 : MonoBehaviour
{
    public GameObject PlayerTrackerThing;
    private Rigidbody2D rb;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        if (PlayerTrackerThing == null)
        {
            PlayerTrackerThing = GameObject.Find("PlayerTrackerThing");
        }
    }

    void Update()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MysteriousStranger"))
        {
            PlayerTrackerThing.GetComponent<PlayerTracker>().Player1 = "MysteriousStranger";
        }
        else if (collision.gameObject.CompareTag("BabyBeard"))
        {
            PlayerTrackerThing.GetComponent<PlayerTracker>().Player1 = "BabyBeard";
        }
    }
}
