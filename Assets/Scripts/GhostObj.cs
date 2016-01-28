using UnityEngine;
using System.Collections;

public class GhostObj : MonoBehaviour {

    public GameController controller;

    private enum GhostState
    {
        Wander, Pursue
    }
    private GhostState state;
    
    public float maxWalkRadius;
    private NavMeshAgent agent;
    private Vector3 destination;

    private bool canCurrentlySeePlayer;

	void Start () {
        state = GhostState.Wander;
        agent = transform.parent.GetComponent<NavMeshAgent>();
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        SetNewDestination();
    }

    void Update()
    {
        if(state == GhostState.Wander)
        {
            if (agent.remainingDistance < 1.0f)
                SetNewDestination();
        }
    }

    void FixedUpdate()
    {
        if (CanSeePlayer())
        {
            controller.GhostCanSeePlayer();
            ChangeState(GhostState.Pursue);
            canCurrentlySeePlayer = true;
        }
        else if (canCurrentlySeePlayer)
        {
            canCurrentlySeePlayer = false;
            ChangeState(GhostState.Wander);
        }
    }

    private void ChangeState(GhostState newState)
    {
        if(newState == GhostState.Pursue)
        {
            state = GhostState.Pursue;
            agent.destination = controller.mainCharacter.transform.position;

        } else if(newState == GhostState.Wander)
        {
            state = GhostState.Wander;
            SetNewDestination();
        }
    }

    private bool CanSeePlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, controller.mainCharacter.transform.position - transform.position, out hit))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    private void SetNewDestination()
    {
        agent.destination = controller.GetRandomPointWithinRadius(maxWalkRadius, transform.parent.transform.position);
    }
}
