using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public float speed = 10;
    public float spawnTimer = 5f;
    public Vector2 moveDir,acceleration;
    public float lifeDuration = 5f;
    public float homingDuration = 3f;
    [SerializeField] GameObject bulletPrefab;
    //target 0 - shoot in a direction, target > 0 shoot at player
    public int target;
    
    float elapsedTime;
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > spawnTimer)
        {
            elapsedTime = 0;
            //Fire();

            float numberOfBullets = 5;
            for (int i = 0; i < numberOfBullets; i++)
            {
                GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 20*i, 0)) as GameObject;
                bulletInstance.GetComponent<Projectile>().SetValues(moveDir, speed, acceleration, lifeDuration, target, homingDuration);
                bulletInstance.GetComponent<Projectile>().Shoot();
            }
        }
    }

    private void FixedUpdate()
    {
        
    }
    private void Fire()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;

        bulletInstance.GetComponent<Projectile>().SetValues(moveDir, speed, acceleration, lifeDuration, target, homingDuration);

        bulletInstance.GetComponent<Projectile>().Shoot();
        /*if(homingDuration>0)
            bulletInstance.GetComponent<Projectile>().ShootHomingMissileAtPlayer();
        else
            bulletInstance.GetComponent<Projectile>().ShootAtPlayer();*/
        
        //bulletInstance.GetComponent<Rigidbody2D>().gravityScale = gravity;
    }
}
