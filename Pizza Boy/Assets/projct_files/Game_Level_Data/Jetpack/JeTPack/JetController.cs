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
    private Vector3 playerStartPosition;
    private bool isFlyingUp = false;
    private bool isFlyingDown = false;
    private bool isMovingInAir = false;

    void Start()
    {
        playerStartPosition = transform.position;
    }

    void Update()
    {
        HandleInput();
        if (transform.position.y < -335f)
        {
            transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }

        if(transform.position.y > -300f)
        {
            MoveInAir();
            MoveRotation();
        }
       
    }

    public void OnAcceleration()
    {
        if (!isFlyingUp && transform.position.y < flyHeight)
        {
            isFlyingUp = true;
            targetRotationUp = Quaternion.Euler(maxUpRotationAngle, transform.rotation.eulerAngles.y, 0);
        }
    }

    public void OnDeceleration()
    {
        if (!isFlyingDown && transform.position.y > 0)
        {
            isFlyingDown = true;
            targetRotationUp = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0);
        }
    }

    public void SteerLeft()
    {
        moveHorizontal = -1;
    }

    public void SteerRight()
    {
        moveHorizontal = 1;
    }

    public void SteerMid()
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

        moveHorizontal = Input.GetAxis("Horizontal") * controlSensitivity;
        moveVertical = Input.GetAxis("Vertical") * controlSensitivity;
    }
    void MoveRotation()
    {
        targetRotationMove = targetRotationUp;
        if (transform.position.y > -300f && (Input.GetAxis("Vertical") > 0))
        {
            targetRotationMove = Quaternion.Euler(maxMoveRotationAngle, transform.rotation.eulerAngles.y, 0);
        }
        else if (transform.position.y > -300f && (Input.GetAxis("Horizontal") > 0 || (Input.GetAxis("Horizontal") < 0)))
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
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, flyHeight, transform.position.z), Time.deltaTime * flySpeed);
        }
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

    void DescendToGround()
    {
        if (transform.position.y > -345f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,-338f, transform.position.z), Time.deltaTime * descendSpeed);
            targetRotationUp = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0);
        }
        else
        {
            isFlyingDown = false;
            isMovingInAir = false;
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
}
