using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public static VehicleManager Instance;
    public VehicleData[] vehicleDataArray;
    private GameObject activeVehicle;
    public GameObject[] targets; // Ensure this is populated in the inspector
    [SerializeField] private Transform spawnPos;
    public int index;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure the VehicleManager persists across scenes if needed
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SelectVehicle();
    }

    public void SelectVehicle()
    {
        if (VehicleSelection.Instance == null)
        {
            Debug.LogError("VehicleSelection.Instance is null.");
            return;
        }

        index = VehicleSelection.Instance.CurrentPosition();
        Debug.Log("Selected vehicle index: " + index);

       

        if (spawnPos == null)
        {
            Debug.LogError("Spawn position is null.");
            return;
        }

        Debug.Log("Spawn position: " + spawnPos.position);

        if (index < 0 || index >= vehicleDataArray.Length)
        {
            Debug.LogError("Invalid vehicle index.");
            return;
        }

        if (activeVehicle != null)
        {
            Destroy(activeVehicle);
        }

        if (vehicleDataArray[index] != null)
        {
            Debug.Log("Instantiating vehicle prefab: " + vehicleDataArray[index].vehiclePrefab.name);
            activeVehicle = Instantiate(vehicleDataArray[index].vehiclePrefab, spawnPos.position, Quaternion.identity);
            CollectableSpawner.instance.GetActivePlayer(activeVehicle);
            VehicleUIManager.instance.GetPlayerVehicle(activeVehicle);
        }
        else
        {
            Debug.LogError("Vehicle prefab is null.");
        }
    }

    public GameObject GetActiveTarget()
    {
        if (targets == null || targets.Length == 0)
        {
            Debug.LogError("No targets available.");
            return null;
        }
        return targets[0];
    }

    public GameObject GetActiveVehicle()
    {
        return activeVehicle;
    }
}
