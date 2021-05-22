using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Javelin : MonoBehaviour
{
	public float maxDistance = 14;
	public float timeToJumpApex = .4f;
	Controller2D controller;
	float maxJumpVelocity;

	float gravity;
	Vector3 velocity;
	Vector2 acceleration;


	void Start()
	{
		controller = GetComponent<Controller2D>();
		
		gravity = -(2 * maxDistance) / Mathf.Pow(timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		velocity.y = maxJumpVelocity;
		Destroy(gameObject, timeToJumpApex * 2);
	}

	void Update()
	{
		CalculateVelocity();
		controller.Move(velocity * Time.deltaTime, new Vector2(0,0));
	}

	void CalculateVelocity()
	{
		//velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		//velocity.x = 0;
		velocity.y += gravity * Time.deltaTime;
	}
}
