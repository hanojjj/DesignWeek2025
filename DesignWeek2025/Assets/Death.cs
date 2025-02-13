using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    //levels to load
    /*The Random.Range in the array should be between the length of the array and the placement in the build settings
      of the first actual level. For example, if there are 8 levels in the build and thw first actual level is at
      the third slot in the build settings (technically slot 2 since arrays start a 0) then the random.range should
      start at 2. I've added a public integer to hold the first level slot in order to make this simpler*/
    public int[] levels;
    public int firstLevelSlot;

    //Time remaining on the timer
    public float timeRemaining = 30f;

    //Scores
    [SerializeField]
    public FloatSO P1Score;
    [SerializeField]
    public FloatSO P2Score;

    //Players
    public static PlayerInput p1;
    public static Player2Input p2;

    // Start is called before the first frame update
    void Start()
    {
        //find player 1 and player 2
        p1 = FindAnyObjectByType<PlayerInput>();
        p2 = FindAnyObjectByType<Player2Input>();
    }

    // Update is called once per frame
    void Update()
    {
        //Decrease timer
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            //Debug.Log(timeRemaining);
        }
        //Timeout
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            if (p1.isHammerHeld == true)
            {               
                if (P1Score.Value == 3)
                {
                    SceneManager.LoadScene(1);
                }
                else
                {
                    P1Score.Value++;
                    SceneManager.LoadScene(Random.Range(firstLevelSlot, levels.Length));
                    Debug.Log(P1Score.Value);
                }
            }
            else if (p2.isHammerHeld == true)
            {                
                if (P2Score.Value == 3)
                {
                    SceneManager.LoadScene(2);
                }
                else
                {
                    P2Score.Value++;
                    SceneManager.LoadScene(Random.Range(firstLevelSlot, levels.Length));
                    Debug.Log(P2Score.Value);
                }
            }
            else
            {
                SceneManager.LoadScene(Random.Range(firstLevelSlot, levels.Length));
            }
        }
        //Handle Deaths
        if (p1.playerHealth <= 0)
        {           
            if (P2Score.Value == 3)
            {
                SceneManager.LoadScene(2);
            }
            else
            {
                P2Score.Value++;
                SceneManager.LoadScene(Random.Range(firstLevelSlot, levels.Length));
                Debug.Log(P2Score.Value);
            }
        }
        if (p2.playerHealth <= 0)
        {
            if (P1Score.Value == 3)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                P1Score.Value++;
                SceneManager.LoadScene(Random.Range(firstLevelSlot, levels.Length));
                Debug.Log(P1Score.Value);
            }
        }

    }
}
