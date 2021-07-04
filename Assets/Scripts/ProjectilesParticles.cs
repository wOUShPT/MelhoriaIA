using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectilesParticles : MonoBehaviour
{
    public int poolCapacity;
    public ParticleSystem particlesPrefab;
    public string projectilesTag;
    public GameObject particlesPool;
    private List<ParticleSystem> _particlesPool;
    void Start()
    {
        _particlesPool = new List<ParticleSystem>();
        for (int i = 0; i < poolCapacity; i++)
        {
            ParticleSystem particles = Instantiate(particlesPrefab);
            particles.transform.SetParent(particlesPool.transform);
            particles.gameObject.SetActive(false);
        }

        List<GameObject> projectileList = GameObject.FindGameObjectsWithTag(projectilesTag).ToList();
        foreach (var projectile in projectileList)
        {
            projectile.GetComponent<Projectile>()._projectileCollisionEvent.AddListener(SetParticles);
        }
    }

    public ParticleSystem GetFromPool()
    {
        foreach (var particles in _particlesPool)
        {
            if (!particles.gameObject.activeSelf)
            {
                return particles;
            }
        }

        return null;
    }

    public void SetParticles(Vector3 position)
    {
        ParticleSystem particles = GetFromPool();
        particles.gameObject.SetActive(true);
        particles.transform.position = position;
        particles.Play();
    }
    
}
