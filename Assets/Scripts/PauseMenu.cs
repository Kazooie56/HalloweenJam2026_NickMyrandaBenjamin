using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private InputActionReference pauseAction;
    [SerializeField] private GameObject pausePanel;   // the UI panel to show/hide

    public static bool IsPaused;

    private void OnEnable()
    {
        pauseAction.action.Enable();
    }

    private void OnDisable()
    {
        pauseAction.action.Disable();
    }

    private void Start()
    {
        Resume();
    }

    private void Update()
    {
        if (pauseAction.action.WasPressedThisFrame())
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void QuitToTitleScreen()
    {
        Resume();
        SceneLoader.Load(SceneLoader.GameScene.TitleScreen);
    }

    public void ReturnToLobby()
    {
        Resume();
        SceneLoader.Load(SceneLoader.GameScene.Lobby);
    }

    // this actually isn't on any pause screen but I put it in the credits and main menu
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}