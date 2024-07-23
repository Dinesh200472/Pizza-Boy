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
    private bool isCycle = false;
    private bool isScooter = false;
    private bool isAuto = false;
    private bool isJet = false;
    private AudioClip General;
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
        isCycle = false;
        isScooter = false;
        isAuto = false;
        isJet = false;
        GameObject x = GameObject.FindWithTag("Car");
        GetPlayerVehicle(x);
    }

    public void GetPlayerVehicle(GameObject temp)
    {
        activeVehicle = temp;
        TypeOfVehicle type = activeVehicle.GetComponent<TypeOfVehicle>();
        if(type !=null)
        {
            TypeVehicle vehicle = type.Type;
            if(TypeVehicle.Cycle == vehicle)
            {
                isCycle = true;
            }
            else if(TypeVehicle.Scooty == vehicle)
            {
                isScooter = true;
            }
            else if(TypeVehicle.Auto == vehicle)
            {
                isAuto = true;
            }
            else if(TypeVehicle.Jet == vehicle)
            {
                isJet = true;
            }
        }
    }

    private void Update()
    {
        if(isCycle)
        {
            General = AudioManager.instance.cycle;
        }
        if (isScooter)
        {
            General = AudioManager.instance.scooter;
        }
        if (isAuto)
        {
            General = AudioManager.instance.auto;
        }
        if(isJet)
        {
            General = AudioManager.instance.jet;
        }
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
        AudioManager.instance.PlayAcc(General);
    }

    public void OnGasReleased()
    {
        isGas = false;
        AudioManager.instance.PlayOff();
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
        AudioManager.instance.PlayAcc(General);
    }

    public void OnForwardButtonReleased()
    {
        isMovingForward = false;
        AudioManager.instance.PlayOff();
        activeVehicle.SendMessage("OnAccelerationBack", SendMessageOptions.DontRequireReceiver);
    }

    public void OnBackwardButtonPressed()
    {
        isMovingBackward = true;
        AudioManager.instance.PlayAcc(General);
    }

    public void OnBackwardButtonReleased()
    {
        isMovingBackward = false;
        AudioManager.instance.PlayOff();
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
