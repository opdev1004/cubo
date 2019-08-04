using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour
{
    public GameObject mainScreen;
    public GameObject howToPlayScreen;

    private void Start()
    {
        howToPlayScreen.SetActive(false);
    }

    public void OnStartClick()
    {
        SceneManager.LoadScene("World");
    }

    public void OnInstructionsClick(GameObject from)
    {
        ChangeMenu(from, howToPlayScreen);
    }

    public void OnClickMainMenu(GameObject from)
    {
        ChangeMenu(from, mainScreen);
    }

    private void ChangeMenu(GameObject from, GameObject to)
    {
        from.SetActive(false);
        to.SetActive(true);
    }
}
