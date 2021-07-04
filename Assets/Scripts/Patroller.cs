using System.Collections;
using System.Collections.Generic;
using Panda;
using UnityEngine;

public class Patroller : MonoBehaviour
{
    private Enemy _enemy;
    private List<Watcher> _enemyStationaries;
    private Transform _playerTransform;

    void Start()
    {
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
}
