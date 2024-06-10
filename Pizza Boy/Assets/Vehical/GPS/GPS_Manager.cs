using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class GPS_Manager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private GameObject TargetPrefab;
    [SerializeField] private LineRenderer Path;
    [SerializeField] private float PathHeightOffset = 1.25f;
    [SerializeField] private float PathUpdateSpeed = 0.25f;

    private GameObject PlayerInstance;
    private GameObject TargetInstance;
    private Coroutine DrawPathCoroutine;

    private void Start()
    {
        SpawnObjects();
    }

    public void SpawnObjects()
    {
        if (Demo.Instance == null)
        {
            Debug.LogError("ObjectTransferManager is not assigned!");
            return;
        }

        Vector3 playerPosition = Demo.Instance.PlayerPosition;
        Vector3 targetPosition = Demo.Instance.TargetPosition;

        PlayerInstance = Instantiate(PlayerPrefab, playerPosition, Quaternion.identity);
        TargetInstance = Instantiate(TargetPrefab, targetPosition, Quaternion.identity);

        if (DrawPathCoroutine != null)
        {
            StopCoroutine(DrawPathCoroutine);
        }
        DrawPathCoroutine = StartCoroutine(DrawPathToCollectable());
    }

    private IEnumerator DrawPathToCollectable()
    {
        WaitForSeconds wait = new WaitForSeconds(PathUpdateSpeed);
        NavMeshPath path = new NavMeshPath();

        while (true)
        {
            if (NavMesh.CalculatePath(PlayerInstance.transform.position, TargetInstance.transform.position, NavMesh.AllAreas, path))
            {
                Path.positionCount = path.corners.Length;
                for (int i = 0; i < path.corners.Length; i++)
                {
                    Path.SetPosition(i, path.corners[i] + Vector3.up * PathHeightOffset);
                }
            }
            else
            {
                Debug.LogError($"Unable to calculate path between {PlayerInstance.transform.position} and {TargetInstance.transform.position}!");
            }
            yield return wait;
        }
    }
}
