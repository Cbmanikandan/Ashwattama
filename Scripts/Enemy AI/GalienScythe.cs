using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalienScythe : MonoBehaviour
{
	public float bounce = 1;
	public float maxDistance = 6;
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
		//velocity.x = Random.Range(1, 10);
		velocity.y = maxJumpVelocity;
		Destroy(gameObject, 10);
	}
	void Update()
	{
		CalculateVelocity();

		controller.Move(velocity * Time.deltaTime, new Vector2(0, 0));
		
        //Code for implementing bounce 
        if (controller.collisions.above || controller.collisions.below)
        {
            if (bounce > 0)
            {
                velocity.y = -velocity.y * bounce;
            }
            else
            {
                velocity.y = 0;
            }
        }
        if (controller.collisions.left || controller.collisions.right)
        {
            if (bounce > 0)
            {
                velocity.x = -velocity.x * bounce;
            }
            else
            {
                velocity.y = 0;
            }
        }
    }
	void CalculateVelocity()
	{
		//float targetVelocityX = directionalInput.x * moveSpeed;
		//velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
	}
}
