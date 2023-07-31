using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject PlayerTracker;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerTracker = GameObject.Find("PlayerTrackerThing");
    }
    void Update()
    {
        var playersh = 0;
        var playersv = 0;
        if (Input.GetAxis("Horizontal") != 0)
        {
            playersh++;
        }
        if (Input.GetAxis("Horizontal2") != 0)
        {
            playersh++;
        }
        if (Input.GetAxis("Horizontal3") != 0)
        {
            playersh++;
        }
        if (Input.GetAxis("Horizontal4") != 0)
        {
            playersh++;
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            playersv++;
        }
        if (Input.GetAxis("Vertical2") != 0)
        {
            playersv++;
        }
        if (Input.GetAxis("Vertical3") != 0)
        {
            playersv++;
        }
        if (Input.GetAxis("Vertical4") != 0)
        {
            playersv++;
        }

        if (playersh != 0 && playersv == 0)
        {
            var hor = (Input.GetAxis("Horizontal") + Input.GetAxis("Horizontal2") + Input.GetAxis("Horizontal3") + Input.GetAxis("Horizontal4")) / playersh;
            rb.velocity = new Vector2(hor * 10f, 0);
        }
        else if (playersh == 0 && playersv != 0)
        {
            var ver = (Input.GetAxis("Vertical") + Input.GetAxis("Vertical2") + Input.GetAxis("Vertical3") + Input.GetAxis("Vertical4")) / playersv;
            rb.velocity = new Vector2(0, -ver * 10f);
        }
        else if (playersh != 0 && playersv != 0)
        {
            var hor = (Input.GetAxis("Horizontal") + Input.GetAxis("Horizontal2") + Input.GetAxis("Horizontal3") + Input.GetAxis("Horizontal4")) / playersh;
            var ver = (Input.GetAxis("Vertical") + Input.GetAxis("Vertical2") + Input.GetAxis("Vertical3") + Input.GetAxis("Vertical4")) / playersv;
            rb.velocity = new Vector2(hor * 10f, -ver * 10f);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ThePit"))
        {
            SceneManager.LoadScene("ThePit-4Player");
        }
        else if (collision.gameObject.CompareTag("SpinningMoon"))
        {
            SceneManager.LoadScene("SpinningMoon");
        }
    }
}
