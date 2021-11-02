using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
	public float thrust = 1.0f;
	public Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		//rb.AddForce(transform.forward * thrust);
		rb.velocity = transform.right * thrust;
	}
}
