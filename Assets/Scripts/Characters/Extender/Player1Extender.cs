using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player1Extender : MonoBehaviour
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

    //Attributes for the Dash ability
    public float DashDistance;
    public float DashSpd;
    public float DashCooldown;
    private bool Dashed = false;
    private bool Dashing = false;
    private Vector2 DashDir;

    //Attributes for the Arm Extension ability
    public float ArmExtendLength;
    public float ArmExtendPeriod;
    public float ExtendCooldown;
    private float OriginalArmLength;
    private bool Extending = false;
    private bool Extended = false;
    private bool Retracting = false;

    #endregion
    //----------------------------------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        #region Initialization
        //Setting the hinge as the correct one and also setting up motor references to allow the motor to be turned off and on and change direction
        hinge = GetComponent<HingeJoint2D>();
        rb = GetComponent<Rigidbody2D>();
        arm.transform.rotation = new Quaternion(0, 0, 0, 0);
        arm.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 2, 0);
        arm.transform.localScale = new Vector3(0.5f, 2, 1);
        Can = GameObject.Find("Canvas");
        lives = int.Parse(Can.transform.GetChild(0).gameObject.GetComponent<Text>().text);
        OriginalArmLength = arm.transform.localScale.y;

        motorRef1 = new JointMotor2D { motorSpeed = -spd, maxMotorTorque = 10000 };
        motorRef2 = new JointMotor2D { motorSpeed = spd, maxMotorTorque = 10000 };
        motorRef3 = new JointMotor2D { motorSpeed = 0, maxMotorTorque = 10000 };

        Dashed = false;
        Dashing = false;
        Extending = false;
        Extended = false;

        if (Respawn == null)
        {
            Respawn = GameObject.Find("Respawn").transform;
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
        var controllerRot = new Vector3(0, 0, (Mathf.Atan2(-Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * 180 / Mathf.PI) - 90f);


        //Align the controllers angle numbers to the arms angle numbers
        if (controllerRot.z + 360f < 360f)
        {
            controllerRot.z = controllerRot.z + 360f;
        }

        //This if is asking if there is any activity in the controllers stick
        if (Input.GetAxis("Vertical") > 0.15 || Input.GetAxis("Vertical") < -0.15 || Input.GetAxis("Horizontal") > 0.15 || Input.GetAxis("Horizontal") < -0.15)
        {
            //This is checking whether the rotation of the arm is greater or less than the rotation of the controller's stick
            //It also checks if they are within 10 degrees of each other and if so it doesn't keep moving to prevent stuttering
            if (armRot.z > controllerRot.z && !ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 2))
            {
                if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 20f))
                {
                    if (Mathf.Abs(armRot.z - controllerRot.z) < 180)
                    {
                        hinge.motor = motorRef1;
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z + 180, 20f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = spd, maxMotorTorque = 10000 };
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z + 180, 10f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = spd / 2, maxMotorTorque = 10000 };
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z + 180, 8f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = spd / 4, maxMotorTorque = 10000 };
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z + 180, 5f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = spd / 6, maxMotorTorque = 10000 };
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z + 180, 3.5f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = spd / 10, maxMotorTorque = 10000 };
                    }
                }
                else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 10f))
                {
                    hinge.motor = new JointMotor2D { motorSpeed = -spd / 2, maxMotorTorque = 10000 };
                }
                else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 8f))
                {
                    hinge.motor = new JointMotor2D { motorSpeed = -spd / 4, maxMotorTorque = 10000 };
                }
                else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 5f))
                {
                    hinge.motor = new JointMotor2D { motorSpeed = -spd / 6, maxMotorTorque = 10000 };
                }
                else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 3.5f))
                {
                    hinge.motor = new JointMotor2D { motorSpeed = -spd / 10, maxMotorTorque = 10000 };
                }
            }
            else if (armRot.z < controllerRot.z && !ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 2))
            {
                if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 20f))
                {
                    if (Mathf.Abs(armRot.z - controllerRot.z) < 180)
                    {
                        hinge.motor = motorRef2;
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z + 180, controllerRot.z, 20f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = -spd, maxMotorTorque = 10000 };
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z + 180, controllerRot.z, 10f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = -spd / 2, maxMotorTorque = 10000 };
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z + 180, controllerRot.z, 8f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = -spd / 4, maxMotorTorque = 10000 };
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z + 180, controllerRot.z, 5f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = -spd / 6, maxMotorTorque = 10000 };
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z + 180, controllerRot.z, 3.5f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = -spd / 10, maxMotorTorque = 10000 };
                    }
                }
                else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 20f))
                {
                    hinge.motor = new JointMotor2D { motorSpeed = spd / 2, maxMotorTorque = 10000 };
                }
                else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 15f))
                {
                    hinge.motor = new JointMotor2D { motorSpeed = spd / 4, maxMotorTorque = 10000 };
                }
                else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 10f))
                {
                    hinge.motor = new JointMotor2D { motorSpeed = spd / 6, maxMotorTorque = 10000 };
                }
                else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 5f))
                {
                    hinge.motor = new JointMotor2D { motorSpeed = spd / 10, maxMotorTorque = 10000 };
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
        if (Input.GetAxis("Dash") > 0 && !Dashed && !Dashing && !Extending && !Retracting)
        {
            StartCoroutine("Dash");
        }

        //Movement Code for Dashing
        if (Dashing)
        {
            rb.velocity = DashDir;
        }

        #endregion

        #region Extending Input

        //Input for the Extend ability
        if (Input.GetAxis("Shoot") > 0 && !Extending && !Extended && !Retracting && !Dashing)
        {
            StartCoroutine("Extend");
        }

        if (Extending)
        {
            arm.transform.localScale = new Vector3(arm.transform.localScale.x, arm.transform.localScale.y + ((ArmExtendLength / ArmExtendPeriod) * Time.deltaTime), arm.transform.localScale.x);
        }

        if (Retracting)
        {
            arm.transform.localScale = new Vector3(arm.transform.localScale.x, arm.transform.localScale.y - ((ArmExtendLength / (ArmExtendPeriod / 3)) * Time.deltaTime), arm.transform.localScale.x);
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
                Can.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = lives.ToString();
                Instantiate(Player, Respawn.position, this.transform.rotation);
                Can.transform.GetChild(0).GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                Can.transform.GetChild(0).GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
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
        Can.transform.GetChild(0).GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(DashDistance);
        Dashed = true;
        StartCoroutine("DashCooldownTimer");
    }

    IEnumerator DashCooldownTimer()
    {
        Dashing = false;
        yield return new WaitForSeconds(DashCooldown);
        Can.transform.GetChild(0).GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        Dashed = false;
    }
    #endregion
    //-------------------------------------------------------------------------------------------------------------------------------
    #region Extending Coroutines
    IEnumerator Extend()
    {

        Extending = true;
        Can.transform.GetChild(0).GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(ArmExtendPeriod);
        Retracting = true;
        StartCoroutine("Retract");
    }

    IEnumerator Retract()
    {
        Extending = false;
        Can.transform.GetChild(0).GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(ArmExtendPeriod / 3);
        Extended = true;
        StartCoroutine("ExtendCooldownTimer");
    }

    IEnumerator ExtendCooldownTimer()
    {
        arm.transform.localScale = new Vector3(arm.transform.localScale.x, OriginalArmLength, arm.transform.localScale.z);
        arm.transform.localPosition = new Vector3(Mathf.Clamp(arm.transform.localPosition.x, -2, 2), Mathf.Clamp(arm.transform.localPosition.y, -2, 2), 0);
        arm.transform.localRotation = new Quaternion(0, 0, (this.transform.position.y - arm.transform.position.y) / (this.transform.position.x - arm.transform.position.x), arm.transform.rotation.w);
        Retracting = false;
        yield return new WaitForSeconds(ExtendCooldown);
        Can.transform.GetChild(0).GetChild(2).gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        Extended = false;
    }
    #endregion

    #endregion
}
