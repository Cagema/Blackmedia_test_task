using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyScript : MonoBehaviour
{
    public int skinNum;
    public bool isPurchased;
    public string ppName;
    public Text priceText;
    public int price;
    public int purch;

    private void Start()
    {
        purch = PlayerPrefs.GetInt(ppName, 0);

        if (purch == 0)
        {
            priceText.text = price.ToString() + '$';
        }
        else
        {
            priceText.text = "Sold";
            isPurchased = true;
        }
    }

    public void Buy()
    {
        int money = PlayerPrefs.GetInt("Money");
        if (money >= price && isPurchased == false)
        {
            money -= price;
            PlayerPrefs.SetInt(ppName, 1);
            PlayerPrefs.SetInt("Money", money);
            priceText.text = "Sold";
            isPurchased = true;
        }
        else if (isPurchased == true)
        {
            StartCoroutine(Select());
        }
    }

    private IEnumerator Select()
    {
        string lastText = priceText.text;
        priceText.text = "OK";
        PlayerPrefs.SetInt("SelectedSkin", skinNum);
        yield return new WaitForSecondsRealtime(0.5f);
        priceText.text = lastText;
    }
}
