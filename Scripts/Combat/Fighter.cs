using Game.Core;
using System.Collections;
using UnityEngine;

namespace Game.Combat
{
    public class Fighter : MonoBehaviour
    {
        Rigidbody2D rb;
        CharacterStateList cState;
        [SerializeField] float damage = 50f;
        Animator anim;

        [Space(5)]

        [Header("Attacking")]
        [SerializeField] float timeBetweenAttack = 01f;
        [SerializeField] Transform attackTransform; // this should be a transform childed to the player but to the right of them, where they attack from.
        [SerializeField] float attackRadius = 1;
        [SerializeField] LayerMask attackableLayer;
        float timeSinceAttack;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            cState = GetComponent<CharacterStateList>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {

        }

        void SetInputs()
        {
            
        }

        public void Attack()
        {            
            timeSinceAttack += Time.deltaTime;
            if (Input.GetButtonDown("Fire1") && timeSinceAttack >= timeBetweenAttack)
            {
                timeSinceAttack = 0;
                anim.SetTrigger("Attack");
                Collider2D[] objectsToHit = Physics2D.OverlapCircleAll(attackTransform.position, attackRadius, attackableLayer);
                if (objectsToHit.Length > 0)
                {
                    cState.recoilingX = true;
                }
                for (int i = 0; i < objectsToHit.Length; i++)
                {
                    //Here is where you would do whatever attacking does in your script.
                    //In my case its passing the Hit method to an Enemy script attached to the other object(s).
                    objectsToHit[i].GetComponent<Health>().TakeDamage(damage);
                }

                SoundManager.PlaySound(SoundManager.Sound.PlayerAttack);

            }
        }

        //During recoiling due to getting hurt, cannot attack

        //patrol path

        //Check radius

        //alert

        //aggression - charge, attack, shoot projectile

        //Fire - projectile array - quantity, origin, displacement, speed, isHoming

        //lose aggression
    }
}