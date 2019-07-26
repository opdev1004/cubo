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
    public float jumpForce = 3.5f; //jump force per second
    public float jumpDuration = 0.6f; //jump duration in seconds

    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    
    float jumpAccel;
    public bool isJumping { get; private set; }
    bool startJump;

    Vector3 previousJumpPosition;

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
        //Check for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        if (isWalking)
        {
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
            m_Rotation = Quaternion.LookRotation(desiredForward);

            m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * movementSpeed * Time.deltaTime);

            m_Rigidbody.MoveRotation(m_Rotation);
        }
        Jump();
    }

    //runs every frame
    void Update()
    {
    
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

    //Moves the character up while it is jumping.
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
}
