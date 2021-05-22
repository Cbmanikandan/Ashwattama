using Game.Combat;
using Game.Core;
using Game.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{

    public class PlayerController : PhysicsObject
    {
        CharacterStateList cState;
        Mover mover;
        Health health;
        Fighter fighter;
        Rigidbody2D rb;
        [SerializeField] Animator anim;

        //Inputs
        float xAxis, yAxis;

        void Start()
        {
            health = GetComponent<Health>();
            fighter = GetComponent<Fighter>();
            rb = GetComponent<Rigidbody2D>();
            mover = GetComponent<Mover>();
            cState = GetComponent<CharacterStateList>();
        }


        protected override void ComputeVelocity()
        {
            if (health.IsDead()) return;

            Vector2 move = Vector2.zero;

            move.x = Input.GetAxis("Horizontal") * 20;
            
            if(Input.GetButtonDown("Jump") && mover.Grounded())
            {
                //move.y =
            }
            targetVelocity = move;
            
            
            //GetInputs();
            //Move(xAxis);
            //Recoil();
            //Attack();
        }

        private void Recoil()
        {
            mover.Recoil();
        }

        private void GetInputs()
        {
            //Check Robbie Platformer for stacking the inputs 
            //WASD/Joystick
            /*yAxis = Input.GetAxis("Vertical");
            xAxis = Input.GetAxis("Horizontal");

            //Sensitivity.
            if (yAxis > 0.25)
                yAxis = 1;
            else if (yAxis < -0.25)
                yAxis = -1;
            else
                yAxis = 0;

            if (xAxis > 0.25)
                xAxis = 1;
            else if (xAxis < -0.25)
                xAxis = -1;
            else
                xAxis = 0;

            anim.SetBool("Grounded", mover.Grounded());

            //Jumping
            if (Input.GetButtonDown("Jump") && mover.Grounded())
                cState.jumping = true;
            if (Input.GetButtonUp("Jump") && cState.jumping)
                cState.jumping = false;
            anim.SetBool("Jumping", cState.jumping);*/
        }

        void Move(float moveDirection)
        {
            //mover.Move(moveDirection);
            //anim.SetBool("Walking", cState.walking);
        }

        void Attack()
        {
            fighter.Attack();
           // anim.SetTrigger("Attack");
        }

    }
}
