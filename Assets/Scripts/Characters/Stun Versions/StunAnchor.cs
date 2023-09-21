using System.Collections;
using UnityEngine;

public class StunAnchor : MonoBehaviour
{
    #region Attributes

    private bool Activated;
    private bool CooldownPeriod;
    private bool GracePeriod;
    private Rigidbody2D rb;
    private GameObject Anchor;

    public float Cooldown;
    public float NewWeight;
    public float NewGravityScale;
    public PhysicsMaterial2D NewFric;
    public float AbilityLen;
    private int PlayerNum;
    private string SpecialButton;
    private float OrigWeight = 11f;
    private float OrigGravityScale = 2.5f;
    private float rate;
    private float rate2;
    public PhysicsMaterial2D OldFric;
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
        PlayerNum = GetComponent<StunMovement>().playerNum;
        Anchor = transform.GetChild(2).gameObject;
        Anchor.GetComponent<SpriteRenderer>().enabled = false;
        Anchor.transform.localScale = new Vector3(3f, 3f, 1f);
        rb.mass = OrigWeight;
        rb.gravityScale = OrigGravityScale;
        rate = (NewWeight - OrigWeight) / AbilityLen;
        rate2 = 2 / AbilityLen;
        GetComponent<Rigidbody2D>().sharedMaterial = OldFric;
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
                Anchor.GetComponent<SpriteRenderer>().enabled = false;
                CooldownPeriod = true;
                rb.mass = OrigWeight;
                rb.gravityScale = OrigGravityScale;
                rb.GetComponent<Rigidbody2D>().sharedMaterial = OldFric;
                StartCoroutine("SpecialCooldown");

            }

            if (Activated)
            {
                rb.mass = rb.mass - (rate * Time.deltaTime);
                Anchor.transform.localScale = new Vector3(Anchor.transform.localScale.x - (rate2 * Time.deltaTime), Anchor.transform.localScale.y - (rate2 * Time.deltaTime), 1f);
            }
        }
        else
        {
            StopAllCoroutines();
            Activated = false;
            Anchor.GetComponent<SpriteRenderer>().enabled = false;
            CooldownPeriod = false;
            rb.mass = OrigWeight;
            rb.gravityScale = OrigGravityScale;
            rb.GetComponent<Rigidbody2D>().sharedMaterial = OldFric;
        }
    }

    #endregion

    #region Coroutines

    IEnumerator Changer()
    {
        rb.mass = NewWeight;
        rb.gravityScale = NewGravityScale;
        rb.GetComponent<Rigidbody2D>().sharedMaterial = NewFric;
        Anchor.GetComponent<SpriteRenderer>().enabled = true;
        Anchor.transform.localScale = new Vector3(3, 3, 1);
        Activated = true;
        yield return new WaitForSeconds(AbilityLen);
        rb.GetComponent<Rigidbody2D>().sharedMaterial = OldFric;
        rb.mass = OrigWeight;
        rb.gravityScale = OrigGravityScale;
        Activated = false;
        Anchor.GetComponent<SpriteRenderer>().enabled = false;
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