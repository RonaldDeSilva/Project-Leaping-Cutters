using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MovementBase : MonoBehaviour
{
    #region Components and Attributes

    //Hinge stuff
    private HingeJoint2D hinge;
    private JointMotor2D motorRef1;
    private JointMotor2D motorRef2;
    private JointMotor2D motorRef3;

    //Components
    private GameObject arm;
    private Transform Respawn;
    public GameObject Player;
    private GameObject Can;

    //Attributes
    public float spd;
    private int lives;
    public bool dying;

    //Player number specific attributes
    public int playerNum;
    private int childNum;
    public float armHeight;
    private string respawnName;
    private string Vertical;
    private string Horizontal;

    //Sound effects
    public GameObject AudioPlayer;
    public AudioClip[] HurtSounds;
    public AudioClip DeathSound1;
    public AudioClip DeathSound2;
    public AudioClip RespawningSound;
    public AudioClip LandingSoundGround;
    public AudioClip LandingSoundPlayer;
    private GameObject AlreadyLandedOn;
    private GameObject AlreadyHitMe;
    private GameObject AlreadyHitMe2;
    private GameObject AlreadyHitMe3;
    private int HitCount;

    #endregion

    #region Initialization

    public void Awaken(int Number)
    {
        playerNum = Number;
    }

    void Start()
    {

        if (playerNum == 1)
        {
            childNum = 0;
            respawnName = "Respawn";
            Vertical = "Vertical";
            Horizontal = "Horizontal";
        } 
        else if (playerNum == 2)
        {
            childNum = 1;
            respawnName = "Respawn2";
            Vertical = "Vertical2";
            Horizontal = "Horizontal2";
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0.682353f, 0.8117647f, 0.1882353f); ;
            var color2 = new Color(0.3529412f, 0.3843137f, 0.8196079f);
            GetComponent<SpriteRenderer>().color = color2;
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = color2;
        }
        else if (playerNum == 3)
        {
            childNum = 2;
            respawnName = "Respawn3";
            Vertical = "Vertical3";
            Horizontal = "Horizontal3";
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0.1607843f, 0.6156863f, 0.7254902f);
            var color2 = new Color(0.7647059f, 0.4352941f, 0.1568628f);
            GetComponent<SpriteRenderer>().color = color2;
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = color2;
        }
        else if (playerNum == 4)
        {
            childNum = 3;
            respawnName = "Respawn4";
            Vertical = "Vertical4";
            Horizontal = "Horizontal4";
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8862745f, 0.454902f, 0.8705882f);
            var color2 = new Color(0.07058824f, 0.6039216f, 0.2235294f);
            GetComponent<SpriteRenderer>().color = color2;
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = color2;
        }

        hinge = GetComponent<HingeJoint2D>();
        Can = GameObject.Find("Canvas");
        lives = int.Parse(Can.transform.GetChild(childNum).GetComponentInChildren<Text>().text.ToString());
        arm = gameObject.transform.GetChild(0).gameObject;
        //arm.transform.rotation = new Quaternion(0, 0, 0, 0);
        //arm.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + armHeight, 0);
        AlreadyHitMe = null;
        AlreadyHitMe2 = null;
        AlreadyHitMe3 = null;
        AlreadyLandedOn = null;
        HitCount = 1;
        dying = false;
        GetComponent<SpriteRenderer>().enabled = true;
        arm.GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = true;
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;

        motorRef1 = new JointMotor2D { motorSpeed = -spd, maxMotorTorque = 10000 };
        motorRef2 = new JointMotor2D { motorSpeed = spd, maxMotorTorque = 10000 };
        motorRef3 = new JointMotor2D { motorSpeed = 0, maxMotorTorque = 10000 };

        if (Respawn == null)
        {
            Respawn = GameObject.Find(respawnName).transform;
        }

    }

    #endregion

    #region Movement

    void FixedUpdate()
    {
        if (!dying)
        {
            //Calculating the rotational position of the arm, and converting the input axes into rotation
            var armRot = arm.transform.eulerAngles;

            //Atan2 is a function to get the angle between a point on a circle and the positive X axis, 
            var controllerRot = new Vector3(0, 0, (Mathf.Atan2(-Input.GetAxis(Vertical), Input.GetAxis(Horizontal)) * 180 / Mathf.PI) - 90f);


            //Align the controllers angle numbers to the arms angle numbers
            if (controllerRot.z + 360f < 360f)
            {
                controllerRot.z = controllerRot.z + 360f;
            }

            //This if is asking if there is any activity in the controllers stick
            if (Input.GetAxis(Vertical) > 0.15 || Input.GetAxis(Vertical) < -0.15 || Input.GetAxis(Horizontal) > 0.15 || Input.GetAxis(Horizontal) < -0.15)
            {
                //This is checking whether the rotation of the arm is greater or less than the rotation of the controller's stick
                //It then determines based on that and also the distance from the controllers rotation to the arms rotation
                //How fast to move the arm so that it can be moved percisely but also quickly
                if (armRot.z > controllerRot.z && !ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 1.5f))
                {
                    if (Mathf.Abs(armRot.z - controllerRot.z) > 345)
                    {
                        if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z + 360, 20f))
                        {
                            hinge.motor = new JointMotor2D { motorSpeed = spd, maxMotorTorque = 10000 };
                        }
                        else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z + 360, 10f))
                        {
                            hinge.motor = new JointMotor2D { motorSpeed = spd / 2, maxMotorTorque = 10000 };
                        }
                        else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z + 360, 8f))
                        {
                            hinge.motor = new JointMotor2D { motorSpeed = spd / 4, maxMotorTorque = 10000 };
                        }
                        else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z + 360, 5f))
                        {
                            hinge.motor = new JointMotor2D { motorSpeed = spd / 6, maxMotorTorque = 10000 };
                        }
                        else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z + 360, 3.5f))
                        {
                            hinge.motor = new JointMotor2D { motorSpeed = spd / 10, maxMotorTorque = 10000 };
                        }
                        else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z + 360, 1.5f))
                        {
                            hinge.motor = new JointMotor2D { motorSpeed = spd / 15, maxMotorTorque = 10000 };
                        }
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 20f))
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
                        else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z + 180, 1.5f))
                        {
                            hinge.motor = new JointMotor2D { motorSpeed = spd / 15, maxMotorTorque = 10000 };
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
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 1.5f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = -spd / 15, maxMotorTorque = 10000 };
                    }
                }
                else if (armRot.z < controllerRot.z && !ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 1.5f))
                {
                    if (Mathf.Abs(armRot.z - controllerRot.z) > 345)
                    {
                        if (!ApproximatelyFunction.FastApproximately(armRot.z + 360, controllerRot.z, 20f))
                        {
                            hinge.motor = new JointMotor2D { motorSpeed = -spd, maxMotorTorque = 10000 };
                        }
                        else if (!ApproximatelyFunction.FastApproximately(armRot.z + 360, controllerRot.z, 10f))
                        {
                            hinge.motor = new JointMotor2D { motorSpeed = -spd / 2, maxMotorTorque = 10000 };
                        }
                        else if (!ApproximatelyFunction.FastApproximately(armRot.z + 360, controllerRot.z, 8f))
                        {
                            hinge.motor = new JointMotor2D { motorSpeed = -spd / 4, maxMotorTorque = 10000 };
                        }
                        else if (!ApproximatelyFunction.FastApproximately(armRot.z + 360, controllerRot.z, 5f))
                        {
                            hinge.motor = new JointMotor2D { motorSpeed = -spd / 6, maxMotorTorque = 10000 };
                        }
                        else if (!ApproximatelyFunction.FastApproximately(armRot.z + 360, controllerRot.z, 3.5f))
                        {
                            hinge.motor = new JointMotor2D { motorSpeed = -spd / 10, maxMotorTorque = 10000 };
                        }
                        else if (!ApproximatelyFunction.FastApproximately(armRot.z + 360, controllerRot.z, 1.5f))
                        {
                            hinge.motor = new JointMotor2D { motorSpeed = -spd / 15, maxMotorTorque = 10000 };
                        }
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 20f))
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
                        else if (!ApproximatelyFunction.FastApproximately(armRot.z + 180, controllerRot.z, 1.5f))
                        {
                            hinge.motor = new JointMotor2D { motorSpeed = -spd / 15, maxMotorTorque = 10000 };
                        }
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 10f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = spd / 2, maxMotorTorque = 10000 };
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 8f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = spd / 4, maxMotorTorque = 10000 };
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 5f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = spd / 6, maxMotorTorque = 10000 };
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 3.5f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = spd / 10, maxMotorTorque = 10000 };
                    }
                    else if (!ApproximatelyFunction.FastApproximately(armRot.z, controllerRot.z, 1.5f))
                    {
                        hinge.motor = new JointMotor2D { motorSpeed = spd / 15, maxMotorTorque = 10000 };
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

            if (armRot.z < 175 && armRot.z > 5)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = true;
                transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().flipX = false;
                transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    #endregion

    #region Death Coroutine

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Deathbox") && !dying)
        {
            if (lives > 1)
            {
                lives -= 1;
                Can.transform.GetChild(childNum).GetComponentInChildren<Text>().text = lives.ToString();
                if (lives == 2)
                {
                    Can.transform.GetChild(childNum).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().Lives2;
                }
                else if (lives == 1)
                {
                    Can.transform.GetChild(childNum).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().Lives1;
                }
                GetComponent<ShooterScript>().dying = true;
                GetComponent<CapsuleCollider2D>().enabled = false;
                StartCoroutine("Death");
                dying = true;
            }
            else
            {
                lives -= 1;
                Can.transform.GetChild(childNum).GetComponentInChildren<Text>().text = lives.ToString();
                Can.transform.GetChild(childNum).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().Lives0;
                GetComponent<ShooterScript>().dying = true;
                GetComponent<CapsuleCollider2D>().enabled = false;
                StartCoroutine("Death2");
                dying = true;
            }
        }
    }

    IEnumerator Death()
    {
        hinge.motor = motorRef3;
        Can.transform.GetChild(childNum).GetChild(1).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().DashCooldown5;
        Can.transform.GetChild(childNum).GetChild(2).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().ReloadCooldown5;
        var sound = Instantiate(AudioPlayer);
        GetComponent<SpriteRenderer>().enabled = false;
        arm.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        if (SceneManager.GetActiveScene().name == "BabyBeards_Ship")
        {
            sound.GetComponent<SoundPlayer>().Awaken(DeathSound1, 0.9f);
        }
        else
        {
            sound.GetComponent<SoundPlayer>().Awaken(DeathSound2, 0.9f);
        }
        yield return new WaitForSeconds(3f);
        var sound2 = Instantiate(AudioPlayer);
        sound2.GetComponent<SoundPlayer>().Awaken(RespawningSound, 1f);
        if (lives > 0)
        {
            var p = Instantiate(Player, Respawn.position, this.transform.rotation);
            p.GetComponent<MovementBase>().Awaken(playerNum);
        }
        Destroy(this.gameObject);
    }

    IEnumerator Death2()
    {
        hinge.motor = motorRef3;
        Can.transform.GetChild(childNum).GetChild(1).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().DashCooldown5;
        Can.transform.GetChild(childNum).GetChild(2).gameObject.GetComponent<Image>().sprite = Can.GetComponent<LivesTextures>().ReloadCooldown5;
        var sound = Instantiate(AudioPlayer); 
        if (SceneManager.GetActiveScene().name == "BabyBeards_Ship")
        {
            sound.GetComponent<SoundPlayer>().Awaken(DeathSound1, 0.9f);
        }
        else
        {
            sound.GetComponent<SoundPlayer>().Awaken(DeathSound2, 0.9f);
        }
        Destroy(this.gameObject);
        yield return new WaitForSeconds(0.1f);
    }

    #endregion

    #region Collision for SFX

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (HitCount == 1)
        {
            if (collision.gameObject.CompareTag("Bullet") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
            {
                AlreadyHitMe = collision.gameObject;
                HitCount += 1;
                var rando = Random.Range(0, 3);
                var sound2 = Instantiate(AudioPlayer);
                sound2.GetComponent<SoundPlayer>().Awaken(HurtSounds[rando], 0.8f);
            }
            else if (collision.gameObject.CompareTag("Arm") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
            {
                AlreadyHitMe = collision.gameObject;
                HitCount += 1;
                var rando = Random.Range(0, 3);
                var sound3 = Instantiate(AudioPlayer);
                sound3.GetComponent<SoundPlayer>().Awaken(HurtSounds[rando], 0.8f);
                StartCoroutine("HitResetter1");
            }
        }
        else if (HitCount == 2)
        {
            if (collision.gameObject.CompareTag("Bullet") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
            {
                AlreadyHitMe2 = collision.gameObject;
                HitCount += 1;
                var rando = Random.Range(0, 3);
                var sound4 = Instantiate(AudioPlayer);
                sound4.GetComponent<SoundPlayer>().Awaken(HurtSounds[rando], 0.8f);
            }
            else if (collision.gameObject.CompareTag("Arm") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
            {
                AlreadyHitMe2 = collision.gameObject;
                HitCount += 1;
                var rando = Random.Range(0, 3);
                var sound5 = Instantiate(AudioPlayer);
                sound5.GetComponent<SoundPlayer>().Awaken(HurtSounds[rando], 0.8f);
                StartCoroutine("HitResetter2");
            }
        }
        else if (HitCount == 3)
        {
            if (collision.gameObject.CompareTag("Bullet") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
            {
                AlreadyHitMe3 = collision.gameObject;
                HitCount = 1;
                var rando = Random.Range(0, 3);
                var sound6 = Instantiate(AudioPlayer);
                sound6.GetComponent<SoundPlayer>().Awaken(HurtSounds[rando], 0.8f);
            }
            else if (collision.gameObject.CompareTag("Arm") && collision.gameObject != AlreadyHitMe && collision.gameObject != AlreadyHitMe2 && collision.gameObject != AlreadyHitMe3)
            {
                AlreadyHitMe3 = collision.gameObject;
                HitCount = 1;
                var rando = Random.Range(0, 3);
                var sound7 = Instantiate(AudioPlayer);
                sound7.GetComponent<SoundPlayer>().Awaken(HurtSounds[rando], 0.8f);
                StartCoroutine("HitResetter3");
            }
        }

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > 1.2f || Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > 1.2f)
        {
            if (collision.gameObject.CompareTag("Platform") && collision.gameObject != AlreadyLandedOn)
            {
                AlreadyLandedOn = collision.gameObject;
                var sound = Instantiate(AudioPlayer);
                sound.GetComponent<SoundPlayer>().Awaken(LandingSoundGround, 0.8f);
                StartCoroutine("LandResetter");
            }
            else if(collision.gameObject.CompareTag("Player") && collision.gameObject != AlreadyLandedOn)
            {
                AlreadyLandedOn = collision.gameObject;
                var sound = Instantiate(AudioPlayer);
                sound.GetComponent<SoundPlayer>().Awaken(LandingSoundPlayer, 0.8f);
                StartCoroutine("LandResetter");
            }

        }
    }

    IEnumerator HitResetter1()
    {
        yield return new WaitForSeconds(0.3f);
        AlreadyHitMe = null;
    }

    IEnumerator HitResetter2()
    {
        yield return new WaitForSeconds(0.3f);
        AlreadyHitMe2 = null;
    }

    IEnumerator HitResetter3()
    {
        yield return new WaitForSeconds(0.3f);
        AlreadyHitMe3 = null;
    }

    IEnumerator LandResetter()
    {
        yield return new WaitForSeconds(1f);
        AlreadyLandedOn = null;
    }

    #endregion
}
