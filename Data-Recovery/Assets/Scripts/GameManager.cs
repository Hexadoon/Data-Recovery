
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;


    public void EndGame(bool win) {
      if (!gameHasEnded) {

        gameHasEnded = true;
        Debug.Log("game over dun dun dun");
        EndMenu end = gameObject.AddComponent(typeof(EndMenu)) as EndMenu;
        end.setEndMsg(win);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
  }

}
