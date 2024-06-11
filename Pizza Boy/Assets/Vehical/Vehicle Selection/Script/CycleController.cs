using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CycleController : MonoBehaviour
{
    public static bool isfinished;
    public float maxSpeed = 20f;
    public float acceleration = 10f;
    public float brakeForce = 20f;
    public float steerAngle = 30f,steerAngle2;
    public Transform handlebar;
    public Transform leftPedal;
    public Transform rightPedal;
    public Transform frontWheel;
    public Transform rearWheel;
    public GameObject SkidObj;
    public float pedalSpeed = 100f;
    public float wheelRotationSpeed = 1000f;
    public float tiltAngle = 45f;
    public float tiltSpeed = 5f;

    private Rigidbody rb;
    private float moveInput;
    private float steerInput;
    private bool isBraking = false;
    private float pedalRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Update input values and rotate handlebar, pedals, and wheels
       GetInput();
        RotateHandlebar();
        RotatePedals();
        RotateWheels();
    }

    void FixedUpdate()
    {
        Move();
        Steer();
        Brake();
        CycleTilt();
       
    }

    
    void GetInput()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }
    public void OnAcceleration()
    {
        Debug.Log("Cycle accelerating");
        moveInput = 1;
    }

    public void OnAccelerationBack()
    {
        Debug.Log("Cycle acceleration back");
        moveInput = 0.01f;
    }

    public void OnDeceleration()
    {
        Debug.Log("Cycle decelerating");
        moveInput = -1;
    }

    public void OnDecelerationBack()
    {
        Debug.Log("Cycle deceleration back");
        moveInput = -0.01f;
    }

    public void Steer_Left()
    {
        Debug.Log("Cycle steering left");
        steerInput = -1;
    }

    public void Steer_Right()
    {
        Debug.Log("Cycle steering right");
        steerInput = 1;
    }

    public void Steer_Mid()
    {
        Debug.Log("Cycle steering mid");
        steerInput = 0;
    }

    public void OnBrake()
    {
        Debug.Log("Cycle braking");
        isBraking = true;
    }

    public void OnBrakeOff()
    {
        Debug.Log("Cycle brake off");
        isBraking = false;
    }
   
    void Move()
    {
        if (!isBraking)
        {
            Vector3 desiredVelocity = -transform.right * moveInput * maxSpeed;
            //Apply Acc force
            rb.velocity = Vector3.Lerp(rb.velocity, desiredVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            // Apply brake force
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, brakeForce * Time.deltaTime);
        }
    }

    void Steer()
    {
        // Rotate the bicycle based on steer input
        float turn = steerInput * steerAngle2 * moveInput *Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    void Brake()
    {
        if (isBraking || Input.GetKey(KeyCode.Space))
        {
            // Apply brake force
            SkidObj.GetComponentInChildren<TrailRenderer>().emitting = true;
            Vector3 brakeVelocity = rb.velocity * (1f - brakeForce * Time.fixedDeltaTime);
            
            rb.velocity = Vector3.Lerp(rb.velocity, brakeVelocity, Time.fixedDeltaTime * brakeForce);
        }
        else
        {
            SkidObj.GetComponentInChildren<TrailRenderer>().emitting = false;
        }
    }

    void RotateHandlebar()
    {
        if (handlebar != null)
        {
            handlebar.localRotation = Quaternion.Euler(0, steerInput * steerAngle, 0);
        }
    }
    void CycleTilt()
    {
        float targetTiltAngle = steerInput * tiltAngle * Mathf.Clamp(rb.velocity.magnitude / maxSpeed, 0, 1);
        Quaternion targetRotation = Quaternion.Euler(targetTiltAngle, transform.localRotation.eulerAngles.y, 0);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, Time.fixedDeltaTime * tiltSpeed));
    }
    void RotatePedals()
    {
        if (leftPedal != null && rightPedal != null)
        {
            pedalRotation += moveInput * pedalSpeed * Time.deltaTime;
            leftPedal.localRotation = Quaternion.Euler(0, 0, pedalRotation);
            rightPedal.localRotation = Quaternion.Euler(0, 0, pedalRotation);
        }
    }

    void RotateWheels()
    {
        if (frontWheel != null && rearWheel != null)
        {
            float wheelRotation = moveInput * wheelRotationSpeed * Time.deltaTime;
            frontWheel.localRotation = Quaternion.Euler(0, 0, wheelRotation);
            rearWheel.localRotation = Quaternion.Euler(0, 0, wheelRotation);
        }
    }
    private void OnTriggerEnter(Collider collison)
    {
        if (collison.gameObject.CompareTag("Finish"))
        {
            finish_manager.finish_fn();
            GameObject collideobjct = collison.gameObject;
            Destroy(collideobjct);
            isfinished = true;
            
        }
    }
}
