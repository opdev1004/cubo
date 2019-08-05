using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    //dash skill
    public float dashCooldown = 2f;
    private float dashCooldownTimer = 0f;
    public bool dashIsOnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        UICooldown.instance.DashReady(true, gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        //update dash cooldown
        if (dashIsOnCooldown == true)
        {
            RunDashCooldown();
        }
    }

    public void StartDashCooldown()
    {
        dashCooldownTimer = dashCooldown;
        dashIsOnCooldown = true;
        UICooldown.instance.DashReady(false, gameObject.name);
    }

    //calculates the cooldown for dash
    private void RunDashCooldown()
    {
        if (dashCooldownTimer < 0f)
        {
            dashIsOnCooldown = false;
            UICooldown.instance.DashReady(true, gameObject.name);
        } else
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }
}
