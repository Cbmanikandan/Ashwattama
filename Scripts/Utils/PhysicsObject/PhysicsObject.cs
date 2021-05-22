using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    //Rigidbody has to be kinematic. So gravity on the object doesn't act.
    public float minGroundNormalY = 0.65f;
    //suvat
    protected Vector2 movePosition;
    protected Vector2 velocity;
    public Vector2 initialVelocity;
    public Vector2 acceleration;

    public float gravityModifier;
    public Vector2 gravity;



    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;

    protected Rigidbody2D rb2D;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    public const float shellRadius = 0.01f;

    private void OnEnable()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //gravity can be changed using the Physics2D.gravity vector in the class that calls this
        
    }

    private void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;

        velocity = initialVelocity;
        Physics2D.gravity = gravity;
    }

    private void Update()
    {
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }    

    protected void FixedUpdate()
    {
        // 1. Calculate change in acceleration on the object
        acceleration = CalculateAcceleration();
        Physics2D.gravity += acceleration * Time.fixedDeltaTime ;

        //2. calculate change in velocity on the object
        velocity += gravityModifier * Physics2D.gravity * Time.fixedDeltaTime+targetVelocity;

        //3. Calculate change in position on the object
        movePosition = velocity * Time.fixedDeltaTime;

        //velocity.x = targetVelocity.x;
        //grounded = false;

        //Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        //Vector2 move = moveAlongGround * deltaPos.x;

        Movement(movePosition, false) ;
       
        //Vector2 deltaPos = velocity * Time.deltaTime + 0.5f * gravityModifier * Physics2D.gravity * Time.deltaTime * Time.deltaTime;

        //move = Vector2.up * deltaPos.y;
        //Movement(move, true);
    }

    private Vector2 CalculateAcceleration()
    {
        return acceleration;
        //a=f(t) or f(x); 
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;
        if (distance > minMoveDistance)
        {
            int count = rb2D.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                //below condition checks for the slope angle
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                // direction of the dot product < 0
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }
                float modifiedDistance = hitBuffer[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;

                //calculate bounce
                Vector2 reflectedDirection = Vector2.Reflect(velocity.normalized, currentNormal);
            }
        }

        rb2D.MovePosition(rb2D.position + move.normalized * distance);
        //rb2D.position = rb2D.position + move.normalized * distance;
    }
}
