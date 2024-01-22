using UnityEngine;

public class Clouds : MonoBehaviour
{
    public GameObject Cloud;
    private bool spawnedNewGuy;


    void FixedUpdate()
    {
        if (!spawnedNewGuy && transform.position.x > -0.5)
        {
            Instantiate(Cloud, new Vector3(-73.2f, 0, 0), new Quaternion(0,0,0,0));
            spawnedNewGuy = true;
        }

        transform.position = new Vector3(transform.position.x + (2 * Time.deltaTime), 0, 0);

        if (transform.position.x > 54.8)
        {
            Destroy(this.gameObject);
        }
    }
}
