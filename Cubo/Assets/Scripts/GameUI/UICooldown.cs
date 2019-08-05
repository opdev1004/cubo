using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICooldown : MonoBehaviour
{
    public Text p1Dash;
    public Text p2Dash;
    public GameObject playerOne;
    public GameObject playerTwo;
    public static UICooldown instance;

    private void Awake()
    {
        instance = this;
    }

    public void DashReady(bool ready, string playerName)
    {
        if (playerName == playerOne.name)
        {
            SetDashText(p1Dash, ready);
        } else if (playerName == playerTwo.name)
        {
            SetDashText(p2Dash, ready);
        }
    }

    void SetDashText(Text dashText, bool ready)
    {
        if (ready)
        {
            dashText.text = "Dash: Ready";
        } else
        {
            dashText.text = "Dash: Not Ready";
        }
    }
}
