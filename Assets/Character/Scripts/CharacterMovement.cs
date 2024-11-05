using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    private Animator _animator;
    private Rigidbody _rb;

    private float _speed;
    [SerializeField] private float _rotateSpeed = 1;
    [SerializeField] private float _walkSpeed = 4;
    [SerializeField] private float _runSpeed = 7;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float movementDirection = Input.GetAxis("Vertical");

        _animator.SetBool("walk", movementDirection > 0);
        _animator.SetBool("run", Input.GetKey(KeyCode.LeftShift));

        _speed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed;

        if (movementDirection > 0)
        {
            _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
        }

        transform.Rotate(0, Input.GetAxis("Horizontal") * _rotateSpeed * Time.deltaTime, 0);
        ClampRotation();
        ClampVelocity();
    }
    private void ClampRotation()
    {
        Vector3 rotationAngles = transform.rotation.eulerAngles;
        rotationAngles.y = rotationAngles.y > 180 ? rotationAngles.y - 360 : rotationAngles.y;
        rotationAngles.y = Mathf.Clamp(rotationAngles.y, -45, 45);
        transform.rotation = Quaternion.Euler(rotationAngles);
    }
    private void ClampVelocity()
    {
        Vector3 vel = _rb.velocity;
        vel.y = 0;
        float velocity = vel.magnitude;

        if (velocity > _speed)
        {
            Vector3 movementDirection = vel.normalized;
            Vector3 rbVelocity = movementDirection * _speed;
            rbVelocity.y = _rb.velocity.y;
            _rb.velocity = rbVelocity;
        }
    }

}
