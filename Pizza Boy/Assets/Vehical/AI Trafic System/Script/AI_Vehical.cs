using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Vehicle : MonoBehaviour
{
    public List<Transform> forwardWaypoints;
    public float waypointRadius = 1.0f;
    public float obstacleDetectionDistance = 5.0f;
    public float stopDistance = 2.0f;
    public float slowDownSpeed = 2.0f;
    public float resumeSpeed = 5.0f;
    public float playerDetectionRadius = 10.0f; // Radius to detect the player
    public float playerStopDistance = 3.0f; // Distance to stop when player is very close
    public float playerSlowDownDistance = 7.0f; // Distance to slow down when player is nearby

    private NavMeshAgent agent;
    private int currentWaypointIndex;
    private bool isStopped;
    private bool playerDetected;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentWaypointIndex = 0;
        isStopped = false;
        playerDetected = false;

        if (forwardWaypoints.Count > 0)
        {
            agent.SetDestination(forwardWaypoints[currentWaypointIndex].position);
            agent.speed = resumeSpeed;
        }
    }

    void Update()
    {
        if (forwardWaypoints.Count == 0) return;

        HandleObstacleDetection();
        //HandlePlayerDetection();

        if (!isStopped && !playerDetected)
        {
            MoveToNextWaypoint();
        }

        DrawRaycastLineFromCenter();
    }

    void HandleObstacleDetection()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        Vector3 raycastDirection = transform.forward;

        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, obstacleDetectionDistance))
        {
            DrawRaycastLine(raycastOrigin, hit.point);

            if (hit.transform.CompareTag("Vehicle"))
            {
                float distanceToObstacle = hit.distance;

                if (distanceToObstacle < stopDistance)
                {
                    StopVehicle();
                }
                else
                {
                    SlowDownVehicle();
                }
            }else if(hit.transform.CompareTag("Player"))
            {
                float distanceToObstacle = hit.distance;

                if (distanceToObstacle < stopDistance)
                {
                    StopVehicle();
                }
                else
                {
                    SlowDownVehicle();
                }
            }
        }
        else
        {
            DrawRaycastLine(raycastOrigin, raycastOrigin + raycastDirection * obstacleDetectionDistance);

            ResumeVehicle();
        }
    }

    void HandlePlayerDetection()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, playerDetectionRadius);
        bool playerNearby = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                playerNearby = true;
                float distanceToPlayer = Vector3.Distance(transform.position, hitCollider.transform.position);

                if (distanceToPlayer < playerStopDistance)
                {
                    StopVehicle();
                }
                else if (distanceToPlayer < playerSlowDownDistance)
                {
                    SlowDownVehicle();
                }
                else
                {
                    ResumeVehicle();
                }

                break;
            }
        }

        if (!playerNearby && playerDetected)
        {
            playerDetected = false;
            ResumeVehicle();
        }

        playerDetected = playerNearby;
    }

    void DrawRaycastLine(Vector3 start, Vector3 end)
    {
        Debug.DrawLine(start, end, Color.red);
    }

    void DrawRaycastLineFromCenter()
    {
        Vector3 vehicleCenter = GetComponent<Collider>().bounds.center;
        Vector3 raycastDirection = transform.forward * obstacleDetectionDistance;
        Debug.DrawLine(vehicleCenter, vehicleCenter + raycastDirection, Color.blue);
    }

    void MoveToNextWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, forwardWaypoints[currentWaypointIndex].position);

        if (distanceToWaypoint < waypointRadius)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= forwardWaypoints.Count)
            {
                currentWaypointIndex = 0; // Loop back to the first waypoint
            }

            agent.SetDestination(forwardWaypoints[currentWaypointIndex].position);
        }
    }

    void StopVehicle()
    {
        if (!isStopped)
        {
            agent.isStopped = true;
            isStopped = true;
        }
    }

    void SlowDownVehicle()
    {
        if (!isStopped)
        {
            agent.speed = slowDownSpeed;
        }
    }

    void ResumeVehicle()
    {
        if (isStopped)
        {
            agent.isStopped = false;
            isStopped = false;
            agent.SetDestination(forwardWaypoints[currentWaypointIndex].position);
        }

        agent.speed = resumeSpeed;
    }
}
