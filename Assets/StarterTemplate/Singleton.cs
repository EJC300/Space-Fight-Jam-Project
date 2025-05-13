using UnityEngine;
[RequireComponent(typeof(GameManager),typeof(PlayerInput))]
public class Singleton : MonoBehaviour
{
    // This is a simple singleton pattern implementation in C#.
    // It ensures that only one instance of the class exists and provides a global point of access to it.
    // The instance is created lazily, meaning it is created when it is first accessed.

    private GameManager gameManager;
    private PlayerInput playerInput;
    public static Singleton instance;
    public GameManager GameManager { get { return gameManager; } set { gameManager = value; } }
    public PlayerInput PlayerInput { get { return playerInput; } set { playerInput = value; } }




   public void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        gameManager = GetComponent<GameManager>();
        // Ensure that the instance is not destroyed when loading a new scene
        if (instance != null && instance != this)
        {

            Destroy(gameObject);
            return;
        }
        else
        {

            instance = this;
        }
            Debug.Log("Singleton instance created.");
        DontDestroyOnLoad(gameObject);
      
    }
   
}
