using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Panda;
using UnityEngine;
using UnityEngine.Events;

public class Watcher : MonoBehaviour
{
    private Enemy _enemy;
    public UnityEvent AlertOthers;
    private List<Patroller> _enemyPatrollers;
    private List<Watcher> _enemyWatchers;
    private Transform _playerTransform;
    private List<Door> _doors;
   
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _doors = FindObjectsOfType<Door>().ToList();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        AlertOthers = new UnityEvent();
        _enemyPatrollers = FindObjectsOfType<Patroller>().ToList();
        foreach (var enemy in _enemyPatrollers)
        {
            AlertOthers.AddListener(enemy.AwareOfPlayer);
        }

        _enemyWatchers = FindObjectsOfType<Watcher>().ToList();
        foreach (var enemy in _enemyWatchers)
        {
            if (enemy != this)
            {
                AlertOthers.AddListener(enemy.AwareOfPlayer);
            }
        }
    }
    
    void Update()
    {
        if (_enemy.PlayerDetected)
        {
            LockDoors();
            AlarmAll();
        }
    }
    
    void AlarmAll()
    {
        AlertOthers.Invoke();
    }

    void LockDoors()
    {
        foreach (var door in _doors)
        {
            door.CloseAndLock();
        }
    }

    void UnlockDoors()
    {
        foreach (var door in _doors)
        {
            door.Unlock();
        }
    }
    
    [Task]
    void ForgetPlayer()
    {
        _enemy.alerted = false;
        UnlockDoors();
        Task.current.Succeed();
    }
    
    public void AwareOfPlayer()
    {
        _enemy.GlobalDetection = true;
        _enemy.Alerted = true;
        _enemy.lastPlayerPosition = _playerTransform.position;
    }
}
