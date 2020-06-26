using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;


    void Start() {
      GameIsPaused = false;
      pauseMenuUI.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
      //Debug.Log("up");
      if (Input.GetKeyDown(KeyCode.Escape)){
        if (GameIsPaused) {
          Resume();
        }
        else {
          Pause();
        }
      }

    }
    public void Resume() {
      pauseMenuUI.SetActive(false);
      Time.timeScale = 1f;
      GameIsPaused = false;
    }

    void Pause() {
      pauseMenuUI.SetActive(true);
      Time.timeScale = 0f;
      GameIsPaused = true;
    }
    public void Restart() {
      Debug.Log("Restarting game...");
      // create variables for loading current scene
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      Time.timeScale = 1f;
    }

    public void QuitGame() {
      Debug.Log("Quitting game...");
      Application.Quit();
    }
}
