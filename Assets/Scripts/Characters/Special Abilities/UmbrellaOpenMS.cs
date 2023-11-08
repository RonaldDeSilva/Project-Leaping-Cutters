using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UmbrellaOpenMS : MonoBehaviour
{
    public Sprite UmbrellaOpen;
    public Sprite UmbrellaClosed;
    private bool Activated;
    private bool CooldownPeriod;
    private bool GracePeriod;
    private Rigidbody2D rb;
    private GameObject Can;

    public float Cooldown;
    public float MaxSpd;
    public float AbilityLen;
    private int PlayerNum;
    private string SpecialButton;

    public GameObject AudioPlayer;
    public AudioClip CooldownSound;
    public AudioClip UmbrellaOpenSound;
    public AudioClip UmbrellaCloseSound;

    private int childNum;

    void Start()
    {
        StopAllCoroutines();
        rb = GetComponent<Rigidbody2D>();
        Activated = false;
        CooldownPeriod = false;
        GracePeriod = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = UmbrellaClosed;
        PlayerNum = GetComponent<MovementBase>().playerNum;
        Can = GameObject.Find("Canvas");

        if (PlayerNum == 1)
        {
            SpecialButton = "Special";
            childNum = 0;
        } 
        else if (PlayerNum == 2)
        {
            SpecialButton = "Special2";
            childNum = 1;
        }
        else if (PlayerNum == 3)
        {
            SpecialButton = "Special3";
            childNum = 2;
        }
        else if (PlayerNum == 4)
        {
            SpecialButton = "Special4";
            childNum = 3;
        }
        Can.transform.GetChild(childNum).GetChild(3).gameObject.GetComponent<Image>().color = Color.white;
    }

    void FixedUpdate()
    {
        if (Input.GetAxis(SpecialButton) > 0 && !Activated && !CooldownPeriod)
        {
            StartCoroutine("Changer");
            GracePeriod = true;
            StartCoroutine("Grace");
        }
        else if (Input.GetAxis(SpecialButton) > 0 && Activated && !CooldownPeriod && !GracePeriod)
        {
            StopAllCoroutines();
            Activated = false;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = UmbrellaClosed;
            var sound2 = Instantiate(AudioPlayer);
            sound2.GetComponent<SoundPlayer>().Awaken(UmbrellaCloseSound, 1f);
            CooldownPeriod = true;
            StartCoroutine("SpecialCooldown");

        }
        
        if (Activated)
        {
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, 0f, MaxSpd/2), Mathf.Clamp(rb.velocity.y, 0f, MaxSpd));
        }

    }

    IEnumerator Changer()
    {
        Activated = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = UmbrellaOpen;
        var sound = Instantiate(AudioPlayer);
        sound.GetComponent<SoundPlayer>().Awaken(UmbrellaOpenSound, 1f);
        Can.transform.GetChild(childNum).GetChild(3).gameObject.GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(AbilityLen);
        Activated = false;
        var sound2 = Instantiate(AudioPlayer);
        sound2.GetComponent<SoundPlayer>().Awaken(UmbrellaCloseSound, 1f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = UmbrellaClosed;
        CooldownPeriod = true;
        StartCoroutine("SpecialCooldown");
    }

    IEnumerator SpecialCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        Can.transform.GetChild(childNum).GetChild(3).gameObject.GetComponent<Image>().color = Color.white;
        var sound = Instantiate(AudioPlayer);
        sound.GetComponent<SoundPlayer>().Awaken(CooldownSound, 1f);
        CooldownPeriod = false;
    }

    IEnumerator Grace()
    {
        yield return new WaitForSeconds(0.5f);
        GracePeriod = false;
    }

}
