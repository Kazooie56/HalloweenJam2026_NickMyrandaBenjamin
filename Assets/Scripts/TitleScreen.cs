using UnityEngine;
using static SceneLoader;

public class TitleScreen : MonoBehaviour
{
    public void StartGame()
    {
        SceneLoader.Load(GameScene.Lobby);
    }
}