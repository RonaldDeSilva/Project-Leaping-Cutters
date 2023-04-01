using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfter5 : MonoBehaviour
{
    // Start is called before the first frame update
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
