using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour
{
    //Objects linked through unity
    public GameObject mainScreen;
    public GameObject howToPlayScreen;

    // Start is called before the first frame update
    private void Start()
    {
        howToPlayScreen.SetActive(false);
    }

    //Runs when start game is clicked
    public void OnStartClick()
    {
        SceneManager.LoadScene("World");
    }

    //Runs when the game instructions are selected
    public void OnInstructionsClick(GameObject from)
    {
        ChangeMenu(from, howToPlayScreen);
    }

    //Runs when returning to the main menu from another menu
    public void OnClickMainMenu(GameObject from)
    {
        ChangeMenu(from, mainScreen);
    }

    //changes the current menu displayed on the main menu. 
    private void ChangeMenu(GameObject from, GameObject to)
    {
        from.SetActive(false);
        to.SetActive(true);
    }

	public void QuitGame()
	{
		Debug.Log("Quit Game");
		Application.Quit();
	}

}
