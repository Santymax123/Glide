using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{
    public float awarenessRadius = 1;
    public bool isAggro;

    public Material aggroMat;
    private Transform playersTransform;

    private void Start()
    {
        var player = FindObjectOfType<PlayerMove>();
        if (player != null)
        {
            playersTransform = player.transform;
        }
        else
        {
            Debug.LogError("PlayerMove script not found in the scene.");
        }
    }


    private void Update()
    {
        if (playersTransform == null) return;
        var dist = Vector3.Distance(transform.position ,playersTransform.position);

        if (dist <= awarenessRadius)
        {
            isAggro = true;
        }

        if (isAggro)
        {
            GetComponent<MeshRenderer>().material = aggroMat;
        }
    }
}
