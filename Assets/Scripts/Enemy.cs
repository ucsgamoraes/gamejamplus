using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent navMeshAgent;
    public float raycastDistance = 5f;
    public LayerMask hitLayers;

    public int damagePerHit;
    public float hitInterval;
    public float hitTimer;
    public float stopDistance;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    GameObject GetClosestPlant(ref float dist)
    {
        float minDist = float.MaxValue;
        GameObject minPlant = null;

        foreach (GameObject plant in PlantManager.Instance.allPlants)
        {
            float currentDist = Vector3.Magnitude(transform.position - plant.transform.position);
            if (currentDist < minDist)
            {
                minDist = currentDist;
                minPlant = plant;
            }
        }
        dist = minDist;
        return minPlant;
    }

    void Update()
    {
        float dist = 0.0f;
        Vector3 target = GetClosestPlant(ref dist).transform.position;
        navMeshAgent.SetDestination(target);
        Debug.Log(dist);
        navMeshAgent.isStopped = dist <= stopDistance;

        // Perform raycast to detect objects with the "Plant" tag.
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, hitLayers))
        {
            if (hit.collider.CompareTag("Plant"))
            {
                Plant plant = hit.collider.GetComponent<Plant>();
                if (plant != null && hitTimer >= hitInterval)
                {
                    plant.TakeDamage(damagePerHit);
                    hitTimer = 0.0f;
                }
            }
        }


        hitTimer += Time.deltaTime;
    }
}