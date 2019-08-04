using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("World");
    }

    public void OnInstructionsClick()
    {
        Debug.Log("How to Play clicked");
    }
}
