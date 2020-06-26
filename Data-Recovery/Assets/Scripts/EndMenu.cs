using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndMenu : MonoBehaviour
{

  bool gameHasEnded = false;
  public Text endMsg;
  public GameObject endMenuUI;

  void Start() {
    //endMenuUI = gameObject;
    endMenuUI.SetActive(false);

  }

  public void EndGame(bool win) {
    if (!gameHasEnded) {
      Time.timeScale = 0f;

      gameHasEnded = true;
      Debug.Log("game over dun dun dun");
      //EndMenu end = gameObject.AddComponent(typeof(EndMenu)) as EndMenu;
      endMenuUI.SetActive(true);
      setEndMsg(win);
      //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

  }
}

    public void setEndMsg(bool win) {
      if (win) {
        endMsg.text = "Congratulations! You've won!";
      }
      if (!win) {
        endMsg.text = "You've run out of lives :(";
      }

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
