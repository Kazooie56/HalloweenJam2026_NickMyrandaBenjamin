using UnityEngine;
using static SceneLoader;

public class TitleScreen : MonoBehaviour
{
    public void StartGame()
    {
        // Only used for the title screen start button
        SceneLoader.Load(GameScene.Lobby);
    }
}