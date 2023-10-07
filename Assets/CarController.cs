using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public float carSpeed;
    public float minXLimit;
    public float maxXLimit;
    public float minYLimit;
    public float maxYLimit;

    private Rigidbody2D myRigidbody;
    private bool canMoveRight = true;
    private bool canMoveLeft = true;
    private bool canMoveUp = true;
    private bool canMoveDown = true;
    private LogicManager logic;
    private bool carIsAlive = true;

    public AudioClip collisionSound; 
    private AudioSource audioSource; 

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();

        
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (carIsAlive)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 newPosition = transform.position + new Vector3(horizontalInput, verticalInput, 0) * carSpeed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, minXLimit, maxXLimit);
            newPosition.y = Mathf.Clamp(newPosition.y, minYLimit, maxYLimit);

            transform.position = newPosition;

            if (canMoveRight && Input.GetKeyDown(KeyCode.RightArrow))
            {
                canMoveRight = false;
                canMoveLeft = true;
            }
            else if (canMoveLeft && Input.GetKeyDown(KeyCode.LeftArrow))
            {
                canMoveLeft = false;
                canMoveRight = true;
            }

            if (canMoveUp && Input.GetKeyDown(KeyCode.UpArrow))
            {
                canMoveUp = false;
                canMoveDown = true;
            }
            else if (canMoveDown && Input.GetKeyDown(KeyCode.DownArrow))
            {
                canMoveDown = false;
                canMoveUp = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (carIsAlive)
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
            {
                myRigidbody.velocity = Vector2.zero;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (carIsAlive)
        {
            
            audioSource.PlayOneShot(collisionSound);

            carIsAlive = false;
            logic.gameOver();
        }
    }
}
