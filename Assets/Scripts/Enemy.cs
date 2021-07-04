using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.AI;
using Panda;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Shooter shooterScript;
    private Transform playerTransform;
    [Task]
    private bool playerDetected;
    [Task]
    public bool alerted;
    [Task]
    private bool globalDetection;
    public UnityEvent AlertOthers;
    private Vector3 startPosition;
    private bool _canAlertOthers;
    public Vector3 lastPlayerPosition;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _canAlertOthers = false;
        startPosition = transform.position;
        playerDetected = false;
        alerted = false;
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, (playerTransform.position-transform.position).normalized);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, -1, QueryTriggerInteraction.Ignore) && hit.transform.CompareTag("Player"))
        {
            globalDetection = false;
            playerDetected = true;
            alerted = true;
            lastPlayerPosition = playerTransform.position;
            Debug.DrawRay(transform.position, playerTransform.position - transform.position, Color.green);
        }
        else
        {
            playerDetected = false;
            Debug.DrawRay(transform.position, playerTransform.position - transform.position, Color.red);
        }
    }

    [Task]
    void Chase()
    {
        agent.SetDestination(lastPlayerPosition);
        Task.current.Succeed();
    }

    [Task]
    void Shoot()
    {
        shooterScript.Shoot((new Vector3(lastPlayerPosition.x, 1, lastPlayerPosition.z) - new Vector3(transform.position.x, 1 ,transform.position.z)).normalized);
        Task.current.Succeed();
    }

    [Task]
    void CheckAround()
    {
        agent.SetDestination(transform.position + Random.rotation*Vector3.forward*3);
        Task.current.Succeed();
    }

    [Task]
    void CheckLastPosition()
    {
        agent.SetDestination(lastPlayerPosition);
        if (Vector3.Distance(transform.position ,lastPlayerPosition) < 0.5 || agent.isPathStale)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    void ReturnToOrigin()
    {
        agent.SetDestination(startPosition);
        if (transform.position == startPosition)
        {
            Task.current.Succeed();
        }
    }
    
    public bool PlayerDetected => playerDetected;
    

    public bool GlobalDetection
    {
        get => globalDetection;
        set => globalDetection = value;
    }

    public bool Alerted
    {
        get => alerted;
        set => alerted = value;
    }
}