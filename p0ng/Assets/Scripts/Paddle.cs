using UnityEngine;
using UnityEngine.InputSystem;
public class Paddle : MonoBehaviour
{
    [SerializeField]
    float speed = 0.0f;

    Vector3 size;
    float move = 0.0f;

    public bool isRight;

    private void Awake()
    {
        size = GetComponent<Renderer>().bounds.size;
    }

    private void Update()
    {

        // Check if is colliding with walls
        if (transform.position.z < GameManager.bottomLeft.z + size.z / 2 && move < 0)
        {
            move = 0;
        }

        if (transform.position.z > GameManager.topRight.z - size.z / 2 && move > 0)
        {
            move = 0;
        }

        transform.position += Vector3.forward * (move * speed * Time.deltaTime);
    }

    public void Init(bool isRightPaddle)
    {
        isRight = isRightPaddle;
        Vector3 pos = Vector3.zero;

        if (isRightPaddle)
        {
            pos = new Vector3(GameManager.topRight.x, 0.0f, 0.0f);
            pos -= Vector3.right * GetComponent<Renderer>().bounds.size.x;

            transform.name = "PaddleRight";
        }
        else
        {
            pos = new Vector3(GameManager.bottomLeft.x, 0.0f, 0.0f);
            pos += Vector3.right * GetComponent<Renderer>().bounds.size.x;

            transform.name = "PaddleLeft";
        }

        transform.position = pos;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>().x;
        if (!GameManager.isStarted) GameManager.StartBall(isRight);
    }

}
