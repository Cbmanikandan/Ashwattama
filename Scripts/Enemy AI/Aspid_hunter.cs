using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Movement;

public class Aspid_hunter : MonoBehaviour
{
    public float moveSpeed;
    public float chaseRange;
    public float shootingRange;
    public float fireRate = 1f;
    public float nextFireTime;

    private Transform Player;
    [SerializeField] public float hoverInterval = 1f;
    
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletPos;

    Animator anim;
    CharacterStateList cState;
    Mover mover;
    float timePassed;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        timePassed = 0;
        cState = transform.GetComponent<CharacterStateList>();
        mover = transform.GetComponent<Mover>();
        cState.lookingRight = false;
    }

    void FixedUpdate()
    {
        //CheckAggressionRadius
        float distanceFromPlayer = Vector2.Distance(Player.position, transform.position);
        if (distanceFromPlayer < chaseRange && distanceFromPlayer>shootingRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, moveSpeed * Time.fixedDeltaTime);
        }
        if(distanceFromPlayer<=shootingRange && nextFireTime<Time.time)
        {
            Instantiate(bulletPrefab, bulletPos.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
        //Alert if player inside
        //Charge towards Player - path finding
        //Flip based on velocity
       /* timePassed += Time.deltaTime;
        if (timePassed > hoverInterval)
            Hover();*/
    }
    
    //Hover + then chase within radius

    private void Hover()
    {
        timePassed = 0;
        transform.position += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        if ((transform.position - FindObjectOfType<Player>().transform.position).x < 0 && cState.lookingRight == false)
        {
            cState.lookingRight = true;            
            anim.SetTrigger("turn");
            mover.Flip(1);
        }

        if ((transform.position - FindObjectOfType<Player>().transform.position).x > 0 && cState.lookingRight == true)
        {
            cState.lookingRight = false;
            mover.Flip(-1);
            anim.SetTrigger("turn");
        }
    }

    private void FalseKnightSpawnBullets()
    {
        //CreateRandomSpawnPoint();
        //Instantiate()
        //GameObject bulletInstance = Instantiate(bulletPrefab, bulletPos.Position, Quaternion.identity) as GameObject;
        //bulletInstance.GetComponent<Projectile>().SetValues();
    }

    private void Fire()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, bulletPos.transform.position, Quaternion.identity, transform) as GameObject;
        //bulletInstance.GetComponent<Rigidbody2D>().velocity = rb.velocity + rb.velocity;
        //Vector3 dir = (FindObjectOfType<Player>().transform.position - transform.position).normalized;
        //bulletInstance.GetComponent<Projectile>().SetValues(new Vector3(-10,-10,0));

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

}
