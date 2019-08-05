using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //Player Scripts
    PlayerMovement playerMovement;
    PlayerSkills playerSkills;
    
    //jump controls
    private List<KeyCode> p1JumpKey = new List<KeyCode> { KeyCode.Space, KeyCode.Joystick1Button0 };
    private List<KeyCode> p2JumpKey = new List<KeyCode> { KeyCode.RightControl, KeyCode.Joystick2Button0 };
    private List<KeyCode> jumpKey;

    //dash controls
    private List<KeyCode> p1DashKey = new List<KeyCode> { KeyCode.LeftShift, KeyCode.Joystick1Button2 };
    private List<KeyCode> p2DashKey = new List<KeyCode> { KeyCode.RightShift, KeyCode.Joystick2Button2 };
    private List<KeyCode> dashKey;

    //references movement controls in the unity input system found in project settings
    private const string P1VerticalAxis = "P1Vertical";
    private const string P2VerticalAxis = "P2Vertical";
    private const string P1HorizontalAxis = "P1Horizontal";
    private const string P2HorizontalAxis = "P2Horizontal";

    //references the object names of the players in unity
    private const string PlayerOneName = "red_cubo_bot";
    private const string PlayerTwoName = "blue_cubo_bot";

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerSkills = GetComponent<PlayerSkills>();

        //sets the correct controls for the player
        if (this.gameObject.name.Equals(PlayerOneName))
        {
            jumpKey = p1JumpKey;
            dashKey = p1DashKey;
        } else
            if (this.gameObject.name.Equals(PlayerTwoName))
        {
            jumpKey = p2JumpKey;
            dashKey = p2DashKey;
        }
    }

    /**
   * Instead of being called before every rendered frame like Update(),
   * FixedUpdate is called before the physics system solves any collisions and other interactions that have happened.
   * By default it is called exactly 50 times every second.
   */
    void FixedUpdate()
    {
        if (this.gameObject.name.Equals(PlayerOneName))
        {
            playerMovement.Movement(P1VerticalAxis, P1HorizontalAxis);
        }
        else if (this.gameObject.name.Equals(PlayerTwoName))
        {
            playerMovement.Movement(P2VerticalAxis, P2HorizontalAxis);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //check for jump key
        if (!playerMovement.isJumpingUp)
        {
            if (KeyIsPressed(jumpKey))
            {
                playerMovement.JumpStart();
            }
        }

        //check for dash key
        if (!playerSkills.dashIsOnCooldown)
        {
            if (!playerMovement.movementLocked)
            {
                UICooldown.instance.DashReady(true, gameObject.name, 1f);
                if (KeyIsPressed(dashKey))
                {
                    playerSkills.StartDashCooldown();
                    playerMovement.DashStart();
                }
            }
            else
            {
                UICooldown.instance.DashReady(false, gameObject.name, playerMovement.GetKnockbackProgressAsPercent());
            }
        } else
        {
            if (playerMovement.currentKnockbackTime > playerSkills.dashCooldownTimer)
            {
                UICooldown.instance.DashReady(false, gameObject.name, playerMovement.GetKnockbackProgressAsPercent());
            } else
            {
                UICooldown.instance.DashReady(false, gameObject.name, playerSkills.GetDashCooldownAsPercent());
            }
        } 
    }

    //returns true if a key for a given control is pressed
    private bool KeyIsPressed(List<KeyCode> keys)
    {
        foreach (KeyCode key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }
}
