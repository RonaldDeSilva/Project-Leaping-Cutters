using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector3 : MonoBehaviour
{
    public GameObject PlayerTrackerThing;
    private Rigidbody2D rb;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal3") * 2.5f, -Input.GetAxis("Vertical3") * 2.5f);
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
    }
}
