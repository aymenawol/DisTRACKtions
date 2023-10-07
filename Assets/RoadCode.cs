using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCode : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadzone = -15;

    private bool canMove = true;
    internal float initialMoveSpeed;

   

    void Update()
    {
        if (canMove)
        {
            transform.position = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime;
        }

        if (transform.position.y < deadzone)
        {
            Destroy(gameObject);
        }
    }

    public void StopMoving()
    {
        canMove = false;
    }
}