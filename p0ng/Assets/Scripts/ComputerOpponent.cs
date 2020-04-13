using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerOpponent : MonoBehaviour
{
    public float initialSpeed;
    private float speed;
    private float innacuracy = 0.0f;

    private Ball ball;
    private Rigidbody rb;

    private float leftLimit;
    private float rightLimit;
    
    private void Awake()
    {
        ball = GameManager.gBall;
        rb = GetComponent<Rigidbody>();
        
        float width = GetComponent<Renderer>().bounds.size.z;
        leftLimit = GameManager.bottomLeft.z + width / 2;
        rightLimit = GameManager.topRight.z - width / 2;
    }

    private void Start()
    {
        ChangeSpeed();
    }

    private void Update()
    {
        if (GameManager.isStarted)
        {
            // Speed adjusted to framerate
            float fixedSpeed = speed * Time.deltaTime;

            float zGoal = ball.transform.position.z + innacuracy;

            // The position that the paddle will move to in the next frame.
            Vector3 step = transform.position;
            step.z = transform.position.z > zGoal ? transform.position.z - fixedSpeed : transform.position.z + fixedSpeed;

            if (Mathf.Abs(zGoal - step.z) < fixedSpeed) step.z = zGoal;

            // Prevent paddle from trespassing walls
            if (step.z < leftLimit) step.z = leftLimit;
            else if (step.z > rightLimit) step.z = rightLimit;

            transform.position = step;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangeSpeed();
        innacuracy = Random.Range(-5f, 5f);
    }

    private void ChangeSpeed()
    {
        speed = Random.Range(15f, 20f);
    }
}
