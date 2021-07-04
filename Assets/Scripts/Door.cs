using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isLocked;
    private bool isOpened;
    private Animator _animator;
    private bool isPlayerInRange;
    private bool isInteractionKeyPressed;
    void Start()
    {
        _animator = GetComponent<Animator>();
        isLocked = false;
        isOpened = false;
        isInteractionKeyPressed = false;
        isPlayerInRange = false;
    }
    
    void Update()
    {
        if (!isLocked && isPlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && !isInteractionKeyPressed)
            {
                //Debug.Log("Key to Open Door Pressed");
                isInteractionKeyPressed = true;
                isOpened = !isOpened;
                ChangeDoorState(isOpened);
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            isInteractionKeyPressed = false;
        }
    }

    private void ChangeDoorState(bool state)
    {
        if (state)
        {
            _animator.SetTrigger("Open");
        }
        else
        {
            _animator.SetTrigger("Close");
        }
        
    }

    public void CloseAndLock()
    {
        if (isOpened && !isLocked)
        {
            _animator.SetTrigger("Close");
            isLocked = true;
            return;
        }

        isLocked = true;
    }

    public void Unlock()
    {
        isLocked = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player is in Range");
            isPlayerInRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
    
}
