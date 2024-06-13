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
    }

    private void Start()
    {

    }
    private void Update()
    {
        if (PlayerPrefab != null && TargetPrefab != null)
        {
            DrawPath();
        }
        else
        {
            GetActivePlayer();
            GetActiveTarget();
        }
    }
    public void GetActiveTarget()
    {
        if (TargetPrefab == null)
        {
            int i = 0;
            GameObject targetObject = GameObject.FindWithTag("Finish");
            TargetPrefab = targetObject;
            Debug.Log("Active Target " + ++i);
        }
    }
    public void GetActivePlayer()
    {
        int i = 0;
        GameObject car = GameObject.FindWithTag("Car");
        PlayerPrefab = car;
        Debug.Log("Active Player " + ++i);
    }

    public void DrawPath()
    {

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
