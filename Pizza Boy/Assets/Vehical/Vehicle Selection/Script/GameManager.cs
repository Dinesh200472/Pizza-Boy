using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPos;
    public static GameManager Instance;

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

    void Start()
    {
        /* Make sure SpawnPos array is populated correctly
        if (spawnPos == null || spawnPos.Length == 0)
        {
            Debug.LogError("Spawn positions not set in GameManager.");
        }
        else
        {
            Debug.Log("Spawn positions initialized correctly.");
        }*/
    }

    public Transform Spawn_Vehicle()
    {
        if (spawnPos == null || spawnPos.Length == 0)
        {
            Debug.LogError("Spawn positions array is empty.");
            return null;
        }
        return spawnPos[0];
    }
}
