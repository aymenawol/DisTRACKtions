using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

public class CarCode : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float carSpeed;
    public AudioClip startAudioClip;
    public AudioClip gameAudioClip;

    private bool gameStarted = false;
    private bool canMove = false;

    private AudioSource audioSource;

    void Start()
    {
        myRigidbody.velocity = Vector2.zero;
        audioSource = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            gameStarted = false;

            if (startAudioClip != null)
            {
                audioSource.clip = startAudioClip;
                audioSource.Play();
            }
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            if (!gameStarted)
            {
                gameStarted = true;
                myRigidbody.velocity = Vector2.left * carSpeed;
                canMove = true;

                if (gameAudioClip != null)
                {
                    audioSource.clip = gameAudioClip;
                    audioSource.Play();
                }
            }

            // Allow space input only in "StartScene"
            // Added conditional statement to check if the current scene is the StartScene before playing the space sound
            if (canMove && Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "StartScene")
            {
                myRigidbody.velocity = Vector2.left * carSpeed;
                canMove = false;

                StartCoroutine(DelayedSceneSwitch());
            }
        }
        else if (SceneManager.GetActiveScene().name == "GameScene")
        {
            // Disable space input in "GameScene"
            canMove = false;
        }
    }

    IEnumerator DelayedSceneSwitch()
    {
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene("GameScene");
    }
}
