using UnityEngine;

public class VehicleUIManager : MonoBehaviour
{
    public GameObject GasDecrease;

    private bool isMovingForward = false;
    private bool isMovingBackward = false;
    private bool isTurningRight = false;
    private bool isTurningLeft = false;
    private bool isBraking = false;
    private bool isGas = false;
    public static VehicleUIManager instance;

    [SerializeField] private GameObject activeVehicle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
       
        GameObject x = GameObject.FindWithTag("Car");
        GetPlayerVehicle(x);
    }

    public void GetPlayerVehicle(GameObject temp)
    {
        activeVehicle = temp;
    }

    private void Update()
    {
        if (activeVehicle == null)
        {
            if (activeVehicle == null)
            {
                Debug.LogWarning("No active vehicle found.");
                return;
            }
        }
        if (JetController.isJet)
        {
            GasDecrease.SetActive(true);
        }
        else
        {
            GasDecrease.SetActive(false);
        }

        HandleVehicleControls();
    }

    private void HandleVehicleControls()
    {
        if (isMovingForward)
        {
            activeVehicle.SendMessage("OnAcceleration", SendMessageOptions.DontRequireReceiver);
        }

        if (isMovingBackward)
        {
            activeVehicle.SendMessage("OnDeceleration", SendMessageOptions.DontRequireReceiver);
        }

        if (isTurningRight)
        {
            activeVehicle.SendMessage("Steer_Right", SendMessageOptions.DontRequireReceiver);
        }

        if (isTurningLeft)
        {
            activeVehicle.SendMessage("Steer_Left", SendMessageOptions.DontRequireReceiver);
        }

        if (isBraking)
        {
            activeVehicle.SendMessage("OnBrake", SendMessageOptions.DontRequireReceiver);
        }
        if(isGas)
        {
            activeVehicle.SendMessage("OnGasPressed", SendMessageOptions.DontRequireReceiver);
        }
    }


    public void OnGasPress()
    {
        isGas = true;
    }

    public void OnGasReleased()
    {
        isGas = false;
        activeVehicle.SendMessage("OnGasReleased", SendMessageOptions.DontRequireReceiver);
    }


    public void OnBrakePressed()
    {
        isBraking = true;
    }

    public void OnBrakeReleased()
    {
        isBraking = false;
        activeVehicle.SendMessage("OnBrakeOff", SendMessageOptions.DontRequireReceiver);
    }

    public void OnForwardButtonPressed()
    {
        isMovingForward = true;
    }

    public void OnForwardButtonReleased()
    {
        isMovingForward = false;
        activeVehicle.SendMessage("OnAccelerationBack", SendMessageOptions.DontRequireReceiver);
    }

    public void OnBackwardButtonPressed()
    {
        isMovingBackward = true;
    }

    public void OnBackwardButtonReleased()
    {
        isMovingBackward = false;
        activeVehicle.SendMessage("OnDecelerationBack", SendMessageOptions.DontRequireReceiver);
    }

    public void OnRightButtonPressed()
    {
        isTurningRight = true;
    }

    public void OnRightButtonReleased()
    {
        isTurningRight = false;
        activeVehicle.SendMessage("Steer_Mid", SendMessageOptions.DontRequireReceiver);
    }

    public void OnLeftButtonPressed()
    {
        isTurningLeft = true;
    }

    public void OnLeftButtonReleased()
    {
        isTurningLeft = false;
        activeVehicle.SendMessage("Steer_Mid", SendMessageOptions.DontRequireReceiver);
    }
}
