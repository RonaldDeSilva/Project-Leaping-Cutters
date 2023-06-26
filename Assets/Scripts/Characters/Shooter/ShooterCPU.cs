using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShooterCPU : MonoBehaviour
{
    #region Hinge
    private HingeJoint2D hinge;
    private JointMotor2D motorRef1;
    private JointMotor2D motorRef2;
    private JointMotor2D motorRef3;
    #endregion
    //----------------------------------------------------------------------------------------------------------------------------------------------------------
    #region Components
    public GameObject arm;
    public Transform Respawn;
    public GameObject CPU;
    private GameObject Player;
    private Rigidbody2D rb;
    private GameObject Can;
    #endregion
    //----------------------------------------------------------------------------------------------------------------------------------------------------------
    #region Attributes & Abilities
    public float spd;
    private int lives;

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
    #endregion
    //----------------------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        #region Initialization
        //Setting the hinge as the correct one and also setting up motor references to allow the motor to be turned off and on and change direction
        hinge = GetComponent<HingeJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        arm.transform.rotation = new Quaternion(0, 0, 0, 0);
        arm.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, 0);
        Can = GameObject.Find("Canvas");
        lives = int.Parse(Can.transform.GetChild(0).gameObject.GetComponent<Text>().text);

        motorRef1 = new JointMotor2D { motorSpeed = -spd, maxMotorTorque = 10000 };
        motorRef2 = new JointMotor2D { motorSpeed = spd, maxMotorTorque = 10000 };
        motorRef3 = new JointMotor2D { motorSpeed = 0, maxMotorTorque = 10000 };
        Recoiling = false;
        Dashed = false;
        Dashing = false;
        Reloading = false;

        if (Respawn == null)
        {
            Respawn = GameObject.Find("Respawn").transform;
        }

        //Defining the Player attribute to allow the 
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        #endregion
    }

    void FixedUpdate()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {



            #region Arm Movement

            if (Player.transform.position.x < transform.position.x)
            {
                hinge.motor = motorRef1;
            } 
            else if (Player.transform.position.x > transform.position.x)
            {
                hinge.motor = motorRef1;
            }
            else
            {
                hinge.motor = motorRef3;
            }

            #endregion
            //--------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Dashing Input

            //Input for the dash ability
            if (Input.GetAxis("Dash") > 0 && !Dashed && !Dashing && !Recoiling)
            {
                StartCoroutine("Dash");
            }

            //Movement Code for Dashing
            if (Dashing)
            {
                rb.velocity = DashDir;
            }

            #endregion
            //--------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Projectile Input

            //Input for the Shoot ability
            if (Input.GetAxis("Shoot") > 0 && !Recoiling && !Reloading && !Dashing)
            {
                StartCoroutine("Shoot");
            }

            //Movement Code for recoiling after shooting
            if (Recoiling)
            {
                rb.velocity = RecoilDir;
            }
            #endregion
        }



    }

    #region Coroutines

    #region Death
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Deathbox"))
        {
            Dashing = false;
            Dashed = false;
            if (lives != 1)
            {
                lives -= 1;
                Can.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = lives.ToString();
                Instantiate(CPU, Respawn.position, this.transform.rotation);
                Can.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Image>().color = Color.white;
                Can.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Image>().color = Color.blue;
                Destroy(this.gameObject);
            }
            else
            {
                lives -= 1;
                Can.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = lives.ToString();
                Destroy(this.gameObject);
            }

        }
    }
    #endregion
    //---------------------------------------------------------------------------------------------------------------------------------------------------
    #region Dashing Coroutines
    IEnumerator Dash()
    {
        var angle = ((arm.transform.localEulerAngles.z + 90) * Mathf.Deg2Rad);
        var newX = Mathf.Cos(angle);
        var newY = Mathf.Sin(angle);
        DashDir = new Vector2(newX * DashSpd, newY * DashSpd);
        Dashing = true;
        Can.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(DashDistance);
        Dashed = true;
        StartCoroutine("DashCooldownTimer");
    }

    IEnumerator DashCooldownTimer()
    {
        Dashing = false;
        yield return new WaitForSeconds(DashCooldown);
        Can.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Image>().color = Color.white;
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
        Can.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(RecoilDistance);
        Reloading = true;
        StartCoroutine("ShootCooldownTimer");
    }

    IEnumerator ShootCooldownTimer()
    {
        Recoiling = false;
        yield return new WaitForSeconds(ReloadTime);
        Can.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Image>().color = Color.blue;
        Reloading = false;
    }
    #endregion

    #endregion
}
