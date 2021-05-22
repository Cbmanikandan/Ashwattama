using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PhysicsObject
{
    Rigidbody2D projectileRB;
    
    float speed;
    Vector2 moveDirection;
    Vector2 acceleration;
    GameObject target;
    float lifeDuration, homingDuration, elapsed;
    bool isHoming;

    protected Vector2 velocity;
    public float gravityModifier=1f;

    public void SetValues(Vector2 moveDirection, float speed, Vector2 acceleration, float lifeDuration,  int target, float homingDuration)
    {

        GetRigidBody();
        this.moveDirection = moveDirection.normalized;
        this.speed = speed;
        this.acceleration = acceleration;
        this.homingDuration = homingDuration;
        if (target != 0)
        {
            this.target = GameObject.FindGameObjectWithTag("Player");
            
        }
        if (homingDuration > 0)
            isHoming = true;
        gravityModifier = 1f;
        Destroy(gameObject, lifeDuration);
    }

    //SetValues => Shoot, ShootatTarget, ShoothomingmissileatTarget, Shootwithacceleration? - same function but if acc=0, zero acc
    //SetValues => speed, acc, direction, projectile image,b 

    private void GetRigidBody()
    {
        projectileRB = transform.GetComponent<Rigidbody2D>();
    }

    //Shoot in the specified direction
    public void Shoot()
    {
        //projectileRB.velocity = moveDirection * speed;
        projectileRB.velocity = Vector2.right * speed;
        //projectileRB.MovePosition()
        //scripting acceleration & gravity
        //projectileRB.velocity += moveDirection * speed + acceleration * Time.time;
        //Physics2D.gravity = Vector2.up * acceleration;
        //projectileRB.position = moveDirection * speed;

    }

    //Shoots a bullet once towards the Player - hornet throw - forward charge + reverse = two split motions, xero
    public void ShootAtPlayer()
    {
       moveDirection = (target.transform.position - transform.position).normalized;
       projectileRB.velocity = moveDirection * speed;
    }

    //Shoots a homing bullet - soul master
    public void ShootHomingMissileAtPlayer()
    {
        moveDirection = (target.transform.position - transform.position).normalized;
        projectileRB.velocity = moveDirection * speed;
    }

    //Shoot and rotate ala soul master
    public void ShootAndRotate()
    {

    }

    //Acceleration
    private void FixedUpdate()
    {
        /*velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        //Vector2 deltaPos = velocity * Time.deltaTime;

        Vector2 deltaPos = velocity * Time.deltaTime + 0.5f * gravityModifier * Physics2D.gravity * Time.deltaTime * Time.deltaTime;

        //Vector2 move = Vector2.up * deltaPos.y;
        Movement(deltaPos);*/
    }
    void Movement(Vector2 move)
    {
        float distance = move.magnitude;
       
        projectileRB.position = projectileRB.position + move;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if(health != null) 
        {
            health.TakeDamage(5);
            Destroy(gameObject);
        }
        
    }

}
