using System.Collections;
using UnityEngine;

public class DeleteAfter5 : MonoBehaviour
{
    // This is a function which makes the object it is placed on destroy itself after 5 seconds,
    // its pretty stupid just whipped it up so that I could make levels with platforms
    void Start()
    {
        StartCoroutine("Death");
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
