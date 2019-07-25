using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    PlayerMovement playerMovement;
    
    //controls
    private List<KeyCode> p1JumpKey = new List<KeyCode> { KeyCode.Space, KeyCode.Joystick1Button0 };
    private List<KeyCode> p2JumpKey = new List<KeyCode> { KeyCode.RightControl, KeyCode.Joystick2Button0 };
    private List<KeyCode> jumpKey;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (this.gameObject.name.Equals("Player1"))
        {
            jumpKey = p1JumpKey;
        } else
            if (this.gameObject.name.Equals("Player2"))
        {
            jumpKey = p2JumpKey;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerMovement.isJumping)
        {
            foreach (KeyCode key in jumpKey)
            {
                if (Input.GetKeyDown(key))
                {
                    playerMovement.JumpStart();
                }
            }
        }
    }
}
