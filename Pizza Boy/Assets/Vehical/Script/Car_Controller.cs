using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.XR;

public class Car_Controller : MonoBehaviour,IVehicleController
{
    public static bool isfinished;
    public bool Done;
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
        public GameObject WheelEffectObj;
        public Axel axel;
    }

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;
    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public List<Wheel> wheels;

    [SerializeField] private bool isbrake;
    public float moveInput;
    public float turnInput;
    private Rigidbody carrb;
    public Vector3 _centerofmass;
    //private object wheel;

    void Start()
    {
        Time.timeScale = 1;
        carrb = GetComponent<Rigidbody>();
        carrb.centerOfMass = _centerofmass;
    }

    void Update()
    {
        // GetInput();
        Done = isfinished;
        WheelSkid();
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

    public void OnAcceleration()
    {
            moveInput = 1;
            
    }
    public void OnAccelerationBack()
    {
            moveInput = 0.01f;
    }

    public void OnDeceleration()
    {
        moveInput = -1;
    }
    public void OnDecelerationBack()
    {
        moveInput = -0.01f;
    }

    public void Steer_Left()
    {
        turnInput = -1;
    }

    public void Steer_Right()
    {
        turnInput = 1;
    }

    public void Steer_Mid()
    {
        turnInput = 0;
    }
    public void OnBrake()
    {
        isbrake = true;
    }

    public void OnBrakeOff()
    {
        isbrake = false;
        moveInput = 0;
    }
    private void Move()
    {
        foreach(var wheel in wheels)
        {
            if (wheel.axel == Axel.Rear)
            {
                wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
            }
                
        }
    }

    void Brake()
    {
        if(Input.GetKey(KeyCode.Space) || isbrake)
        {
            foreach(var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 600 * brakeAcceleration * Time.deltaTime;
            }
        }
        else
        {
            foreach(var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }
    void Steer()
    {
        foreach(var wheel in wheels)
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
        foreach(var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos,out rot);
            wheel.wheelmodel.transform.position = pos;
            wheel.wheelmodel.transform.rotation = rot;
        }
    }

    void WheelSkid()
    {
        foreach(var wheel in wheels)
        {
            if ( ( isbrake && wheel.axel == Axel.Rear) || (Input.GetKey(KeyCode.Space) && wheel.axel == Axel.Rear))
            {
                wheel.WheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
            }
            else
            {
                wheel.WheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            isfinished = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            isfinished = false;
        }
    }

    public bool OnTarget()
    {
        return Done;
    }
}
