using UnityEngine;

public class BikeController_Normal : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 100f;

    public void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void MoveBackward()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
    }

    public void TurnLeft()
    {
        transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
    }

    public void TurnRight()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
