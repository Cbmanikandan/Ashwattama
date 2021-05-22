using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        Animator anim;
        Rigidbody2D rb;

        bool isDead = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }
        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            //instantiate attack effect
            //Add recoil state
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            //hit sound
            SoundManager.PlaySound(SoundManager.Sound.EnemyHit);
            //Camera Shake
            /*if (gameObject.tag == "Player")
            {
                EZCameraShake.CameraShaker.Instance.Shake(EZCameraShake.CameraShakePresets.Bump);
                //EZCameraShake.CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            }
            else
                EZCameraShake.CameraShaker.Instance.ShakeOnce(2f, 5f, 1f, 1f);*/
            if (healthPoints == 0)
            {
                Die();
            }
        }
        private void Die()
        {
            if (isDead) return;

            isDead = true;
            anim.SetTrigger("die");
            //Death sound and vfx
            SoundManager.PlaySound(SoundManager.Sound.EnemyDie);
            rb.gravityScale = 1;
            rb.sharedMaterial = null;
            //this.GetComponentInParent<CircleCollider2D>().enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
           /* if (isDead)
                return;*/
            if ((gameObject.tag == "Player") && (collision.tag == "Enemies" || collision.tag == "Bullets"))
                TakeDamage(50);
        }
    }
}
