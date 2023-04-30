using UnityEngine;

public class Spinning : MonoBehaviour
{
    private Rigidbody2D rb;
    public float thing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.Rotate(Vector3.up, thing * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.left * thing * Time.deltaTime, Space.Self);

        rb.isKinematic = true;
    }
}
