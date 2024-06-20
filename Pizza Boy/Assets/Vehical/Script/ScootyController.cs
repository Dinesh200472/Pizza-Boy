using UnityEngine;
using UnityEngine.XR;

public class ScootyController : MonoBehaviour, IVehicleController
{
    public static bool isfinished = false;
    public bool Done;
    RaycastHit hit;
    public float moveInput, steerInput, rayLength, currentVelocityOffset;
    public Vector3 velocity;
    public Rigidbody rb;
    public float maxSpeed, acceleration, steerStrength, tiltAngle,tiltSpeed, handleRotVal = 30f, handleRotSpeed = 0.15f;
    [Range(1, 10)]
    public float brakeForce;
    public GameObject Handle, SkidObj;
    public LayerMask derivableSurface;
    public bool isBrake;

    void Start()
    {
        Time.timeScale = 1;
        rayLength = GetComponent<CapsuleCollider>().height / 2 + 0.2f;
        velocity = rb.transform.InverseTransformDirection(rb.velocity);
    }

    void Update()
    {
        //GetInput();
        Done = isfinished;
       
    }

    void FixedUpdate()
    {
        Movement();
        ScootyTilt();
    }

    void GetInput()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    public void OnAcceleration()
    {
        AudioManager.instance.OnScooterMove(); 
        Debug.Log("OnAcceleration called");
        moveInput = 1;
    }

    public void OnAccelerationBack()
    {
        AudioManager.instance.StopScooterMove();
        Debug.Log("OnAccelerationBack called");
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
        Debug.Log("Steer_Left called");
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
        isBrake = true;
    }

    public void OnBrakeOff()
    {
        Debug.Log("OnBrakeOff called");
        isBrake = false;
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
    }

    void ScootyTilt()
    {
        float targetTiltAngle = steerInput * tiltAngle * Mathf.Clamp(rb.velocity.magnitude / maxSpeed, 0, 1);
        Quaternion targetRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y, targetTiltAngle);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, Time.fixedDeltaTime * tiltSpeed));
    }
    void Acceleration()
    {
        Debug.Log("Accelerating with moveInput: " + moveInput);
       
        rb.velocity = Vector3.Lerp(rb.velocity, moveInput * maxSpeed * transform.forward, Time.fixedDeltaTime * acceleration);
    }

    void Steering()
    {
        Debug.Log("Steering with steerInput: " + steerInput);
        transform.Rotate(0, steerInput * moveInput * steerStrength * Time.deltaTime, 0, Space.World);
        Handle.transform.localRotation = Quaternion.Slerp(Handle.transform.localRotation, Quaternion.Euler(-20, 0, handleRotVal * steerInput), handleRotSpeed);
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || isBrake)
        {
            rb.velocity *= brakeForce / 10;
            SkidObj.GetComponentInChildren<TrailRenderer>().emitting = true;
        }
        else
        {
            SkidObj.GetComponentInChildren<TrailRenderer>().emitting = false;
        }
    }

    bool Grounded()
    {
        if (Physics.Raycast(rb.position, Vector3.down, out hit, rayLength, derivableSurface))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            finish_manager.finish_fn();
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            Debug.Log("respawn");
            try
            {
                ui_manager.crashed();

            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
            }
        }
    }


    public bool OnTarget()
    {
        return Done;
    }


}
