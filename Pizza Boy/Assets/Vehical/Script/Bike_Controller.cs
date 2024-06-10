using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bike_Controller : MonoBehaviour
{
    RaycastHit hit;
    public float moveInput, steerInput, rayLength, currentVelocityOffset;
    [HideInInspector] public Vector3 velocity;
    public Rigidbody sphererb, bikerb;
    public float maxSpeed, acceleration, steerStrength, tiltangle, gravity, bikeTileIncreament = .09f, zTiltAngle = 45, HandleRotVal = 30f, HandleRotSpeed = .15f;
    [Range(1, 10)]
    public float brakeForce;
    public GameObject Handle, SkidObj;
    public LayerMask derivableSurface;
    public bool isbrake;

    void Start()
    {
        Time.timeScale = 1;
        sphererb.transform.parent = null;
        rayLength = sphererb.GetComponent<SphereCollider>().radius + 0.2f;
        velocity = sphererb.transform.InverseTransformDirection(sphererb.velocity);
        bikerb.transform.parent = null;
    }

    void Update()
    {
        transform.position = sphererb.transform.position;
        bikerb.MoveRotation(transform.rotation);
        GetInput();

    }
    void GetInput()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }
    void FixedUpdate()
    {
        Movement();
        BikeTilt();
    }

    public void OnAcceleration()
    {
        Debug.Log("OnAcceleration called");
        moveInput = 1;
    }

    public void OnAccelerationBack()
    {
        Debug.Log("OnAccelerationback called");
        moveInput = 0.01f;
    }

    public void OnDeceleration()
    {
        Debug.Log("OnDeceleration called");
        moveInput = -1;
    }

    public void OnDecelerationBack()
    {
        Debug.Log("OnDecelerationBack called");
        moveInput = -0.01f;
    }

    public void Steer_Left()
    {
        Debug.Log("Steer_left called");
        if (moveInput > 0.2f || moveInput < -0.2f)
        {
            currentVelocityOffset = 0.3f;
        }
        else
        {
            currentVelocityOffset = 0;
        }
        steerInput = -1;
    }

    public void Steer_Right()
    {
        Debug.Log("Steer_Right called");
        if (moveInput > 0.2f || moveInput < -0.2f)
        {
            currentVelocityOffset = 0.3f;
        }
        else
        {
            currentVelocityOffset = 0;
        }
        steerInput = 1;
    }

    public void Steer_Mid()
    {
        Debug.Log("Steer_Mid called");
        currentVelocityOffset = 0;
        steerInput = 0;
    }

    public void OnBrake()
    {
        Debug.Log("OnBrake called");
        isbrake = true;
    }

    public void OnBrakeOff()
    {
        Debug.Log("OnBrakeOff called");
        isbrake = false;
        moveInput = 0;
    }

    void Movement()
    {
        if (Grounded())
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                Acceleration();
                Steering();
            }
            Brake();
        }
        else
        {
            Gravity();
        }
    }

    void BikeTilt()
    {
        float xRot = (Quaternion.FromToRotation(bikerb.transform.up, hit.normal) * bikerb.transform.rotation).eulerAngles.x;
        float zRot = 0;
        if (currentVelocityOffset > 0)
        {
            zRot = -zTiltAngle * steerInput * currentVelocityOffset;
        }
        Quaternion targetRot = Quaternion.Slerp(bikerb.transform.rotation, Quaternion.Euler(xRot, transform.eulerAngles.y, zRot), bikeTileIncreament);
        Quaternion newRotation = Quaternion.Euler(targetRot.eulerAngles.x, transform.eulerAngles.y, targetRot.eulerAngles.z);
        bikerb.MoveRotation(newRotation);
    }

    void Acceleration()
    {
        Debug.Log("Accelerating with moveInput: " + moveInput);
        sphererb.velocity = Vector3.Lerp(sphererb.velocity, moveInput * maxSpeed * transform.forward, Time.fixedDeltaTime * acceleration);
    }

    void Steering()
    {
        Debug.Log("Steering with steerInput: " + steerInput);
        transform.Rotate(0, steerInput * moveInput * steerStrength * Time.deltaTime, 0, Space.World);
        Handle.transform.localRotation = Quaternion.Slerp(Handle.transform.localRotation, Quaternion.Euler(Handle.transform.localRotation.eulerAngles.x, HandleRotVal * steerInput, Handle.transform.localRotation.eulerAngles.z), HandleRotSpeed);
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || isbrake)
        {
            sphererb.velocity *= brakeForce / 10;
            SkidObj.GetComponentInChildren<TrailRenderer>().emitting = true;
        }
        else
        {
            SkidObj.GetComponentInChildren<TrailRenderer>().emitting = false;
        }
    }

    bool Grounded()
    {
        if (Physics.Raycast(sphererb.position, Vector3.down, out hit, rayLength, derivableSurface))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Gravity()
    {
        sphererb.AddForce(gravity * Vector3.down, ForceMode.Acceleration);
    }
}
