using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private Animator _animator;
    private Rigidbody _rb;

    private float _speed;
    [SerializeField] private float _maxSpeed;
    private void Start()
    {
        _speed = _maxSpeed;
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float movementDirection = Input.GetAxis("Vertical");

        _animator.SetBool("walk", movementDirection > 0);
        _animator.SetBool("run", Input.GetKey(KeyCode.LeftShift));

        if (movementDirection > 0)
        {
            _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
        }

        ClampVelocity();
    }

    private void ClampVelocity()
    {
        float velocity = _rb.velocity.magnitude;

        if (velocity > _maxSpeed)
        {
            Vector3 movementDirection = _rb.velocity.normalized;
            _rb.velocity = movementDirection * _maxSpeed;
        }
    }
}
