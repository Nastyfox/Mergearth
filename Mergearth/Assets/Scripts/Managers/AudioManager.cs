using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Variables
    //Variable for instance of the audio manager (singleton)
    public static AudioManager SharedInstance;

    //Variables for audio playing
    [SerializeField] private AudioClip[] playlist;
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource soundEffectsAudioSource;
    private int audioIndex = 0;
    private bool audioIsPaused;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        //Get the first audio clip to play and play it
        PlaySelectedSong(audioIndex);
    }

    // Update is called once per frame
    void Update()
    {
        //Check if the music is over and launch the next music
        if(!musicAudioSource.isPlaying && !audioIsPaused)
        {
            PlayNextMusic();
        }
    }

    private void Awake()
    {
        //Get the instance for audio manager
        SharedInstance = this;

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    #region Methods
    private void PlayNextMusic()
    {
        //Select next music (or first one if playlist is over) and play it
        audioIndex = (audioIndex + 1) % playlist.Length;
        PlaySelectedSong(audioIndex);
    }

    private void PlaySelectedSong(int index)
    {
        //Select audio clip in playlist and play it
        musicAudioSource.clip = playlist[index];
        musicAudioSource.Play();
    }

    public void PauseAudioSource()
    {
        //Pause the current music
        musicAudioSource.Pause();
        audioIsPaused = true;
    }

    public void ResumeAudioSource()
    {
        //Unpause the current music
        musicAudioSource.Play();
        audioIsPaused = false;
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        //Play the selected sound effect
        soundEffectsAudioSource.PlayOneShot(clip);
    }
    #endregion
}
