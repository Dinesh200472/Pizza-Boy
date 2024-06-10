// IVehicleController.cs
public interface IVehicleController
{
    void OnAcceleration();
    void OnAccelerationBack();
    void OnDeceleration();
    void OnDecelerationBack();
    void Steer_Left();
    void Steer_Right();
    void Steer_Mid();
    void OnBrake();
    void OnBrakeOff();
}
