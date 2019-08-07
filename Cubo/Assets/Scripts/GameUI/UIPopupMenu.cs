using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPopupMenu : MonoBehaviour
{
    public static UIPopupMenu instance;

    //Gameover menu variables
    public bool winDialogIsPresent { get; private set; }
    //Unity UI Elements
    public GameObject gameOverMenu;
    public Text victoryText;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        gameOverMenu.SetActive(false);
        Time.timeScale = 1;
    }

    //Shows the gameover menu when the game is finished
    public void DisplayGameOverMenu(string winner)
    {
        if (!winDialogIsPresent)
        {
            Time.timeScale = 0;
            winDialogIsPresent = true;
            gameOverMenu.SetActive(true);
            victoryText.text = winner + " wins.";
        }
    }

    //Restarts the game
    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
