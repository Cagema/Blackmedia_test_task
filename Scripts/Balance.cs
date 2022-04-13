using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Balance : MonoBehaviour
{
    public Text balance;
    void FixedUpdate()
    {
        balance.text = PlayerPrefs.GetInt("Money").ToString();
    }
}
