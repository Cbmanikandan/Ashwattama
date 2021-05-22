using System.Collections;
using UnityEngine;

namespace Game.Movement
{
    public class Mover : MonoBehaviour
    {
        Rigidbody2D rb;
        CharacterStateList cState;

        [Header("X Axis Movement")]
        [SerializeField] float walkSpeed = 25f;

        [Space(5)]

        [Header("Y Axis Movement")]
        [SerializeField] float jumpSpeed = 45;
        [SerializeField] float fallSpeed = 45;
        [SerializeField] int maxJumpSteps = 10;

        [Header("Recoil")]
        [SerializeField] int recoilXSteps = 4;
        [SerializeField] int recoilYSteps = 10;
        [SerializeField] float recoilXSpeed = 45;
        [SerializeField] float recoilYSpeed = 45;
        [Space(5)]

        [Header("Ground Checking")]
        [SerializeField] Transform groundTransform; //This is supposed to be a transform childed to the player just under their collider.
        [SerializeField] float groundCheckY = 0.2f; //How far on the Y axis the groundcheck Raycast goes.
        [SerializeField] float groundCheckX = 1;//Same as above but for X.
        [SerializeField] LayerMask groundLayer;
        [Space(5)]

        [Header("Roof Checking")]
        [SerializeField] Transform roofTransform; //This is supposed to be a transform childed to the player just above their collider.
        [SerializeField] float roofCheckY = 0.2f;
        [SerializeField] float roofCheckX = 1; // You probably want this to be the same as groundCheckX

        int stepsXRecoiled;
        int stepsYRecoiled;
        int stepsJumped = 0;
        float gravity;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            gravity = rb.gravityScale;
            cState = GetComponent<CharacterStateList>();
        }

       

        void FixedUpdate()
        {
            /*if (cState.recoilingX == true && stepsXRecoiled < recoilXSteps)
            {
                stepsXRecoiled++;
            }
            else
            {
                StopRecoilX();
            }
            if (cState.recoilingY == true && stepsYRecoiled < recoilYSteps)
            {
                stepsYRecoiled++;
            }
            else
            {
                StopRecoilY();
            }
            if (Grounded())
            {
                StopRecoilY();
            }

            Jump();*/
        }

        void Jump()
        {
            if (cState.jumping)
            {

                if (stepsJumped < maxJumpSteps && !Roofed())
                {
                    //rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                    //commented to implement accelerated jump
                    rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Force);

                    //acceleration
                    //transform.position += jumpSpeed * Time.deltaTime + 0.5f * Physics2D.gravity * Time.deltaTime * Time.deltaTime;
                    //Physics2D.gravity = new Vector2(-100f, -10f);

                    stepsJumped++;
                }
                else
                {
                    cState.jumping = false;
                    stepsJumped = 0;
                }
            }
            else
            {
                rb.AddForce(Vector2.down * fallSpeed, ForceMode2D.Force);
                stepsJumped = 0;
            }

            //This limits how fast the player can fall
            //Since platformers generally have increased gravity, you don't want them to fall so fast they clip trough all the floors.
            if (rb.velocity.y < -Mathf.Abs(fallSpeed))
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -Mathf.Abs(fallSpeed), Mathf.Infinity));
            }
        }

        public void Move(float moveDirection)
        {
            Flip(moveDirection);
            //Move right of left
            if (!cState.recoilingX)
            {
                rb.velocity = new Vector2(moveDirection * walkSpeed, rb.velocity.y);

                if (Mathf.Abs(rb.velocity.x) > 0)
                {
                    cState.walking = true;
                }
                else
                {
                    cState.walking = false;
                }
                if (moveDirection > 0)
                {
                    cState.lookingRight = true;
                }
                else if (moveDirection < 0)
                {
                    cState.lookingRight = false;
                }

            }

        }

        public void Flip(float moveDirection)
        {
            if (moveDirection > 0)
            {
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
            else if (moveDirection < 0)
            {
                transform.localScale = new Vector2(-1, transform.localScale.y);
            }
        }

        public bool Grounded()
        {
            //this does three small raycasts at the specified positions to see if the player is grounded.
            if (Physics2D.Raycast(groundTransform.position, Vector2.down, groundCheckY, groundLayer)
                || Physics2D.Raycast(groundTransform.position + new Vector3(-groundCheckX, 0), Vector2.down, groundCheckY, groundLayer)
                || Physics2D.Raycast(groundTransform.position + new Vector3(groundCheckX, 0), Vector2.down, groundCheckY, groundLayer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Roofed()
        {
            //This does the same thing as grounded but checks if the players head is hitting the roof instead.
            //Used for canceling the jump.
            if (Physics2D.Raycast(roofTransform.position, Vector2.up, roofCheckY, groundLayer)
                || Physics2D.Raycast(roofTransform.position + new Vector3(roofCheckX, 0), Vector2.up, roofCheckY, groundLayer)
                || Physics2D.Raycast(roofTransform.position + new Vector3(roofCheckX, 0), Vector2.up, roofCheckY, groundLayer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void SetInputs()
        {
            
        }
        public void Recoil()
        {
            //since this is run after Walk, it takes priority, and effects momentum properly.
            if (cState.recoilingX)
            {
                if (cState.lookingRight)
                {
                    rb.velocity = new Vector2(-recoilXSpeed, 0);
                }
                else
                {
                    rb.velocity = new Vector2(recoilXSpeed, 0);
                }
            }
            if (cState.recoilingY)
            {
                if (!cState.falling)
                {
                    rb.velocity = new Vector2(rb.velocity.x, recoilYSpeed);
                    rb.gravityScale = 0;
                }
                else
                {
                    rb.velocity = new Vector2(rb.velocity.x, -recoilYSpeed);
                    rb.gravityScale = 0;
                }

            }
            else
            {
                rb.gravityScale = gravity;
            }
        }

        void StopRecoilX()
        {
            stepsXRecoiled = 0;
            cState.recoilingX = false;
        }

        void StopRecoilY()
        {
            stepsYRecoiled = 0;
            cState.recoilingY = false;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            /*Gizmos.DrawWireSphere(attackTransform.position, attackRadius);
            Gizmos.DrawWireSphere(downAttackTransform.position, downAttackRadius);
            Gizmos.DrawWireSphere(upAttackTransform.position, upAttackRadius);*/
            //Gizmos.DrawWireCube(groundTransform.position, new Vector2(groundCheckX, groundCheckY));

            Gizmos.DrawLine(groundTransform.position, groundTransform.position + new Vector3(0, -groundCheckY));
            Gizmos.DrawLine(groundTransform.position + new Vector3(-groundCheckX, 0), groundTransform.position + new Vector3(-groundCheckX, -groundCheckY));
            Gizmos.DrawLine(groundTransform.position + new Vector3(groundCheckX, 0), groundTransform.position + new Vector3(groundCheckX, -groundCheckY));

            Gizmos.DrawLine(roofTransform.position, roofTransform.position + new Vector3(0, roofCheckY));
            Gizmos.DrawLine(roofTransform.position + new Vector3(-roofCheckX, 0), roofTransform.position + new Vector3(-roofCheckX, roofCheckY));
            Gizmos.DrawLine(roofTransform.position + new Vector3(roofCheckX, 0), roofTransform.position + new Vector3(roofCheckX, roofCheckY));
        }

        //Hover - To hover around a center point, slowly moving towards a direction - distance, speed, pause in between

        //Dash - To dash - distance, speed

        //Backstep - distance, speed, height

        //Roll - distance, speed
    }
}