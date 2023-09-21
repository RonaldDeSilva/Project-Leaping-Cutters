using System.Collections;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private float soundLen;
    public void Awaken(AudioClip cl, float vol)
    {
        GetComponent<AudioSource>().volume = vol;
        GetComponent<AudioSource>().clip = cl;
        GetComponent<AudioSource>().Play();
        soundLen = cl.length;
        StartCoroutine("Delete");
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(soundLen);
        GetComponent<AudioSource>().clip = null;
        Destroy(this.gameObject);
    }
}
