using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetGPS : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;
    [SerializeField] private GameObject TargetPrefab;
    [SerializeField] private LineRenderer Path;
    [SerializeField] private float PathHeightOffset = 1.25f;
    [SerializeField] private float PathUpdateSpeed = 0.25f;

    private Coroutine DrawPathCoroutine;
    public static JetGPS instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DrawPath();
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

        while (true)
        {
            if (PlayerPrefab != null && TargetPrefab != null)
            {
                
                float distance = Vector3.Distance(PlayerPrefab.transform.position, TargetPrefab.transform.position);
                Debug.Log($"Distance between Player and Target: {distance}");

                Path.positionCount = 2;
                Path.SetPosition(0, PlayerPrefab.transform.position + Vector3.up * PathHeightOffset);
                Path.SetPosition(1, TargetPrefab.transform.position + Vector3.up * PathHeightOffset);
            }
            else
            {
                Debug.Log("Player or Target is not set!");
            }

            yield return wait;
        }
    }
}
