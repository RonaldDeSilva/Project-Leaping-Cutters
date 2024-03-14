using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Experimentalpunch : MonoBehaviour
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
    private GameObject Fist;
    private HingeJoint2D ArmJoint;
    private SpringJoint2D SpringJoint;
    private bool PunchRecoil = false;

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

    public float[] XCoordinates = new float[10];
    public float[] YCoordinates = new float[10];
    public float[] ZRotCoordinates = new float[10];


    #endregion

    #region Initialization

    void Start()
    {
        StopAllCoroutines();
        rb = GetComponent<Rigidbody2D>();
        Can = GameObject.Find("Canvas");
        arm = gameObject.transform.GetChild(0).gameObject;
        Fist = gameObject.transform.GetChild(1).gameObject;
        ArmJoint = this.GetComponent<HingeJoint2D>();
        SpringJoint = arm.GetComponent<SpringJoint2D>();
        ArmJoint.enabled = true;
        //Fist.GetComponent<CapsuleCollider2D>().isTrigger = true;
        //Fist.GetComponent<Rigidbody2D>().mass = 0.0001f;
        //Fist.GetComponent<CopyRot>().Off = false;
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
                this.GetComponent<MovementBase>().dying = true;
                StartCoroutine("Punch");
                Punched = true;
                
            }

            if (Punching)
            {
                arm.GetComponent<Rigidbody2D>().velocity = PunchDir;
                if (!PunchRecoil)
                {
                    var List = new Collider2D[5];
                    var isTrue = false;
                    arm.GetComponent<CapsuleCollider2D>().OverlapCollider(new ContactFilter2D(), List);
                    for (int i = 0; i < 5; i++)
                    {
                        if (List[i] != null)
                        {
                            if (List[i].gameObject.CompareTag("Platform"))
                            {
                                isTrue = true;
                            }
                        }
                    }

                    if (isTrue)
                    {
                        //rb.AddForce(-PunchDir * 10, ForceMode2D.Impulse);
                        rb.velocity = -PunchDir;
                        PunchRecoil = true;
                    }
                }
            }

            if (Returning)
            {
                var returnVel = new Vector2((this.transform.position.x - arm.transform.position.x) * (PunchSpd / 2), (this.transform.position.y - arm.transform.position.y) * (PunchSpd / 2));
                arm.GetComponent<Rigidbody2D>().velocity = returnVel;
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
        sound.GetComponent<SoundPlayer>().Awaken(DashingSound, 0.8f);
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
        sound.GetComponent<SoundPlayer>().Awaken(DashCooldownSound, 0.8f);
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
        sound.GetComponent<SoundPlayer>().Awaken(GunShotSound, 0.28f);
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
        sound.GetComponent<SoundPlayer>().Awaken(ReloadingSound, 0.8f);
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
        PunchDir = new Vector2(PunchDir.x * Mathf.Clamp(Mathf.Abs(rb.velocity.x), 1f, 1.5f), PunchDir.y * Mathf.Clamp(Mathf.Abs(rb.velocity.y), 1f, 1.5f));
        ArmJoint.enabled = false;
        SpringJoint.enabled = false;
        PunchRecoil = false;
        Punching = true;
        Punched = false;
        arm.GetComponent<Rigidbody2D>().freezeRotation = true;
        var sound = Instantiate(AudioPlayer);
        sound.GetComponent<SoundPlayer>().Awaken(ArmShootSound, 0.8f);
        yield return new WaitForSeconds(PunchDuration);
        Punched = true;
        Punching = false;
        StartCoroutine("Return");
    }


    IEnumerator Return()
    {
        Returning = true;
        yield return new WaitForSeconds(PunchDuration / 2);
        var closest = 200f;
        var num = 0;
        for (int i = 0; i < 10; i++)
        {
            if (Mathf.Abs(arm.transform.eulerAngles.z - ZRotCoordinates[i]) < closest)
            {
                num = i;
                closest = Mathf.Abs(arm.transform.eulerAngles.z - ZRotCoordinates[i]);
            }
        }
        arm.GetComponent<Rigidbody2D>().freezeRotation = false;
        var sound = Instantiate(AudioPlayer);
        sound.GetComponent<SoundPlayer>().Awaken(ArmReturnSound, 0.8f);
        Returning = false;
        arm.transform.localPosition = new Vector3(XCoordinates[num], YCoordinates[num], 0);
        arm.transform.eulerAngles = new Vector3(0,0, ZRotCoordinates[num]);
        arm.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        SpringJoint.enabled = true;
        ArmJoint.enabled = true;
        this.GetComponent<MovementBase>().dying = false;
        yield return new WaitForSeconds(0.08f);
        yield return new WaitForSeconds(PunchCooldown);
        Punched = false;
    }

    #endregion

}
