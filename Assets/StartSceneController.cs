using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public AudioClip spaceSound; // Assign your space sound in the Inspector.
    private AudioSource audioSource; // Reference to the AudioSource component.

    private bool cursorWasVisible; // To track cursor visibility state

    private void Start()
    {
        // Get the AudioSource component from this GameObject.
        audioSource = GetComponent<AudioSource>();

        // Save the initial cursor visibility state
        cursorWasVisible = Cursor.visible;

        // Hide the cursor
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        // Restore the cursor to its previous visibility state
        Cursor.visible = cursorWasVisible;
    }

    private void Update()
    {
        // Check for space input only in the "StartScene."
        if (SceneManager.GetActiveScene().name == "StartScene" && Input.GetKeyDown(KeyCode.Space) && spaceSound != null)
        {
            // Play the space sound.
            audioSource.PlayOneShot(spaceSound);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Hide the cursor when transitioning to any scene
        Cursor.visible = false;
    }
}
