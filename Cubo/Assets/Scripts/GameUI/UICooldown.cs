using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICooldown : MonoBehaviour
{
    //Objects and UI elements linked through unity
    public Text p1Dash;
    public Text p2Dash;
    public GameObject playerOne;
    public GameObject playerTwo;
    public Image P1UIDashRemaining;
    public Image P2UIDashRemaining;

    //dash bar variables
    float P1DashbarOriginal;
    float P2DashbarOriginal;

    public static UICooldown instance;

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

    //Updates the status of the dash skill in the UI for the specified player
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

    //Updates the dash text for the specified text field.
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

    //updates the dash bar in the UI
    void SetDashMeter(Image UIDashRemaining, float originalSize, float percent)
    {

        UIDashRemaining.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * percent);
    }
}
