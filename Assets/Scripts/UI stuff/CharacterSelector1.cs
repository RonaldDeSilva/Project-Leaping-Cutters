using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector1 : MonoBehaviour
{
    public GameObject PlayerTrackerThing;
    private Rigidbody2D rb;
    private string character = " ";

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
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Horizontal"));
        PlayerTrackerThing.GetComponent<PlayerTracker>().Player1 = character;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MysteriousStranger"))
        {
            character = "MysteriousStranger";
        } else if (collision.gameObject.CompareTag("BabyBeard"))
        {
            character = "BabyBeard";
        }
    }
}
