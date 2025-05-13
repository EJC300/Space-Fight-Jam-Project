using UnityEngine;
using UnityEngine.UI;
public class UIMenuManager : MonoBehaviour
{
    /*
     * UI Manager is used to get references to the exit and scene transition buttons.
     * The designer assigns these buttons and thanks to the singleton it is able to keep it without being overwritten
     * 
     */

    private GameManager gameManager
    {
        get { return Singleton.instance.GameManager; }
    }

    public Button NextSceneButton1 { get => NextSceneButton; set => NextSceneButton = value; }
    public Button AncillaryButton1 { get => AncillaryButton; set => AncillaryButton = value; }
    public Button ExitButton1 { get => ExitButton; set => ExitButton = value; }

    [SerializeField] private Button NextSceneButton;

    [SerializeField] private Button AncillaryButton;

    [SerializeField] private Button ExitButton;

   // [SerializeField] private string NextScene = "FirstLevel";

   // [SerializeField] private string ExitScene = "Main Menu";


    void InitButtons()
    {
        Button exitButton = ExitButton1.GetComponent<Button>();
        exitButton.onClick.AddListener(ExitToMenu);
        Button nextSceneButton = NextSceneButton1.GetComponent<Button>();
        nextSceneButton.onClick.AddListener(NextScene);
        Button ancillaryButton = AncillaryButton1.GetComponent<Button>();
        ancillaryButton.onClick.AddListener(AncilleryScene);
    }
    private void Awake()
    {
        InitButtons();
    }
    public void NextScene()
    {
        gameManager.LoadFirstLevel();
    }
    public void AncilleryScene()
    {
        gameManager.LoadAncilleryScene();
    }
    public void ExitToMenu()
    {
        gameManager.ExitToMenu();
    }
}
