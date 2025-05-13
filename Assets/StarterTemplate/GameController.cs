using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
   
   private GameManager GameManager
   {
        get { return Singleton.instance.GameManager; }
   }

   private PlayerInput PlayerInput
    {
        get { return Singleton.instance.PlayerInput; }
    }

    private Scene GetCurrentScene()
    {
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene();
    }
    private void ExitToMenu()
    {

        if (PlayerInput.OnExitToMenu())
        {


            // This method is called when the player wants to exit to the main menu.
            // It loads the main menu scene.
            if (GetCurrentScene().name == GameManager.MainMenuName)
            {
             



                GameManager.ExitGame();
            }
            else
            {
                GameManager.ExitToMenu();
                Debug.Log("Exiting to menu...");
            }
        }
    }

    private void StartFirstLevel()
    {
        if (PlayerInput.OnLoadFirstLevel() && GetCurrentScene().name == GameManager.MainMenuName)
        {
            // This method is called when the player wants to start the game.
            // It loads the first level scene.
            if (GetCurrentScene().name == GameManager.FirstLevelSceneName)
            {
                Debug.Log("Already in the first level.");
            }
            else
            {
                GameManager.LoadFirstLevel();
                Debug.Log("Loading first level...");
            }
        }
    }


    private void Update()
    {
        ExitToMenu();
        StartFirstLevel();
    }
}
