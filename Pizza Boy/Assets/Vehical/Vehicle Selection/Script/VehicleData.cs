using UnityEngine;

[CreateAssetMenu(fileName = "VehicleData", menuName = "ScriptableObjects/VehicleData", order = 1)]
public class VehicleData : ScriptableObject
{
    public GameObject vehiclePrefab;
    public VehicleType vehicleType;
}

public enum VehicleType
{
    Cycle,
    Bike,
    Car
}
