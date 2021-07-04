using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentAmount;

    public int receivedDamageAmount;

    public int maxAmount;

    public void Start()
    {
        Reset();
    }

    public void Decrease()
    {
        currentAmount -= receivedDamageAmount;
        if (currentAmount <= 0)
        {
            transform.gameObject.SetActive(false);
        }
    }

    public void Reset()
    {
        currentAmount = maxAmount;
    }
    
}
