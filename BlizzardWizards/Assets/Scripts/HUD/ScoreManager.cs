using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static int zombiesCounter;
    

    //public Text zombiesScore;
    


    public Text text;


    void Awake()
    {
        //text = GetComponent<Text>();
        score = 0;
        zombiesCounter = 0;
        
    }


    void Update()
    {
        text.text = "SCORE: " + score;
        //zombiesScore.text = "Zombunnies: " + ScoreManager.zombiesCounter;
        
    }
}
