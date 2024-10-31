using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputAction _thrust;
    [SerializeField] private InputAction _rotation;
    [SerializeField] private float _thrustStrength = 100f;
    [SerializeField] private float _rotationStrength = 100f;
    
    private Rigidbody rocketRb;
    AudioSource rocketAudioSource;

    private void OnEnable()
    {
        _thrust.Enable();
        _rotation.Enable();
    }

    private void Start()
    {
        rocketRb = GetComponent<Rigidbody>();
        rocketAudioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (_thrust.IsPressed())
        {
            rocketRb.AddRelativeForce(Vector3.up * _thrustStrength * Time.fixedDeltaTime);
            if (!rocketAudioSource.isPlaying)
            {
                rocketAudioSource.Play();
            }
        }
        else
        {
            rocketAudioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = _rotation.ReadValue<float>();

        if (rotationInput < 0f)
        {
            ApplyRotation(_rotationStrength);
        }
        else if (rotationInput > 0f)
        {
            ApplyRotation(-_rotationStrength);
        }
    }

    private void ApplyRotation(float rotationStrength)
    {
        rocketRb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationStrength * Time.fixedDeltaTime);
        rocketRb.freezeRotation = false;

    }
}
