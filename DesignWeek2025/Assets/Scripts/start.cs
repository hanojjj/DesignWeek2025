using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start : MonoBehaviour
{
    public Button startGame;
    public string gameScene;

    // Start is called before the first frame update
    void Start()
    {
        startGame.onClick.AddListener(playGame);
    }

    // Update is called once per frame
    void playGame()
    {
        SceneManager.LoadScene(gameScene);
    }
}
