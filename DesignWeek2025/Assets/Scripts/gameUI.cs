using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml;
//using System.Xml;


public class gameUI : MonoBehaviour
{

    public TextMeshProUGUI time;
    public Death death; 

    // Start is called before the first frame update
    void Start()
    {
        time.text = "Time: " + death.timeRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        time.text = "Time: " + death.timeRemaining;

    }
}
