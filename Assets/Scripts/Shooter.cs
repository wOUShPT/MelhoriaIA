using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Health healthScript;
    public string opponentProjectileTag;
    public float projectileSpeed;
    public float projectileFireRate;
    public Pool projectilesPool;
    public Transform shootOrigin;
    private bool shotProjectile;
    private float shootTimer;
    void Start()
    {
        shotProjectile = false;
        shootTimer = 0;
    }
    
    void Update()
    {
        if (shotProjectile)
        {
            shootTimer = 0;
            shotProjectile = false;
        }
        
        shootTimer += Time.deltaTime;
    }

    public void Shoot(Vector3 direction)
    {
        if (shootTimer >= projectileFireRate)
        {
            shotProjectile = true;
            GameObject projectile = projectilesPool.GetFromPool();
            projectile.SetActive(true);
            projectile.transform.position = shootOrigin.position;
            projectile.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        
            Rigidbody _projectileRB = projectile.GetComponentInChildren<Rigidbody>();
            _projectileRB.velocity = Vector3.zero;
            _projectileRB.angularVelocity = Vector3.zero;
            _projectileRB.velocity = direction * projectileSpeed;
        }
    }
}
