﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    //dash skill
    public float dashCooldown = 2f;
    public float dashCooldownTimer { get; private set; } = 0f;
    public bool dashIsOnCooldown;

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
    }

    //calculates the cooldown for dash
    private void RunDashCooldown()
    {
        if (dashCooldownTimer < 0f)
        {
            dashIsOnCooldown = false;
        } else
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    //Gets dash cooldown as percentage
    public float GetDashCooldownAsPercent()
    {
        if (dashCooldownTimer <= 0f)
        {
            return 1f;
        }
        return 1 - dashCooldownTimer / dashCooldown;
    }
}
