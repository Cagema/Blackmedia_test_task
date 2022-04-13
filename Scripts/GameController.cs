using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public GameUI GameUI { get; private set; }

    [SerializeField]
    public int lives;

    [SerializeField]
    private int knifeCount;

    [Header("Knife Spawning")]
    [SerializeField]
    private Vector2 knifeSpawnPosition;
    [SerializeField]
    private GameObject knifeObject;

    [SerializeField]
    internal AudioSource gameOverSound;
    [SerializeField]
    internal AudioSource hitSound;
    [SerializeField]
    internal AudioSource appleSound;
    [SerializeField]
    private AudioSource winSound;

    private void Awake()
    {
        Instance = this;
        GameUI = GetComponent<GameUI>();
    }

    private void Start()
    {
        GameUI.SetInitialDisplayedKnifeCount(knifeCount);
        SpawnKnife();
    }

    public void OnSuccessfulKnifeHit()
    {
        GameUI.score++;
        GameUI.SaveScore();

        if (knifeCount > 0)
        {
            SpawnKnife();
        }
        else
        {
            winSound.Play();  
            StartGameOverSequence(true);
        }
    }

    private void SpawnKnife()
    {
        knifeCount--;
        Instantiate(knifeObject, knifeSpawnPosition, Quaternion.identity);
    }

    public void StartGameOverSequence(bool win)
    {
        StartCoroutine("GameOverSequenceCoroutine", win);
    }

    private IEnumerator GameOverSequenceCoroutine(bool win)
    {
        if (win)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            RestartGame();
        }
        else
        {
            GameUI.ShowRestartButton();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
