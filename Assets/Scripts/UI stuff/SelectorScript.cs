using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectorScript : MonoBehaviour
{
    public Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 2.5f, -Input.GetAxis("Vertical") * 2.5f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ThePit"))
        {
            SceneManager.LoadScene("ThePit-4Player");
        }
        else if (collision.gameObject.CompareTag("NoGround"))
        {
            SceneManager.LoadScene("NoGround-4Player");
        }
        else if (collision.gameObject.CompareTag("Spinner"))
        {
            SceneManager.LoadScene("Spinner");
        }
        else if (collision.gameObject.CompareTag("SpinningMoon"))
        {
            SceneManager.LoadScene("SpinningMoon");
        }
    }
}
