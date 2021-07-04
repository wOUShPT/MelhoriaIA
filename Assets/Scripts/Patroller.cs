using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Panda;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class Patroller : MonoBehaviour
{
    private Enemy _enemy;
    private List<Watcher> _enemyStationaries;
    private Transform _playerTransform;
    public List<Transform> waypoints;
    public Transform currentWaypoint;
    private int _currentWaypointIndex;
    [Task]
    private bool hasArrived;

    void Start()
    {
        _currentWaypointIndex = 0;
        _enemy = GetComponent<Enemy>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void AwareOfPlayer()
    {
        _enemy.GlobalDetection = true;
        _enemy.Alerted = true;
        _enemy.lastPlayerPosition = _playerTransform.position;
    }
    
    [Task]
    void ForgetPlayer()
    {
        _enemy.alerted = false;
        Task.current.Succeed();
    }

    [Task]
    void SetWaypoint()
    {
        if (!_enemy.agent.hasPath)
        {
            if (_currentWaypointIndex == waypoints.Count)
            {
                _currentWaypointIndex = 0;
            }
            currentWaypoint = waypoints[_currentWaypointIndex];
            _currentWaypointIndex++;
            Task.current.Succeed();
        }
    }

    [Task]
    void Patrol()
    {
        _enemy.agent.SetDestination(currentWaypoint.position);
        Task.current.Succeed();
    }

    [Task]
    void CheckArrival()
    {
        float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), currentWaypoint.position);
        if (distance < 0.1f)
        {
            Task.current.Succeed();
        }
    }
}
