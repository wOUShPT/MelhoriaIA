using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject prefab;
    public Transform poolParent;
    public int poolCapacity;
    public List<GameObject> pool;
    void Awake()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolCapacity; i++)
        {
            GameObject projectile = Instantiate(prefab);
            projectile.transform.SetParent(poolParent.transform);
            pool.Add(projectile);
            projectile.SetActive(false);
        }
    }

    public GameObject GetFromPool()
    {
        foreach (var gameObject in pool)
        {
            if (!gameObject.activeSelf)
            {
                return gameObject;
            }
        }

        return null;
    }
}
