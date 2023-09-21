using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StunShooter : MonoBehaviour
{
    #region Components and Attributes

    //Components
    private Rigidbody2D rb;
    private GameObject arm;
    private GameObject Can;
    private bool Stunned;

    //Attributes for the Dash ability
    public float DashDistance;
    public float DashSpd;
    public float DashCooldown;
    private bool Dashed = false;
    private bool Dashing = false;
    private Vector2 DashDir;

    //Attributes for the Projectile ability
    public GameObject Proj;
    public float ProjSpd;
    public float RecoilDistance;
    public float RecoilSpd;
    public float ReloadTime;
    private bool Recoiling = false;
    private bool Reloading = false;
    private Vector2 RecoilDir;
    private Vector2 ProjDir;

    //Attributes for Punch ability
    public float PunchSpd;
    public float PunchCooldown;
    public float PunchDuration;
    public float FistWeight;
    private bool Punching = false;
    private bool Punched = false;
    private bool Returning = false;
    private Vector2 PunchDir;
    public Transform FistLocation;
    private GameObject Fist;
    private HingeJoint2D ArmJoint;

    //Player number specific Attributes
    private int playerNum;
    private int childNum;
    private string dashInput;
    private string shootInput;
    private string punchInput;

    //Sound Effects
    public GameObject AudioPlayer;
    public AudioClip WeaponHitPlayer;
    public AudioClip WeaponHitWall;
    public AudioClip WeaponHitWeapon;
    public AudioClip GunShotSound;
    public AudioClip ReloadingSound;
    public AudioClip ArmShootSound;
    public AudioClip ArmReturnSound;
    public AudioClip DashingSound;
    public AudioClip DashCooldownSound;
    public AudioClip ArmHittingPlayer;
    public AudioClip ArmHittingWall;
    public AudioClip ArmHittingWeapon;


    #endregion

    #region Initialization

    void Start()
    {
        StopAllCoroutines();
        rb = GetComponent<Rigidbody2D>();
        Can = GameObject.Find("Canvas");
        arm = gameObject.transform.GetChild(0).gameObject;
        Fist = gameObject.transform.GetChild(1).gameObject;
        ArmJoint = Fist.GetComponent<HingeJoint2D>();
        Fist.transform.position = FistLocation.position;
        Fist.transform.rotation = FistLocation.rotation;
        ArmJoint.enabled = true;
        Fist.GetComponent<CapsuleCollider2D>().isTrigger = true;
        Fist.GetComponent<Rigidbody2D>().mass = 0.0001f;
        Fist.GetComponent<CopyRot>().Off = false;
        playerNum = GetComponent<StunMovement>().playerNum;
        Recoiling = false;
        Dashed = false;
        Dashing = false;
        Reloading = false;
        Punching = false;
        Punched = false;
        Returning = false;
        Stunned = false;

        if (playerNum == 1)
        {
            childNum = 0;
            dashInput = "Dash";
            shootInput = "Shoot";
            punchInput = "Punch";
        }
        else if (playerNum == 2)
        {
            childNum = 1;
            dashInput = "Dash2";
            shootInput = "Shoot2";
            punchInput = "Punch2";
        }
        else if (playerNum == 3)
        {
            childNum = 2;
            dashInput = "Dash3";
            shootInput = "Shoot3";
            punchInput = "Punch3";
        }
        else if (playerNum == 4)
        {
            childNum = 3;
            dashInput = "Dash4";
            shootInput = "Shoot4";
            punchInput = "Punch4";
        }
    }

    #endregion

    #region Input

    void FixedUpdate()
    {
        Stunned = GetComponent<StunMovement>().Stunned;

        if (!Stunned)
        {
            //Input for the dash ability
            if (Input.GetAxis(dashInput) > 0 && !Dashed && !Dashing && !Recoiling && !Punching)
            {
                StartCoroutine("Dash");
            }

            //Movement Code for Dashing
            if (Dashing)
            {
                rb.velocity = DashDir;
            }

            //--------------------------------------------------------------------------------------------------------------------------------------------------------

            //Input for the Shoot ability
            if (Input.GetAxis(shootInput) > 0 && !Recoiling && !Reloading && !Dashing && !Punching)
            {
                StartCoroutine("Shoot");
            }

            //Movement Code for recoiling after shooting
            if (Recoiling)
            {
                rb.velocity = RecoilDir;
            }

            //---------------------------------------------------------------------------------------------------------------------------

            //Input for the Punch ability
            if (Input.GetAxis(punchInput) > 0 && !Recoiling && !Dashing && !Punching && !Punched && !Returning)
            {
                StartCoroutine("Punch");
            }

            if (Punching)
            {
                Fist.GetComponent<Rigidbody2D>().velocity = PunchDir;
            }

            if (Returning)
            {
                var returnVel = new Vector2((this.transform.position.x - Fist.transform.position.x) * (PunchSpd / 2), (this.transform.position.y - Fist.transform.position.y) * (PunchSpd / 2));
                Fist.GetComponent<Rigidbody2D>().velocity = returnVel;
            }
        }

    }

    #endregion

    #region Dashing Coroutines
    IEnumerator Dash()
    {
        var angle = ((arm.transform.localEulerAngles.z + 90) * Mathf.Deg2Rad);
        var newX = Mathf.Cos(angle);
        var newY = Mathf.Sin(angle);
        DashDir = new Vector2(newX * DashSpd, newY * DashSpd);
        Dashing = true;
        Can.transform.GetChild(childNum).GetChild(1).gameObject.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(DashDistance);
        Dashed = true;
        StartCoroutine("DashCooldownTimer");
    }

    IEnumerator DashCooldownTimer()
    {
        Dashing = false;
        yield return new WaitForSeconds(DashCooldown);
        Can.transform.GetChild(childNum).GetChild(1).gameObject.GetComponent<Image>().color = Color.white;
        Dashed = false;
    }
    #endregion
    //-------------------------------------------------------------------------------------------------------------------------------
    #region Shooting Coroutines
    IEnumerator Shoot()
    {
        var angle = ((arm.transform.localEulerAngles.z + 90) * Mathf.Deg2Rad);
        var newX = Mathf.Cos(angle);
        var newY = Mathf.Sin(angle);
        RecoilDir = new Vector2(-newX * RecoilSpd, -newY * RecoilSpd);
        ProjDir = new Vector2(newX * ProjSpd, newY * ProjSpd);
        var pj = Instantiate(Proj, new Vector3(arm.transform.GetChild(0).transform.position.x, arm.transform.GetChild(0).transform.position.y, 0), arm.transform.rotation);
        pj.GetComponent<Projectile>().Awaken(ProjDir);
        Recoiling = true;
        Can.transform.GetChild(childNum).GetChild(2).gameObject.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(RecoilDistance);
        Reloading = true;
        StartCoroutine("ShootCooldownTimer");
    }

    IEnumerator ShootCooldownTimer()
    {
        Recoiling = false;
        yield return new WaitForSeconds(ReloadTime);
        Can.transform.GetChild(childNum).GetChild(2).gameObject.GetComponent<Image>().color = Color.blue;
        Reloading = false;
    }
    #endregion
    //------------------------------------------------------------------------------------------------------------------------------
    #region Punch Coroutines

    IEnumerator Punch()
    {
        var angle = ((arm.transform.localEulerAngles.z + 90) * Mathf.Deg2Rad);
        var newX = Mathf.Cos(angle);
        var newY = Mathf.Sin(angle);
        PunchDir = new Vector2(newX * PunchSpd, newY * PunchSpd);
        ArmJoint.enabled = false;
        Fist.GetComponent<CopyRot>().Off = true;
        Punching = true;
        yield return new WaitForSeconds(0.08f);
        Fist.GetComponent<Rigidbody2D>().mass = FistWeight;
        Fist.GetComponent<CapsuleCollider2D>().isTrigger = false;
        yield return new WaitForSeconds(PunchDuration);
        Punched = true;
        Fist.GetComponent<CapsuleCollider2D>().isTrigger = true;
        Fist.GetComponent<Rigidbody2D>().mass = 0.0001f;
        StartCoroutine("Return");
    }

    IEnumerator Return()
    {
        Punching = false;
        Returning = true;
        yield return new WaitForSeconds(PunchDuration / 2);
        Returning = false;
        Fist.transform.position = FistLocation.position;
        Fist.transform.rotation = FistLocation.rotation;
        ArmJoint.enabled = true;
        Fist.GetComponent<CopyRot>().Off = false;
        yield return new WaitForSeconds(PunchCooldown);
        Punched = false;
    }

    #endregion

}
