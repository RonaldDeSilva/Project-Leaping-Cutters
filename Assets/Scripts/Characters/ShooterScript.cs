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
    private bool Punching = false;
    private bool Punched = false;
    private Vector2 PunchDir;
    //private SpringJoint2D ArmSpringJoint;

    //Player number specific Attributes
    public int playerNum;
    private int childNum;
    private string dashInput;
    private string shootInput;
    private string punchInput;

    #endregion

    #region Initialization

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Can = GameObject.Find("Canvas");
        arm = gameObject.transform.GetChild(0).gameObject;
        //ArmSpringJoint = arm.GetComponent<SpringJoint2D>();
        Recoiling = false;
        Dashed = false;
        Dashing = false;
        Reloading = false;
        Punching = false;
        Punched = false;

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
        if (Input.GetAxis(punchInput) > 0 && !Recoiling && !Reloading && !Dashing && !Punching && !Punched)
        {
            StartCoroutine("Punch");
        }

        if (Punching)
        {
            //Make a short range projectile that just moves away from the player not too far and then comes back to the player
            //Possibly make the punch projectile attached to the player through a spring joint
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
        //ArmSpringJoint.enabled = false;
        Punching = true;
        //Can.transform.GetChild(childNum).GetChild(1).gameObject.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(PunchDuration);
        Punched = true;
        StartCoroutine("PunchCooldownTimer");
    }

    IEnumerator PunchCooldownTimer()
    {
        //ArmSpringJoint.enabled = true;
        Punching = false;
        yield return new WaitForSeconds(PunchCooldown);
        //Can.transform.GetChild(childNum).GetChild(1).gameObject.GetComponent<Image>().color = Color.white;
        Punched = false;
    }
    
    #endregion

}
