using UnityEngine;

public class Demo : MonoBehaviour
{
    public static Demo Instance { get; private set; }

    public Vector3 PlayerPosition;
    public Vector3 TargetPosition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPositionPlayer(Vector3 playerPos)
    {
        PlayerPosition = playerPos;
        
    }
    public void SetPositionTarget(Vector3 targetPos)
    {
        TargetPosition = targetPos;
    }
}
