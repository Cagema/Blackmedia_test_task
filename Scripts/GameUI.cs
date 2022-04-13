using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject restartButton;
    [SerializeField]
    private GameObject mainMenuButton;

    [Header("Knife Count Display")]
    [SerializeField]
    private GameObject panelKnives;

    [SerializeField]
    private GameObject iconKnife;

    [SerializeField]
    private Color usedKnifeIconColor;

    public int score;
    [SerializeField]
    private Text scoreText;

    public int money;
    [SerializeField]
    private Text moneyText;

    [SerializeField]
    public Text livesText;

    private void Start()
    {
        score = PlayerPrefs.GetInt("Score", score);
        money = PlayerPrefs.GetInt("Money", money);
    }

    private void Update()
    {
        scoreText.text = score.ToString();
        moneyText.text = money.ToString();
        livesText.text = "Lives: " + GameController.Instance.lives.ToString();
    }

    public void AddMoney()
    {
        money++;
    }

    public void SubtractMoney(int price)
    {
        money -= price;
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Money", money);
    }

    public void ShowRestartButton()
    {
        restartButton.SetActive(true);

        mainMenuButton.SetActive(true);
    }

    public void SetInitialDisplayedKnifeCount(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(iconKnife, panelKnives.transform);
        }
    }

    private int KnifeCountIndexToChange = 0;
    public void DecrementDisplayedKnifeCount()
    {
        panelKnives.transform.GetChild(KnifeCountIndexToChange++).GetComponent<Image>().color = usedKnifeIconColor;
    }
}
