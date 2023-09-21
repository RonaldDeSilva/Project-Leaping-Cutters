using System.Collections;
using UnityEngine;

public class ArmSoundEffects : MonoBehaviour
{
    public GameObject AudioPlayer;
    public AudioClip ArmHittingWall;
    public AudioClip ArmHittingPlayer;
    public AudioClip ArmHittingWeapon; 
    private GameObject AlreadyHitMe;
    private GameObject AlreadyHitMe2;
    private GameObject AlreadyHitMe3;
    private int HitCount;
    private GameObject Weapon;

    private void Start()
    {
        AlreadyHitMe = null;
        AlreadyHitMe2 = null;
        AlreadyHitMe3 = null;
        HitCount = 1;
        Weapon = transform.parent.GetChild(0).gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != Weapon)
        {
            if (HitCount == 1)
            {
                if (collision.gameObject.CompareTag("Player") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe = collision.gameObject;
                    HitCount += 1;
                    var sound2 = Instantiate(AudioPlayer);
                    sound2.GetComponent<SoundPlayer>().Awaken(ArmHittingPlayer, 1f);
                    StartCoroutine("HitResetter1");
                }
                else if (collision.gameObject.CompareTag("Platform") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe = collision.gameObject;
                    HitCount += 1;
                    var sound2 = Instantiate(AudioPlayer);
                    sound2.GetComponent<SoundPlayer>().Awaken(ArmHittingWall, 1f);
                    StartCoroutine("HitResetter1");
                }
                else if (collision.gameObject.CompareTag("Weapon") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe = collision.gameObject;
                    HitCount += 1;
                    var sound2 = Instantiate(AudioPlayer);
                    sound2.GetComponent<SoundPlayer>().Awaken(ArmHittingWeapon, 1f);
                    StartCoroutine("HitResetter1");
                }
            }
            else if (HitCount == 2)
            {
                if (collision.gameObject.CompareTag("Player") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe2 = collision.gameObject;
                    HitCount += 1;
                    var sound4 = Instantiate(AudioPlayer);
                    sound4.GetComponent<SoundPlayer>().Awaken(ArmHittingPlayer, 1f);
                    StartCoroutine("HitResetter2");
                }
                else if (collision.gameObject.CompareTag("Platform") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe2 = collision.gameObject;
                    HitCount += 1;
                    var sound5 = Instantiate(AudioPlayer);
                    sound5.GetComponent<SoundPlayer>().Awaken(ArmHittingWall, 1f);
                    StartCoroutine("HitResetter2");
                }
                else if (collision.gameObject.CompareTag("Weapon") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe2 = collision.gameObject;
                    HitCount += 1;
                    var sound2 = Instantiate(AudioPlayer);
                    sound2.GetComponent<SoundPlayer>().Awaken(ArmHittingWeapon, 1f);
                    StartCoroutine("HitResetter2");
                }
            }
            else if (HitCount == 3)
            {
                if (collision.gameObject.CompareTag("Player") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe3 = collision.gameObject;
                    HitCount = 1;
                    var sound6 = Instantiate(AudioPlayer);
                    sound6.GetComponent<SoundPlayer>().Awaken(ArmHittingPlayer, 1f);
                    StartCoroutine("HitResetter3");
                }
                else if (collision.gameObject.CompareTag("Platform") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe3 = collision.gameObject;
                    HitCount = 1;
                    var sound7 = Instantiate(AudioPlayer);
                    sound7.GetComponent<SoundPlayer>().Awaken(ArmHittingWall, 1f);
                    StartCoroutine("HitResetter3");
                }
                else if (collision.gameObject.CompareTag("Weapon") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe3 = collision.gameObject;
                    HitCount += 1;
                    var sound2 = Instantiate(AudioPlayer);
                    sound2.GetComponent<SoundPlayer>().Awaken(ArmHittingWeapon, 1f);
                    StartCoroutine("HitResetter3");
                }
            }
        }
    }

    IEnumerator HitResetter1()
    {
        yield return new WaitForSeconds(0.8f);
        AlreadyHitMe = null;
    }

    IEnumerator HitResetter2()
    {
        yield return new WaitForSeconds(0.8f);
        AlreadyHitMe2 = null;
    }

    IEnumerator HitResetter3()
    {
        yield return new WaitForSeconds(0.8f);
        AlreadyHitMe3 = null;
    }
}
