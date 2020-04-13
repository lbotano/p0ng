using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private AudioSource audioSrc;

    [SerializeField]
    float speed = 1.0f;
    [SerializeField]
    float angleModifier = 1.0f;

    float radius;
    private Vector3 direction;
    public Vector3 Direction
    {
        get { return direction; }
        set
        {
            direction = value;
            audioSrc.Play();
        }
    }

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        direction = new Vector3(1f, 0, 1f).normalized;
        radius = GetComponent<Renderer>().bounds.size.z / 2;
    }

    private void Update()
    {
        if (GameManager.isStarted)
        {
            transform.position += Direction.normalized * speed * Time.deltaTime;

            // Bounce with walls
            if (transform.position.z < GameManager.bottomLeft.z + radius && Direction.z < 0 ||
                transform.position.z > GameManager.topRight.z - radius && Direction.z > 0)
            {
                Direction = new Vector3(Direction.x, Direction.y, -Direction.z);
            }

            // Game over
            if (transform.position.x < GameManager.bottomLeft.x + radius && Direction.x < 0)
            {
                GameManager.Lose(true);
            }
            else if (transform.position.x > GameManager.topRight.x - radius && Direction.x > 0)
            {
                GameManager.Lose(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Bounce ball with angle
        if (other.tag == "Paddle")
        {
            bool isRight = other.GetComponent<Paddle>().isRight;

            if (!isRight && Direction.x < 0 ||
                isRight && Direction.x > 0)
            {
                Direction = new Vector3(
                    -Direction.x,
                    Direction.y,
                    (transform.position.z - other.transform.position.z) * angleModifier
                );
            }
        }
    }
}
