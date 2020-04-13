using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Game");
    }
}
