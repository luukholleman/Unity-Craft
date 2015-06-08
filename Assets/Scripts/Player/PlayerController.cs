using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float Acceleration = 1;
    public float MaxSpeed = 1;
    public float JumpPower = 1;

    private Rigidbody _rigidbody;

	// Use this for initialization
	void Start ()
	{
	    _rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    Vector3 movement = Vector3.zero;

	    movement.x = Input.GetAxis("Horizontal") * Acceleration;
	    movement.z = Input.GetAxis("Vertical") * Acceleration;

        movement.x = Mathf.Clamp(movement.x, -Acceleration, Acceleration);
        movement.z = Mathf.Clamp(movement.z, - Acceleration, Acceleration);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.y = JumpPower;
        }

        _rigidbody.AddRelativeForce(movement);

	    if (_rigidbody.velocity.magnitude > MaxSpeed)
	    {
	        _rigidbody.velocity = _rigidbody.velocity.normalized*MaxSpeed;
	    }
	}
}
