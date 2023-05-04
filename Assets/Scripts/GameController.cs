using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public WheelCollider[] wheelCols;
    public Transform[] wheelTrans;

    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float maxBrakeTorque;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<wheelCols.Length; i++)
        {
            wheelCols[i].transform.position = wheelTrans[i].position;
        }
    }

    void FixedUpdate()
    {
        float speed = maxMotorTorque * Input.GetAxis("Vertical");
        float angle = maxSteeringAngle * Input.GetAxis("Horizontal");
        float brake = maxBrakeTorque * 5;

        wheelCols[0].steerAngle = angle;
        wheelCols[1].steerAngle = angle;

        if(Input.GetKey("space"))
        {
            SetMotor(0, brake);
        }
        else
        {
            SetMotor(speed, 0);
        }

        for(int i = 0; i < wheelTrans.Length; i++)
        {
            Vector3 p;
            Quaternion q;

            wheelCols[i].GetWorldPose(out p, out q);

            wheelTrans[i].position = p;
            wheelTrans[i].rotation = q;
        }
    }

    void SetMotor(float speed, float brake)
    {
        wheelCols[0].motorTorque = speed;
        wheelCols[1].motorTorque = speed; 
        wheelCols[0].brakeTorque = brake;
        wheelCols[1].brakeTorque = brake;
    }
}
