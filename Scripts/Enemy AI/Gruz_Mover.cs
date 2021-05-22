using Game.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gruz_Mover : MonoBehaviour
{
    public float speed, circleCheckRadius;
    public GameObject wallCheck, groundCheck, roofCheck;
    public LayerMask groundLayer;
    private bool isFacingRight = true, wallTouch, roofTouch, groundTouch;
    public Vector2 direction;
    
    Rigidbody2D enemyRB;
    Mover mover;
    float timepassed = 0;

    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        //enemyRB.AddForce(direction * speed, ForceMode2D.Impulse);
        mover = GetComponent<Mover>();
        direction.Normalize();
        enemyRB.velocity = speed * direction;
    }

    void FixedUpdate()
    {
        //create a calculate speed function - with acceleration or without acceleration and set this to velocity,
        //takes two parameter speed and acceleration
        // velocity - direction and speed, movetowards - moving target direction, speed
        //Accelerate();
        enemyRB.velocity = direction * speed * Time.fixedDeltaTime;
        HitDetection();
    }
   

    private void Accelerate()
    {
        //v=u+at, a=v-u/t
        //timepassed += Time.fixedDeltaTime;
        float a = Mathf.Cos(Time.time*5);
        //float aparam = (float) a * Time.fixedDeltaTime;
        Vector2 startvelocity = enemyRB.velocity;
        enemyRB.velocity = enemyRB.velocity + a * direction;
        Debug.Log(" Starting Velocity: " + startvelocity + " Ending Velocity: " + enemyRB.velocity + " Acceleration: " + a);
    }

    private void Accelerate1()
    {
        //when ending velocity becomes zero - start accelerating
        // what if the acceleration is just opposite to the moving direction. Like gravity in all direction.
        //gravity + bounce - dung defender, galien
        timepassed += Time.fixedDeltaTime;
        //transform.position = transform.position + 10 cos(timepassed) * direction;
        //accelerate till a certain displacement is reached. Decelerate after that till the zero point - pendulum equation/forces
        //v=u+at, a=v-u/t
        
        float a = Mathf.Sin(timepassed);
        //float aparam = (float) a * Time.fixedDeltaTime;
        enemyRB.velocity = enemyRB.velocity + a * direction;
        Debug.Log("Velocity: " + enemyRB.velocity + " Acceleration: " + a);
    }

    private void HitDetection()
    {
        wallTouch = Physics2D.OverlapCircle(wallCheck.transform.position, circleCheckRadius, groundLayer);
        groundTouch = Physics2D.OverlapCircle(groundCheck.transform.position, circleCheckRadius, groundLayer);
        roofTouch = Physics2D.OverlapCircle(roofCheck.transform.position, circleCheckRadius, groundLayer);
        HitLogic();
    }

    private void HitLogic()
    {
        if (wallTouch && isFacingRight)
        {
            TurnDirection();
        }
        else if (wallTouch && !isFacingRight)
        {
            TurnDirection();
        }
        if(roofTouch)
        {
            direction.y = -1;
        }
        if (groundTouch)
        {
            direction.y = 1;
        }
    }

    private void TurnDirection()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
        direction.x = -direction.x;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(wallCheck.transform.position, circleCheckRadius);
        Gizmos.DrawWireSphere(groundCheck.transform.position, circleCheckRadius);
        Gizmos.DrawWireSphere(roofCheck.transform.position, circleCheckRadius);
    }
}
