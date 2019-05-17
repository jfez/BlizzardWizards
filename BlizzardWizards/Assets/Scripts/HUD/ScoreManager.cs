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
    public Text round;
    private GameManager gameManager;
    static int currentRound;


    void Awake()
    {
        //text = GetComponent<Text>();
        score = 0;
        zombiesCounter = 0;
        gameManager = GameManager.instance;
    }

    void Start()
    {
        currentRound = gameManager.round;
    }


    void Update()
    {
        text.text = "SCORE: " + score;
        round.text = currentRound.ToString();
        //zombiesScore.text = "Zombunnies: " + ScoreManager.zombiesCounter;
        
    }

    public static void changeRound() {
        currentRound++;
    }
}
