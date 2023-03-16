using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : MonoBehaviour
{
    private HingeJoint2D hinge;
    private JointMotor2D motorRef1;
    private JointMotor2D motorRef2;
    private JointMotor2D motorRef3;
    public GameObject arm;
    public float spd;
    //private Vector2 movement;
    //public InputActionReference PlayerControls;



    private void Awake()
    {
        //Setting the hinge as the correct one and also setting up motor references to allow the motor to be turned off and on and change direction
        hinge = GetComponent<HingeJoint2D>();
        motorRef1 = new JointMotor2D { motorSpeed = -spd, maxMotorTorque = 10000 };
        motorRef2 = new JointMotor2D { motorSpeed = spd, maxMotorTorque = 10000 };
        motorRef3 = new JointMotor2D { motorSpeed = 0, maxMotorTorque = 10000 };

    }

    void FixedUpdate()
    {
        //movement = PlayerControls.action.ReadValue<Vector2>();

        //Calculating the rotational position of the arm, and converting the input axes into rotation
        var armRot = arm.transform.eulerAngles;

        //Atan2 is a function to get the angle between a point on a circle and the positive X axis, 
        var controllerRot = new Vector3(0, 0, (Mathf.Atan2(Input.GetAxis("Vertical2"), Input.GetAxis("Horizontal2")) * 180 / Mathf.PI) - 90f);


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
    }
}
