using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml;
//using System.Xml;


public class gameUI : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI time;
    public Death death; 

    // Start is called before the first frame update
    void Start()
    {
        time.text = "Time: " + death.timeRemaining;
        score.text = death.P2Score.Value + " - " + death.P1Score.Value;
    }

    // Update is called once per frame
    void Update()
    {
        time.text = "Time: " + death.timeRemaining.ToString("F1");
        score.text = death.P2Score.Value + " - " + death.P1Score.Value;
    }
}
