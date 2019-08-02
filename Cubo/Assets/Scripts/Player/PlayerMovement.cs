using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //objects
    Rigidbody m_Rigidbody;
    BoxCollider m_BoxCollider;

    public float turnSpeed = 20f; //turn speed in radians per second
    public float movementSpeed = 1.0f;

    //jumping
    public float jumpForce = 3.5f;
    public bool isJumping { get; private set; }
    bool startJump;
    Vector3 previousJumpPosition;

    //movement
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    public bool movementLocked { get; private set; }

    //dash
    public float dashForce = 3.5f;
    private float currentDashTime = 0f;
    public float dashTime = 0.5f;

    //dash collision
    public float currentKnockbackTime = 0f;
    private Vector3 knockbackForward;
    private float knockbackSpeed;
    private Rigidbody playerCollidedWith;
    private bool collided;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_BoxCollider = GetComponent<BoxCollider>();

        previousJumpPosition = new Vector3(0f, 0f, 0f);
    }

    /**
	* Instead of being called before every rendered frame like Update(),
	* FixedUpdate is called before the physics system solves any collisions and other interactions that have happened.
	* By default it is called exactly 50 times every second.
	*/
    void FixedUpdate()
    { 
        Jump();
        if (currentDashTime > 0f)
        {
            Dash();
        }
        if (currentKnockbackTime > 0f)
        {
            Knockback();
        }
        MovementLock();
    }

    //causes the player to start jumping. use only after checking for player jumps as box cast is resource intensive.
    public void JumpStart()
    {
        //check if the player is standing on a solid object (for cube shaped objects)
         if (Physics.BoxCast(m_Rigidbody.position, new Vector3((m_BoxCollider.size.x / 2.05f) * gameObject.transform.localScale.x, (m_BoxCollider.size.y / 2.05f) * gameObject.transform.localScale.y, (m_BoxCollider.size.z / 2.05f) * gameObject.transform.localScale.x), Vector3.down, Quaternion.LookRotation(Vector3.down), m_BoxCollider.size.y * 0.05f))
         {
            isJumping = true;
            startJump = true;
         }
    }

    //Moves the character up while they are jumping.
    void Jump()
    {
        if (isJumping)
        {
            //checks if the player is still gaining height or if player is starting a jump
            if (!Mathf.Approximately(m_Rigidbody.position.y, previousJumpPosition.y) && m_Rigidbody.position.y > previousJumpPosition.y || startJump)
            {
                previousJumpPosition = m_Rigidbody.position;
                m_Rigidbody.MovePosition(previousJumpPosition + new Vector3(0, jumpForce, 0) * Time.deltaTime);
                startJump = false;
            }
            else
            {
                isJumping = false;
            }
        }
    }

    //Moves the player using the controls specified in the unity control settings
    public void Movement(string uInputVertical, string uInputHorizontal)
    {
        if (!movementLocked)
        {
            float vertical = Input.GetAxis(uInputVertical); 
            float horizontal = Input.GetAxis(uInputHorizontal);

            m_Movement.Set(horizontal, 0f, vertical);
            m_Movement.Normalize();

            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
            m_Rotation = Quaternion.LookRotation(desiredForward);

            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * movementSpeed * Time.deltaTime);

            m_Rigidbody.MoveRotation(m_Rotation);
        }
    }

    //Run when you want the player to start dashing
    public void DashStart()
    {
        currentDashTime = dashTime;
        MovementLock();
    }

    //causes the player to move forward quickly in the direction it is facing.
    private void Dash()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + transform.forward * dashForce * movementSpeed * Time.deltaTime);
        currentDashTime -= Time.deltaTime;
    }

    //checks if the hit player meets the conditions for knockback and runs StartKnockback()
    public void CheckAndRunKnockbackOnCollision(PlayerMovement hitPlayer)
    {
        //checks for a collision with another player if dashing
        if (currentDashTime > 0f && !(hitPlayer == null))
        {
            //checks if the collision is being handled by the other player
            if (!hitPlayer.collided)
            {
                collided = true;
                hitPlayer.StartKnockback(dashTime - hitPlayer.currentDashTime, transform.forward, movementSpeed * dashForce);
                //checks if the other player was dashing during the collision.
                if (hitPlayer.currentDashTime > 0)
                {
                    StartKnockback(hitPlayer.dashTime - currentDashTime, hitPlayer.transform.forward, hitPlayer.movementSpeed * hitPlayer.dashForce);
                }
                DashCancel();
                hitPlayer.DashCancel();
                collided = false;
            }
        }
    }
    
    //puts the player in the Knockback state.
    public void StartKnockback(float knockbackTime, Vector3 direction, float speed)
    {
        currentKnockbackTime = knockbackTime;
        MovementLock();
        knockbackForward = direction;
        knockbackSpeed = speed;
    }

    //Cancels the dash ability
    private void DashCancel()
    {
        //End Dash Early
        currentDashTime = 0f;
        MovementLock();
    }

    //Knocks back the player in the direction the colliding player was facing.
    private void Knockback()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + (knockbackForward * knockbackSpeed * Time.deltaTime));
        currentKnockbackTime -= Time.deltaTime;
    }

    //Enables movement if any timers that prevent movement are not running
    private void MovementLock()
    {
        if (currentDashTime <= 0f && currentKnockbackTime <= 0f)
        {
            movementLocked = false;
            currentDashTime = 0f;
            currentKnockbackTime = 0f;
        }
        else
        {
            movementLocked = true;
        }
    }
}
