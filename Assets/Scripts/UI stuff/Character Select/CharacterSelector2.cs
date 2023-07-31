using UnityEngine;

public class CharacterSelector2 : MonoBehaviour
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
        rb.velocity = new Vector2(Input.GetAxis("Horizontal2") * 10f, -Input.GetAxis("Vertical2") * 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MysteriousStranger"))
        {
            PlayerTrackerThing.GetComponent<PlayerTracker>().Player2 = "MysteriousStranger";
        }
        else if (collision.gameObject.CompareTag("BabyBeard"))
        {
            PlayerTrackerThing.GetComponent<PlayerTracker>().Player2 = "BabyBeard";
        }
        else if (collision.gameObject.CompareTag("InflatableGuy"))
        {
            PlayerTrackerThing.GetComponent<PlayerTracker>().Player2 = "InflatableGuy";
        }
    }
}
