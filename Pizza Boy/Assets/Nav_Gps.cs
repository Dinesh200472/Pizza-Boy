using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nav_Gps : MonoBehaviour
{

    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private GameObject TargetPrefab;
    [SerializeField] private LineRenderer Path;
    [SerializeField] private float PathHeightOffset = 1.25f;
    [SerializeField] private float PathUpdateSpeed = 0.25f;
    private Coroutine DrawPathCoroutine;
    public static Nav_Gps instance;

    private void Awake()
    {
        instance = this;
        TargetPrefab = GameObject.FindWithTag("Finish");
    }

    private void Start()
    {
        GameObject car = GameObject.FindWithTag("Car");
        GetActivePlayer(car);
    }

    public void GetActivePlayer(GameObject player)
    {
        PlayerPrefab = player;
        if (TargetPrefab == null)
        {
       
            TargetPrefab = GameObject.FindWithTag("Finish");
            if (TargetPrefab == null)
            
            {
                Debug.LogError("TargetPrefab is not assigned and no target object available from VehicleManager.");
                return;
            }
        }
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
            if (NavMesh.CalculatePath(PlayerPrefab.transform.position, TargetPrefab.transform.position, NavMesh.AllAreas, path))
            {
                Path.positionCount = path.corners.Length;
                for (int i = 0; i < path.corners.Length; i++)
                {
                    Path.SetPosition(i, path.corners[i] + Vector3.up * PathHeightOffset);
                }
            }
            else
            {
                Debug.LogError($"Unable to calculate path between {PlayerPrefab.transform.position} and {TargetPrefab.transform.position}!");
            }
            yield return wait;
        }
    }
}
