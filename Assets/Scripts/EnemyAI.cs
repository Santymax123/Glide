using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private EnemyAwareness enemyAwareness;
    private Transform playersTransform;
    private UnityEngine.AI.NavMeshAgent enemyNavMeshAgent;



    private void Start()
    {
        enemyAwareness = GetComponent<EnemyAwareness>();
        //if (enemyAwareness == null) Debug.LogError("EnemyAwareness component not found!");

        var player = FindObjectOfType<PlayerMove>();
        if (player != null)
        {
            playersTransform = player.transform;
        }
        else
        {
            Debug.LogError("No PlayerMove object found in the scene!");
        }

        enemyNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //if (enemyNavMeshAgent == null) Debug.LogError("NavMeshAgent component not found!");
    }



    private void Update()
    {
        if (enemyAwareness == null){
            return;
        }
        if (enemyAwareness.isAggro)
        {
            enemyNavMeshAgent.SetDestination(playersTransform.position);
        }
        else
        {
            enemyNavMeshAgent.SetDestination(transform.position);
        }
    }
}
