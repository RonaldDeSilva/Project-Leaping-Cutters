using System.Collections;
using UnityEngine;

public class UmbrellaOpenMS : MonoBehaviour
{
    public Sprite UmbrellaOpen;
    public Sprite UmbrellaClosed;
    private bool Activated;
    private bool CooldownPeriod;
    private bool GracePeriod;
    private Rigidbody2D rb;

    public float Cooldown;
    public float MaxSpd;
    public float AbilityLen;
    private int PlayerNum;
    private string SpecialButton;


    void Start()
    {
        StopAllCoroutines();
        rb = GetComponent<Rigidbody2D>();
        Activated = false;
        CooldownPeriod = false;
        GracePeriod = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = UmbrellaClosed;
        PlayerNum = GetComponent<MovementBase>().playerNum;

        if (PlayerNum == 1)
        {
            SpecialButton = "Special";
        } 
        else if (PlayerNum == 2)
        {
            SpecialButton = "Special2";
        }
        else if (PlayerNum == 3)
        {
            SpecialButton = "Special3";
        }
        else if (PlayerNum == 4)
        {
            SpecialButton = "Special4";
        }
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
        yield return new WaitForSeconds(AbilityLen);
        Activated = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = UmbrellaClosed;
        CooldownPeriod = true;
        StartCoroutine("SpecialCooldown");
    }

    IEnumerator SpecialCooldown()
    {
        yield return new WaitForSeconds(Cooldown);
        CooldownPeriod = false;
    }

    IEnumerator Grace()
    {
        yield return new WaitForSeconds(0.5f);
        GracePeriod = false;
    }

}
