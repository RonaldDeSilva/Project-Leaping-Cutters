using UnityEngine;

public class CharacterSelector4 : MonoBehaviour
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
        if (Input.GetAxis("Horizontal4") > 0.15 || Input.GetAxis("Horizontal4") < -0.15 || Input.GetAxis("Vertical4") > 0.15 || Input.GetAxis("Vertical4") < -0.15)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal4") * 10f, -Input.GetAxis("Vertical4") * 10f);
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
            PlayerTrackerThing.GetComponent<PlayerTracker>().Player4 = "MysteriousStranger";
        }
        else if (collision.gameObject.CompareTag("BabyBeard"))
        {
            PlayerTrackerThing.GetComponent<PlayerTracker>().Player4 = "BabyBeard";
        }
    }
}
