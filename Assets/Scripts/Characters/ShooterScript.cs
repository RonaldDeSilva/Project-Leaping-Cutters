using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShooterScript : MonoBehaviour
{
    #region Components and Attributes

    //Components
    private Rigidbody2D rb;
    private GameObject arm;
    private GameObject Can;

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
    public AudioClip GunShotSound;
    public AudioClip ReloadingSound;
    public AudioClip ArmShootSound;
    public AudioClip ArmReturnSound;
    public AudioClip DashingSound;
    public AudioClip DashCooldownSound;
    public bool dying;


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
        Fist.SetActive(true);
        playerNum = GetComponent<MovementBase>().playerNum;
        Recoiling = false;
        Dashed = false;
        Dashing = false;
        Reloading = false;
        Punching = false;
        Punched = false;
        Returning = false;
        dying = false;

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
            Proj.GetComponent<SpriteRenderer>().color = new Color(0.682353f, 0.8117647f, 0.1882353f);
        }
        else if (playerNum == 3)
        {
            childNum = 2;
            dashInput = "Dash3";
            shootInput = "Shoot3";
            punchInput = "Punch3";
            Proj.GetComponent<SpriteRenderer>().color = new Color(0.1607843f, 0.6156863f, 0.7254902f);
        }
        else if (playerNum == 4)
        {
            childNum = 3;
            dashInput = "Dash4";
            shootInput = "Shoot4";
            punchInput = "Punch4";
            Proj.GetComponent<SpriteRenderer>().color = new Color(0.8862745f, 0.454902f, 0.8705882f);
        }
    }

    #endregion

    #region Input

    void FixedUpdate()
    {
        if (!dying)
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
        else
        {
            StopAllCoroutines();
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
        var sound = Instantiate(AudioPlayer);
        sound.GetComponent<SoundPlayer>().Awaken(DashingSound, 1f);
        yield return new WaitForSeconds(DashDistance);
        Can.transform.GetChild(childNum).GetChild(1).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().DashCooldown0;
        Dashed = true;
        StartCoroutine("DashCooldownTimer");
    }

    IEnumerator DashCooldownTimer()
    {
        Dashing = false;
        yield return new WaitForSeconds(DashCooldown / 5);
        Can.transform.GetChild(childNum).GetChild(1).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().DashCooldown1;
        yield return new WaitForSeconds(DashCooldown / 5);
        Can.transform.GetChild(childNum).GetChild(1).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().DashCooldown2;
        yield return new WaitForSeconds(DashCooldown / 5);
        Can.transform.GetChild(childNum).GetChild(1).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().DashCooldown3;
        yield return new WaitForSeconds(DashCooldown / 5);
        Can.transform.GetChild(childNum).GetChild(1).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().DashCooldown4;
        yield return new WaitForSeconds(DashCooldown / 5);
        Can.transform.GetChild(childNum).GetChild(1).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().DashCooldown5;
        var sound = Instantiate(AudioPlayer);
        sound.GetComponent<SoundPlayer>().Awaken(DashCooldownSound, 1f);
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
        pj.GetComponent<Projectile>().Awaken(ProjDir, this.transform.GetChild(0).gameObject);
        var sound = Instantiate(AudioPlayer);
        sound.GetComponent<SoundPlayer>().Awaken(GunShotSound, 0.6f);
        Recoiling = true;
        yield return new WaitForSeconds(RecoilDistance);
        Can.transform.GetChild(childNum).GetChild(2).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().ReloadCooldown0;
        Reloading = true;
        StartCoroutine("ShootCooldownTimer");
    }

    IEnumerator ShootCooldownTimer()
    {
        Recoiling = false;
        yield return new WaitForSeconds(ReloadTime / 5);
        Can.transform.GetChild(childNum).GetChild(2).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().ReloadCooldown1;
        yield return new WaitForSeconds(ReloadTime / 5);
        Can.transform.GetChild(childNum).GetChild(2).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().ReloadCooldown2;
        yield return new WaitForSeconds(ReloadTime / 5);
        Can.transform.GetChild(childNum).GetChild(2).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().ReloadCooldown3;
        yield return new WaitForSeconds(ReloadTime / 5);
        Can.transform.GetChild(childNum).GetChild(2).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().ReloadCooldown4;
        yield return new WaitForSeconds(ReloadTime / 5);
        Can.transform.GetChild(childNum).GetChild(2).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().ReloadCooldown5;
        var sound = Instantiate(AudioPlayer);
        sound.GetComponent<SoundPlayer>().Awaken(ReloadingSound, 1f);
        Can.transform.GetChild(childNum).GetChild(2).gameObject.GetComponent<Image>().color = Color.white;
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
        PunchDir = new Vector2(PunchDir.x * Mathf.Clamp(Mathf.Abs(rb.velocity.x), 1, 1.5f), PunchDir.y * Mathf.Clamp(Mathf.Abs(rb.velocity.y), 1, 1.5f));
        ArmJoint.enabled = false;
        Fist.GetComponent<CopyRot>().Off = true;
        Punching = true;
        var sound = Instantiate(AudioPlayer);
        sound.GetComponent<SoundPlayer>().Awaken(ArmShootSound, 1f);
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
        yield return new WaitForSeconds(PunchDuration/2);
        var sound = Instantiate(AudioPlayer);
        sound.GetComponent<SoundPlayer>().Awaken(ArmReturnSound, 1f);
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
