using System.Collections;
using UnityEngine;

public class WeaponSoundEffects : MonoBehaviour
{
    public GameObject AudioPlayer;
    public AudioClip WeaponHittingWall;
    public AudioClip WeaponHittingPlayer;
    public AudioClip WeaponHittingWeapon;
    private GameObject AlreadyHitMe;
    private GameObject AlreadyHitMe2;
    private GameObject AlreadyHitMe3;
    private int HitCount;
    private GameObject Arm;
    private float HitTimer1;
    private float HitTimer2;
    private float HitTimer3;

    private void Start()
    {
        AlreadyHitMe = null;
        AlreadyHitMe2 = null;
        AlreadyHitMe3 = null;
        HitCount = 1;
        Arm = transform.parent.GetChild(1).gameObject;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != Arm)
        {
            if (HitCount == 1)
            {
                if (collision.gameObject.CompareTag("Player") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe = collision.gameObject;
                    HitCount += 1;
                    var sound2 = Instantiate(AudioPlayer);
                    sound2.GetComponent<SoundPlayer>().Awaken(WeaponHittingPlayer, 0.7f);
                    HitTimer1 = 1f;
                    StartCoroutine("HitResetter1");
                }
                else if (collision.gameObject.CompareTag("Platform") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe = collision.gameObject;
                    HitCount += 1;
                    var sound2 = Instantiate(AudioPlayer);
                    sound2.GetComponent<SoundPlayer>().Awaken(WeaponHittingWall, 0.6f);
                    HitTimer1 = 0.3f;
                    StartCoroutine("HitResetter1");
                }
                else if (collision.gameObject.CompareTag("Weapon") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe = collision.gameObject;
                    HitCount += 1;
                    var sound2 = Instantiate(AudioPlayer);
                    sound2.GetComponent<SoundPlayer>().Awaken(WeaponHittingWeapon, 0.4f);
                    HitTimer1 = 0.8f;
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
                    sound4.GetComponent<SoundPlayer>().Awaken(WeaponHittingPlayer, 0.7f);
                    HitTimer2 = 1f;
                    StartCoroutine("HitResetter2");
                }
                else if (collision.gameObject.CompareTag("Platform") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe2 = collision.gameObject;
                    HitCount += 1;
                    var sound5 = Instantiate(AudioPlayer);
                    HitTimer2 = 0.3f;
                    sound5.GetComponent<SoundPlayer>().Awaken(WeaponHittingWall, 0.6f);
                    StartCoroutine("HitResetter2");
                }
                else if (collision.gameObject.CompareTag("Weapon") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe2 = collision.gameObject;
                    HitCount += 1;
                    var sound2 = Instantiate(AudioPlayer);
                    sound2.GetComponent<SoundPlayer>().Awaken(WeaponHittingWeapon, 0.4f);
                    HitTimer2 = 0.8f;
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
                    sound6.GetComponent<SoundPlayer>().Awaken(WeaponHittingPlayer, 0.7f);
                    HitTimer3 = 1f;
                    StartCoroutine("HitResetter3");
                }
                else if (collision.gameObject.CompareTag("Platform") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe3 = collision.gameObject;
                    HitCount = 1;
                    var sound7 = Instantiate(AudioPlayer);
                    sound7.GetComponent<SoundPlayer>().Awaken(WeaponHittingWall, 0.6f);
                    HitTimer3 = 0.3f;
                    StartCoroutine("HitResetter3");
                }
                else if (collision.gameObject.CompareTag("Weapon") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
                {
                    AlreadyHitMe3 = collision.gameObject;
                    HitCount = 1;
                    var sound2 = Instantiate(AudioPlayer);
                    sound2.GetComponent<SoundPlayer>().Awaken(WeaponHittingWeapon, 0.4f);
                    HitTimer3 = 0.8f;
                    StartCoroutine("HitResetter3");
                }
            }
        }
    }

    IEnumerator HitResetter1()
    {
        yield return new WaitForSeconds(HitTimer1);
        AlreadyHitMe = null;
    }

    IEnumerator HitResetter2()
    {
        yield return new WaitForSeconds(HitTimer2);
        AlreadyHitMe2 = null;
    }

    IEnumerator HitResetter3()
    {
        yield return new WaitForSeconds(HitTimer3);
        AlreadyHitMe3 = null;
    }
}
