using UnityEngine;
using static SceneLoader;

public class PauseMenu : MonoBehaviour
{
    public void ReturnToLobby()
    {
        SceneLoader.Load(GameScene.Lobby);
    }
}