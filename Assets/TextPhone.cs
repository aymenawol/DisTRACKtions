using UnityEditor;
using UnityEngine;

public class RandomMover : MonoBehaviour
{
    public float speed = 5.0f;
    public float minRandomDelay = 1.0f;
    public float maxRandomDelay = 5.0f;
    public float minStayingTime = 1.0f;
    public float maxStayingTime = 5.0f;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float returnX;
    public float returnY;
    public Rigidbody2D myRigidbody;
    public float initialDelay = 2.0f;

    private Vector3 originalPosition;
    private bool isMoving = true;
    private float currentDelay;
    private float stayingTime;
    private float nextMoveTime;
    public KeyCode returnKey = KeyCode.F;

    private bool isAtTargetPosition = false;
    private Vector3 targetPosition;
    private float startTime;

    void Start()
    {
        originalPosition = transform.position;

        if (myRigidbody != null)
        {
            myRigidbody.isKinematic = true;
        }

        
        Invoke("MoveToRandomPosition", initialDelay);
    }

    private void MoveToRandomPosition()
    {
       
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        
        targetPosition = new Vector3(randomX, randomY, transform.position.z);

        
        Vector3 direction = (targetPosition - transform.position).normalized;

        
        myRigidbody.velocity = direction * speed;

        isMoving = true;
    }

    void Update()
    {
        float currentDistance = Vector3.Distance(transform.position, targetPosition);

        if (isMoving)
        {
            if (currentDistance <= 0.1f)
            {
                myRigidbody.velocity = Vector2.zero;
                isMoving = false;
                isAtTargetPosition = true;

                currentDelay = Random.Range(minRandomDelay, maxRandomDelay);
                nextMoveTime = Time.time + currentDelay;
            }
        }
        else
        {
            if (Input.GetKeyDown(returnKey) && isAtTargetPosition)
            {
                ReturnToOriginalPosition();
            }

            if (Time.time >= nextMoveTime)
            {
                MoveToRandomPosition();
                isAtTargetPosition = false;

                stayingTime = Random.Range(minStayingTime, maxStayingTime);
                nextMoveTime = Time.time + stayingTime;
            }
        }
        
        if (!isMoving && !isAtTargetPosition)
        {
            float journeyLength = Vector3.Distance(transform.position, originalPosition);
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distanceCovered / journeyLength;

            if (fractionOfJourney >= 0 && fractionOfJourney <= 1)
            {
                transform.position = Vector3.Lerp(transform.position, originalPosition, fractionOfJourney);
            }
            else
            {
                transform.position = originalPosition;
                isAtTargetPosition = true;
            }
        }
    }

    private void ReturnToOriginalPosition()
    {
        
        targetPosition = new Vector3(returnX, returnY, transform.position.z);
        startTime = Time.time;
        isAtTargetPosition = false;
    }
}
