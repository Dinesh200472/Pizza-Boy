using UnityEngine;

public class JetController : MonoBehaviour
{
    
    public float GravitySpeed;
    public float flyHeight = 10f;
    public float flySpeed = 10f;
    public float descendSpeed = 5f;
    public float rotationSpeed = 2f;
    public float controlSensitivity = 1f;
    public float maxUpRotationAngle = 30f;
    public float maxMoveRotationAngle = 15f;
    public float flyMoveSpeed = 5f;
    public float gravitySpeed = 5f;
    private float moveHorizontal, moveVertical;
    private Quaternion targetRotationUp, targetRotationMove;
  
    public static bool isfinished;
    private bool Done;

    void Start()
    {

    }

    void Update()
    {
        Done = isfinished;
       // GetInput();
        //HandleInput();
        if (transform.position.y < -335f)
        {
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
    }

    private void FixedUpdate()
    {
        if(transform.position.y > -295f)
        {
            MoveInAir();
            MoveRotation();
        }
    }

    void GetInput()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
    }
    void MoveInAir()
    {
        
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        
        if (movement != Vector3.zero)
        {
            Quaternion targetDir = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetDir, Time.deltaTime * rotationSpeed);
        }

        transform.Translate(movement * flyMoveSpeed * Time.deltaTime, Space.World);
    }
    public void OnBrake()
    {
        if (transform.position.y < flyHeight)
        {
            targetRotationUp = Quaternion.Euler(maxUpRotationAngle, transform.rotation.eulerAngles.y, 0);
            FlyToHeight();
            RotationJet();
        }
    }

    public void OnBrakeOff()
    {
        Gravity_Down();
    }

    public void OnAcceleration()
    {

        AudioManager.instance.OnJetMove();
        moveVertical = 1;

    }
    public void OnAccelerationBack()
    {
        AudioManager.instance.StopJetMove();
        moveVertical = 0.1f;
    }

    public void OnDeceleration()
    {
        AudioManager.instance.OnJetMove();
        if (transform.position.y > 0)
        {
            targetRotationUp = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0);
            DescendToGround();
            RotationJet();
        }
    }
    public void OnDecelerationBack()
    {
        AudioManager.instance.StopJetMove();
    }

    public void Steer_Left()
    {
        moveHorizontal = -1;
    }

    public void Steer_Right()
    {
        moveHorizontal = 1;
    }

    public void Steer_Mid()
    {
        moveHorizontal = 0;
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            FlyToHeight();
            RotationJet();
        }
        else if (Input.GetKey(KeyCode.F))
        {
            DescendToGround();
            RotationJet();
        }

       
    }
    void MoveRotation()
    {
        targetRotationMove = targetRotationUp;
        if (transform.position.y > -300f && moveVertical > 0)
        {
            targetRotationMove = Quaternion.Euler(maxMoveRotationAngle, transform.rotation.eulerAngles.y, 0);
        }
        else if (transform.position.y > -300f && ((moveHorizontal < 0) || (moveHorizontal >0)))
        {
            targetRotationMove = Quaternion.Euler(maxMoveRotationAngle, transform.rotation.eulerAngles.y, 0);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotationMove, Time.deltaTime * rotationSpeed);
    }

    void RotationJet()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotationUp, Time.deltaTime * rotationSpeed);
    }
    void FlyToHeight()
    {
        if (transform.position.y < flyHeight)
        {
            AudioManager.instance.OnJetMove();
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, flyHeight, transform.position.z), Time.deltaTime * flySpeed);
        }
    }

   

    void DescendToGround()
    {
        if (transform.position.y > -345f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,-338f, transform.position.z), Time.deltaTime * descendSpeed);
            targetRotationUp = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0);
        }
    }


    void Gravity_Down()
    {
        if(transform.position.y < -335f)
        {
            return;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotationUp, Time.deltaTime * 0.02f);
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -335f, transform.position.z), Time.deltaTime * GravitySpeed);
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
