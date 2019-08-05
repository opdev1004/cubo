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

    float P1DashbarOriginal;
    float P2DashbarOriginal;
    public Image P1UIDashRemaining;
    public Image P2UIDashRemaining;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        P1DashbarOriginal = P1UIDashRemaining.rectTransform.rect.width;
        P2DashbarOriginal = P2UIDashRemaining.rectTransform.rect.width;

    }

    public void DashReady(bool ready, string playerName, float percent)
    {
        if (playerName == playerOne.name)
        {
            SetDashText(p1Dash, ready);
            SetDashMeter(P1UIDashRemaining, P1DashbarOriginal, percent);
        } else if (playerName == playerTwo.name)
        {
            SetDashText(p2Dash, ready);
            SetDashMeter(P2UIDashRemaining, P2DashbarOriginal, percent);
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

    void SetDashMeter(Image UIDashRemaining, float originalSize, float percent)
    {

        UIDashRemaining.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * percent);
    }
}
