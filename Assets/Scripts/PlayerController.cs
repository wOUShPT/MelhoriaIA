using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    [SerializeField]private Camera cam;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Shooter shooterScript;
    public GameObject gameOverScreen;
    private Animator _animator;
    private Vector3 _currentVelocity;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        Physics.IgnoreLayerCollision(6, 10);
        Physics.IgnoreLayerCollision(9, 11);
        Physics.IgnoreLayerCollision(10, 11);
    }

    void Update()
    {
        _currentVelocity = new Vector3(-Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal")).normalized*movementSpeed;
       
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = new Vector3(mousePosition.x, 1, mousePosition.z);
            shooterScript.Shoot((mousePosition - new Vector3(transform.position.x, 1, transform.position.z)).normalized);
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = _currentVelocity;
    }


    private void OnDisable()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }
}