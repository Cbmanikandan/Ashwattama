using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletTest : MonoBehaviour
{
    Vector2 direction;
    float speed;
    Vector2 gravity;
    Rigidbody2D bulletRB;

    public void SetValues(Vector2 d, float sp, Vector2 a)
    {
        this.direction = d;
        this.speed = sp;
        this.gravity = a;
        
    }
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        bulletRB = transform.GetComponent<Rigidbody2D>();
        bulletRB.velocity = speed * direction;
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Health>().TakeDamage(5) ;
    }
}
