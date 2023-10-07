using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public AudioClip gameAudioClip;
    public AudioClip spaceSound;
    public AudioClip gameStartSound; // Add a new AudioClip for the game start sound
    public float gameStartSoundDelay = 2.0f; // Delay before playing the game start sound

    private AudioSource audioSource;
    private bool musicPlaying = false;
    private bool spaceSoundPlayed = false;
    private bool gameStartSoundPlayed = false; // Flag to track if the game start sound has been played

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (gameAudioClip != null)
        {
            audioSource.clip = gameAudioClip;
            audioSource.loop = true;
            audioSource.Play();
            musicPlaying = true;
        }

        // Play the game start sound with a delay
        if (gameStartSound != null)
        {
            StartCoroutine(PlayGameStartSound());
        }
    }

    private IEnumerator PlayGameStartSound()
    {
        yield return new WaitForSeconds(gameStartSoundDelay);

        if (gameStartSound != null && !gameStartSoundPlayed)
        {
            audioSource.PlayOneShot(gameStartSound);
            gameStartSoundPlayed = true;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene" && gameAudioClip != null)
        {
            if (!musicPlaying)
            {
                audioSource.Play();
                musicPlaying = true;
            }
        }
        else if (scene.name == "StartScene" && gameAudioClip != null)
        {
            audioSource.Stop();
            audioSource.Play();
            musicPlaying = true;

            // Check if spaceSound hasn't been played yet, then play it
            if (!spaceSoundPlayed && spaceSound != null)
            {
                audioSource.PlayOneShot(spaceSound);
                spaceSoundPlayed = true;
            }

            // Reset the gameStartSoundPlayed flag to play it again when entering the StartScene
            gameStartSoundPlayed = false;

            // Play the game start sound every time the "StartScene" is loaded
            if (gameStartSound != null && !gameStartSoundPlayed)
            {
                audioSource.PlayOneShot(gameStartSound);
                gameStartSoundPlayed = true;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && spaceSound != null)
        {
            audioSource.PlayOneShot(spaceSound);
        }
    }
}
