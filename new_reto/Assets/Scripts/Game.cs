using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Game : MonoBehaviour
{
    #region SIngleton:Game
    public static Game Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [SerializeField] TextMeshProUGUI[] allCoinsUIText;
  

    public int Coins;
    public int CoinsPerSecond;

    void Start()
    {
        UpdateAllCoinsUIText();
        StartCoroutine(contCoins());
    }

    public void UseCoins(int amount)
    {
        Coins -= amount;
    }


    public bool HasEnoughCoins(int amount)
    {
        return (Coins >= amount);
    }

    public void UpdateAllCoinsUIText()
    {
        for (int i=0;i<allCoinsUIText.Length;i++)
        {
            allCoinsUIText[i].text = Coins.ToString();
        }
    }

    public void AddCoinsPerSecond(int CoinsPerSecondUp)
    {
        CoinsPerSecond += CoinsPerSecondUp;
    }

    public void AddCoins(int CoinsToAdd)
    {
        Coins += CoinsToAdd;
    }

    IEnumerator contCoins()
    {
        yield return new WaitForSeconds(1f);
        Coins += CoinsPerSecond;
        UpdateAllCoinsUIText();
        StartCoroutine(contCoins());
    }
}
