using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    // PUT THIS ON THE CANVAS WITH THE LEVEL'S SONG
    // do this for each scene
    private void Start()
    {
        AudioManager.Instance.PlayMusic(music);
    }
}