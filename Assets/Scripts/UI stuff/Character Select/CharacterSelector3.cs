using UnityEngine;

public class CharacterSelector3 : MonoBehaviour
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
        if (Input.GetAxis("Horizontal3") > 0.15 || Input.GetAxis("Horizontal3") < -0.15 || Input.GetAxis("Vertical3") > 0.15 || Input.GetAxis("Vertical3") < -0.15)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal3") * 10f, -Input.GetAxis("Vertical3") * 10f);
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
}
