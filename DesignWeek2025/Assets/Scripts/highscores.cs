using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highscores : MonoBehaviour
{
    public Button highscoresButton;
        
    // Start is called before the first frame update
    void Start()
    {
        highscoresButton.onClick.AddListener(displayHighscores);

        // Need to grab highscores from other script
    }

    // Update is called once per frame
    void displayHighscores()
    {
        // Display highscores here
    }
}
