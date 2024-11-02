using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private Animator _animator;
    private Rigidbody _rb;

    private float _speed;
    private float _maxSpeed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _rotationSpeed;

    private void Start()
    {
        _maxSpeed = _walkSpeed;
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

        transform.Rotate(0, Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime, 0);



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
да я люблю сосать ч...упачупс
