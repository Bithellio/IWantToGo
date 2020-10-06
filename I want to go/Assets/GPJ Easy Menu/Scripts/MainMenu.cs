using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public string newGameScene;
    public string continueScene;

    public GameObject optionsScreen;
    public GameObject instructionsScreen;
    public GameObject quitConfirmMenu;


	// Use this for initialization
	void Start()
	{
		
	}

	//Update is called once per frame
	void Update()
	{
		
	}

    //Start New Game
    public void NewGame()
    {
        //Check if a scene name has been entered to load
        if (newGameScene.Equals("") || newGameScene.Equals(string.Empty))
        {
            Debug.LogError("No scene name entered to load for New Game");
        }
        else
        {
            
            SceneManager.LoadScene(newGameScene);
        }
    }

    //Continue Game (Ideally used for loading to a level Select)
    public void ContinueGame()
    {
        //Check if a scene name has been entered to load
        if (continueScene.Equals("") || continueScene.Equals(string.Empty))
        {
            Debug.LogError("No scene name entered to load for Continue");
        }
        else
        {

            SceneManager.LoadScene(continueScene);
        }
    }

    //Open Panel for Options
    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    //Close options Panel
    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    //Open Instructions Panel
    public void ShowInstructions()
    {
        instructionsScreen.SetActive(true);
    }

    //Close Instructions Panel
    public void CloseInstructions()
    {
        instructionsScreen.SetActive(false);
    }

    //Open Quit Confirm Dialog
    public void PressQuit()
    {
        quitConfirmMenu.SetActive(true);
    }

    //Exit the game
    public void ConfirmQuit()
    {
        Application.Quit();
    }

    //Cancel quitting the game
    public void CancelQuit()
    {
        quitConfirmMenu.SetActive(false);
    }
}