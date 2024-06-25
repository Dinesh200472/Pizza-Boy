using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class AI_Vehicle : MonoBehaviour
{
    public List<Transform> forwardWaypoints;
    public float waypointRadius = 1.0f;
    public float obstacleDetectionDistance = 5.0f;
    public float stopDistance = 2.0f;
    public float slowDownSpeed = 2.0f;
    public float resumeSpeed = 5.0f;
    public LayerMask obstacleLayers; // Layer mask to detect obstacles
    public float sphereCastRadius = 1.0f; // Radius of the sphere cast

    private NavMeshAgent agent;
    private int currentWaypointIndex;
    private bool isStopped;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentWaypointIndex = 0;
        isStopped = false;

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

        if (!isStopped)
        {
            MoveToNextWaypoint();
        }

        // Uncomment this if you want to visualize the raycast
        // DrawRaycastLineFromCenter();
    }

    void HandleObstacleDetection()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        Vector3 raycastDirection = transform.forward;

        // Debugging line to visualize the sphere cast in the Scene view
        DrawSphereCastLine(raycastOrigin, raycastOrigin + raycastDirection * obstacleDetectionDistance, sphereCastRadius);

        if (Physics.SphereCast(raycastOrigin, sphereCastRadius, raycastDirection, out hit, obstacleDetectionDistance, obstacleLayers))
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
        else
        {
            ResumeVehicle();
        }
    }

    void DrawSphereCastLine(Vector3 start, Vector3 end, float radius)
    {
        Debug.DrawLine(start, end, Color.red);
        // Drawing spheres at the start and end to visualize the sphere cast radius
        Debug.DrawRay(start, Vector3.up * radius, Color.red);
        Debug.DrawRay(start, Vector3.down * radius, Color.red);
        Debug.DrawRay(end, Vector3.up * radius, Color.red);
        Debug.DrawRay(end, Vector3.down * radius, Color.red);
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
