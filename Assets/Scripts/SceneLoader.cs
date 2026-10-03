using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public enum GameScene
    {
        MainMenu,
        Lobby,
        Level1,
        Level2,
        Level3
    }

    public static void Load(GameScene scene)
    {
        // ToString converts the enum to a string
        // so when we call "GameScene.Lobby" like in TitleScreen.cs, 
        // it takes the enum and makes it a string
        // LoadScene only takes strings

        SceneManager.LoadScene(scene.ToString());
    }
}