using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bike_CC_WheelCollider : MonoBehaviour
{
    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]

    public struct Wheel
    {
        public GameObject wheelmodel;
        public WheelCollider wheelCollider;
        public Axel axel;
    }
    public GameObject Handle;
    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;
    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f, HandleRotVal = 30f, HandleRotSpeed = .15f;

    public List<Wheel> wheels;

    float moveInput;
    float turnInput;
    private Rigidbody carrb;
    public Vector3 _centerofmass;
    //private object wheel;

    void Start()
    {
        carrb = GetComponent<Rigidbody>();
        carrb.centerOfMass = _centerofmass;
    }

    void Update()
    {
        GetInput();
    }
    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
        AnimatorWheel();

    }
    void GetInput()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    private void Move()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Rear)
            {
                wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
            }

        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 600 * brakeAcceleration * Time.deltaTime;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }
    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                
                var _steerangle = turnInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerangle, 0.6f);
                
            }
        }
    }
    void AnimatorWheel()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            if (wheel.axel == Axel.Front)
            {
                Handle.transform.localRotation = Quaternion.Slerp(Handle.transform.localRotation, Quaternion.Euler(Handle.transform.localRotation.eulerAngles.x, HandleRotVal * turnInput, Handle.transform.localRotation.eulerAngles.z), HandleRotSpeed);    
            }
            wheel.wheelmodel.transform.rotation = rot;
            wheel.wheelmodel.transform.position = pos;
        }
    }

}
