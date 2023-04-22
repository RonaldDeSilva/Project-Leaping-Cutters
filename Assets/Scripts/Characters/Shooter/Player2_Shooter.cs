using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2_Shooter : MonoBehaviour
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
    public GameObject Player;
    private Rigidbody2D rb;
    private GameObject Can;
    #endregion
    //----------------------------------------------------------------------------------------------------------------------------------------------------------
    #region Attributes & Abilities
    public float spd;
    private int lives;

    public float DashDistance;
    public float DashSpd;
    public float DashCooldown;
    private bool Dashed = false;
    private bool Dashing = false;
    private Vector2 DashDir;

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

    private void Awake()
    {
        #region Initialization
        //Setting the hinge as the correct one and also setting up motor references to allow the motor to be turned off and on and change direction
        hinge = GetComponent<HingeJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        arm.transform.rotation = new Quaternion(0, 0, 0, 0);
        arm.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, 0);
        Can = GameObject.Find("Canvas");
        lives = int.Parse(Can.transform.GetChild(1).gameObject.GetComponent<Text>().text);

        motorRef1 = new JointMotor2D { motorSpeed = -spd, maxMotorTorque = 10000 };
        motorRef2 = new JointMotor2D { motorSpeed = spd, maxMotorTorque = 10000 };
        motorRef3 = new JointMotor2D { motorSpeed = 0, maxMotorTorque = 10000 };
        Recoiling = false;
        Dashed = false;
        Dashing = false;
        Reloading = false;

        if (Respawn == null)
        {
            Respawn = GameObject.Find("Respawn2").transform;
        }
        #endregion
    }

    void FixedUpdate()
    {
        #region Movement and Ability Inputs

        #region Arm Movement

        //Calculating the rotational position of the arm, and converting the input axes into rotation
        var armRot = arm.transform.eulerAngles;

        //Atan2 is a function to get the angle between a point on a circle and the positive X axis, 
        var controllerRot = new Vector3(0, 0, (Mathf.Atan2(-Input.GetAxis("Vertical2"), Input.GetAxis("Horizontal2")) * 180 / Mathf.PI) - 90f);


        //Align the controllers angle numbers to the arms angle numbers
        if (controllerRot.z + 360f < 360f)
        {
            controllerRot.z = controllerRot.z + 360f;
        }

        //This if is asking if there is any activity in the controllers stick
        if (Input.GetAxis("Vertical2") > 0.15 || Input.GetAxis("Vertical2") < -0.15 || Input.GetAxis("Horizontal2") > 0.15 || Input.GetAxis("Horizontal2") < -0.15)
        {
            //This is checking whether the rotation of the arm is greater or less than the rotation of the controller's stick
            //It also checks if they are within 10 degrees of each other and if so it doesn't keep moving to prevent stuttering
            if (armRot.z > controllerRot.z && !ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 10f))
            {
                if (Mathf.Abs(armRot.z - controllerRot.z) < 180)
                {
                    hinge.motor = motorRef1;
                }
                else
                {
                    hinge.motor = motorRef2;
                }
            }
            else if (armRot.z < controllerRot.z && !ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 10f))
            {
                if (Mathf.Abs(armRot.z - controllerRot.z) < 180)
                {
                    hinge.motor = motorRef2;
                }
                else
                {
                    hinge.motor = motorRef1;
                }
            }
            else
            {
                hinge.motor = motorRef3;
            }


        }
        else
        {
            hinge.motor = motorRef3;
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        #region Dashing Input

        //Input for the dash ability
        if (Input.GetAxis("Dash2") > 0 && !Dashed && !Dashing && !Recoiling)
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
        if (Input.GetAxis("Shoot2") > 0 && !Recoiling && !Reloading && !Dashing)
        {
            StartCoroutine("Shoot");
        }

        //Movement Code for recoiling after shooting
        if (Recoiling)
        {
            rb.velocity = RecoilDir;
        }

        #endregion

        #endregion
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
                Can.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = lives.ToString();
                Instantiate(Player, Respawn.position, this.transform.rotation);
                Can.transform.GetChild(1).GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                Can.transform.GetChild(1).GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                Destroy(this.gameObject);
            }
            else
            {
                lives -= 1;
                Can.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = lives.ToString();
                Destroy(this.gameObject);
            }

        }
    }
    #endregion
    //---------------------------------------------------------------------------------------------------------------------------------------------------
    #region Dashing Coroutines
    IEnumerator Dash()
    {
        DashDir = new Vector2(Input.GetAxis("Horizontal2") * DashSpd, -Input.GetAxis("Vertical2") * DashSpd);
        Dashing = true;
        Can.transform.GetChild(1).GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(DashDistance);
        Dashed = true;
        StartCoroutine("DashCooldownTimer");
    }

    IEnumerator DashCooldownTimer()
    {
        Dashing = false;
        yield return new WaitForSeconds(DashCooldown);
        Can.transform.GetChild(1).GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        Dashed = false;
    }
    #endregion
    //-------------------------------------------------------------------------------------------------------------------------------
    #region Shooting Coroutines
    IEnumerator Shoot()
    {
        RecoilDir = new Vector2(-Input.GetAxis("Horizontal2") * RecoilSpd, Input.GetAxis("Vertical2") * RecoilSpd);
        ProjDir = new Vector2(Input.GetAxis("Horizontal2") * ProjSpd, -Input.GetAxis("Vertical2") * ProjSpd);
        var pj = Instantiate(Proj, new Vector3(arm.transform.GetChild(0).transform.position.x, arm.transform.GetChild(0).transform.position.y, 0), arm.transform.rotation);
        pj.GetComponent<Projectile>().Awaken(ProjDir);
        Recoiling = true;
        Can.transform.GetChild(1).GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(RecoilDistance);
        Reloading = true;
        StartCoroutine("ShootCooldownTimer");
    }

    IEnumerator ShootCooldownTimer()
    {
        Recoiling = false;
        yield return new WaitForSeconds(ReloadTime);
        Can.transform.GetChild(1).GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        Reloading = false;
    }
    #endregion

    #endregion
}
