using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector2 : MonoBehaviour
{
    public GameObject PlayerTrackerThing;
    private Rigidbody2D rb;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal2") * 2.5f, -Input.GetAxis("Vertical2") * 2.5f);
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
    }
}
