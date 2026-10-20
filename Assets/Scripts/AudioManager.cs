using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // this thing means if you wanna test specific levels, you gotta go through the entire game with it
    // annoying but it's the last day on the project
    public static AudioManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    // singleton but needs to actually persist through scene changes.
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // PlayOneShot takes a sound and a volume so those are the two parameters.
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
        {
            return;
        }
        
        sfxSource.PlayOneShot(clip, volume);
    }

    public void PlayMusic(AudioClip currentSong)
    {
        // if no song or the song we're choosing is already chosen.
        // This stops restarting the song in scenes where the song should stay
        if (currentSong == null || musicSource.clip == currentSong)
        {
            return; // don't restart the same song
        }

        // that song becomes our currentSong and loops
        musicSource.clip = currentSong;
        musicSource.loop = true;
        musicSource.Play();
    }
}
