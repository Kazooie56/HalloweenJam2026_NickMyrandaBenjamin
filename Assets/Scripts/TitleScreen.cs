using UnityEngine;
using static SceneLoader;

public class TitleScreen : MonoBehaviour
{
    public void StartGame()
    {
        // Only used for the title screen start button
        SceneLoader.Load(GameScene.Lobby);
    }

    // this actually isn't on any pause screen but I put it in the credits and main menu
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}