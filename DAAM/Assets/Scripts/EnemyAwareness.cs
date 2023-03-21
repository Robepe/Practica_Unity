using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwareness : MonoBehaviour
{

    public float awarenessRadius = 8f;
    public bool isAggro;
    private bool isRunning;
    private bool isAttacking;

    private Transform playerTransform;
    public Animator enemyAnimator;

    void Start()
    {
        playerTransform = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        var distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance < awarenessRadius)
        {
            isAggro = true;
            enemyAnimator.SetBool("isAggro", isAggro);
            
        }
    }
}
