using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Ball ball;
    public Paddle paddle;

    private Paddle paddle1;
    private Paddle paddle2;

    [SerializeField]
    private Material paddle1Material;
    [SerializeField]
    private Material paddle2Material;

    private Ball ballInstance;
    public static Ball gBall
    {
        get { return instance.ballInstance; }
        set { instance.ballInstance = value; }
    }

    public GameObject table;

    public static Vector3 bottomLeft;
    public static Vector3 topRight;

    public Text txtRedScore;
    public Text txtBlueScore;
    private int redScore = 0;
    private int blueScore = 0;

    public static bool isStarted = false; // It's false if a player must press a button to start the game

    public static int gRedScore
    {
        get { return instance.redScore; }
        set
        {
            instance.redScore = value;
            instance.txtRedScore.text = value.ToString();
        }
    }
    public static int gBlueScore
    {
        get { return instance.blueScore; }
        set
        {
            instance.blueScore = value;
            instance.txtBlueScore.text = value.ToString();
        }
    }

    private void Awake()
    {
        instance = this;

        // State corner coordinates
        Vector3 tableSize = table.GetComponent<Renderer>().bounds.size;
        bottomLeft = new Vector3(-tableSize.x / 2 + 2f, 0, -tableSize.z / 2 + 1.25f);
        topRight = new Vector3(tableSize.x / 2 - 2f, 0, tableSize.z / 2 - 1.25f);
    }

    private void Start()
    {
        // Create ball
        ballInstance = Instantiate(ball) as Ball;

        // Create the paddles
        paddle1 = Instantiate(paddle) as Paddle;
        paddle2 = Instantiate(paddle) as Paddle;
        paddle1.Init(true);
        paddle2.Init(false);
        paddle1.GetComponent<MeshRenderer>().material = paddle1Material;
        paddle2.GetComponent<MeshRenderer>().material = paddle2Material;

        paddle2.gameObject.AddComponent(Type.GetType("ComputerOpponent"));
        Destroy(paddle2.gameObject.GetComponent<PlayerInput>());
        GetComponent<PlayerInputManager>().DisableJoining();
    }

    public static void Lose(bool isRightWinner)
    {
        // Reset ball
        gBall.transform.position = Vector3.zero;

        // Sum points
        if (isRightWinner) gRedScore++;
        else gBlueScore++;

        // Stop game
        isStarted = false;
    }

    public static void StartBall(bool isRightStarter)
    {
        // Set the direction of the ball
        if (isRightStarter) instance.ballInstance.Direction = new Vector3(1f, 0f, 1f);
        else instance.ballInstance.Direction = new Vector3(-1f, 0, -1f);

        // Resume game
        isStarted = true;
    }
}
