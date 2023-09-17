using System.Collections;
using UnityEngine;

public class StunUmbrellaOpen : MonoBehaviour
{
    #region Attributes

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
    private bool Stunned;

    #endregion

    #region Initialization

    void Start()
    {
        StopAllCoroutines();
        rb = GetComponent<Rigidbody2D>();
        Activated = false;
        CooldownPeriod = false;
        GracePeriod = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = UmbrellaClosed;
        PlayerNum = GetComponent<StunMovement>().playerNum;
        Stunned = false;

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

    #endregion

    #region Input

    void FixedUpdate()
    {
        Stunned = GetComponent<StunMovement>().Stunned;

        if (!Stunned)
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
                rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, 0f, MaxSpd / 2), Mathf.Clamp(rb.velocity.y, 0f, MaxSpd));
            }
        }
        else
        {
            StopAllCoroutines();
            Activated = false;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = UmbrellaClosed;
            CooldownPeriod = false;
        }
    }

    #endregion

    #region Coroutine

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

    #endregion
}
