using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager instance;

    void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    #endregion

    public int amountZombies = 0;
    public int maxZombies = 25;

    public int round = 1;
    public int zombiesPerRound = 10;
    public int zombiesSpawnedInRound = 0;
    public int zombiesDeadInRound = 0;

    private float delayRound = 2f;
    private float delaySpawn = 2f;

    private AudioSource changeRound;

    void Start()
    {
        changeRound = GetComponent<AudioSource>();
    }

    public void newRound() {
        StartCoroutine(delayNewRound());
    }

    IEnumerator delayNewRound()
    {
        yield return new WaitForSeconds(3f);
        round++;
        ScoreManager.changeRound();
        changeRound.Play();
        yield return new WaitForSeconds(5f);
        zombiesPerRound += 5;
        zombiesSpawnedInRound = 0;
        zombiesDeadInRound = 0;
        amountZombies = 0;
    }
}
