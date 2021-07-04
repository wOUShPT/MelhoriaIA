using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    public CollisionEvent _projectileCollisionEvent;

    private void Awake()
    {
        _projectileCollisionEvent = new CollisionEvent();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out Health health))
        {
            health.Decrease();
        }
        _projectileCollisionEvent.Invoke(transform.position);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(90, 0, 0);
        transform.parent.gameObject.SetActive(false);
    }
}

public class CollisionEvent : UnityEvent<Vector3>
{
    
}
