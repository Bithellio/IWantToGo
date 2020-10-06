using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public bool isPaused;
    private float storeTimeScale;

    public string levelSelectSceneName;
    public string mainMenuSceneName;

    public GameObject pauseScreen;
    public GameObject optionsScreen;
    public GameObject instructionsScreen;

	// Use this for initialization
	void Start()
	{
        storeTimeScale = Time.timeScale;
	}

	//Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
	}

    public void PauseUnpause()
    {
        isPaused = !isPaused;
        pauseScreen.SetActive(isPaused);

        if(isPaused)
        {
            storeTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        } else
        {
            Time.timeScale = storeTimeScale;
            optionsScreen.SetActive(false);
            instructionsScreen.SetActive(false);
        }
    }

    public void LevelSelect()
    {
        if (levelSelectSceneName.Equals("") || levelSelectSceneName.Equals(string.Empty))
        {
            Debug.LogError("No scene name entered to load for New Game");
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(levelSelectSceneName);
        }
    }

    public void OpenOptionsScreen()
    {
        optionsScreen.SetActive(true);
    }

    public void OpenInstructionsScreen()
    {
        instructionsScreen.SetActive(true);
    }

    public void CloseInstructionsScreen()
    {
        instructionsScreen.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        if (mainMenuSceneName.Equals("") || mainMenuSceneName.Equals(string.Empty))
        {
            Debug.LogError("No scene name entered to load for New Game");
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}