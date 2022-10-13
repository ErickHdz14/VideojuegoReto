using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Shop : MonoBehaviour
{
    [System.Serializable]
    class ShopItem
    {
        public Sprite Image;
        public int Price;
        public bool IsPurchased = false;
        public string Name;
        public Button.ButtonClickedEvent Prefab;
        public int Experience;
        public int CoinsPerSecond;

    }


    [SerializeField] List<ShopItem> ShopItemsList;
    [SerializeField] Animator NoCoinsAnimation;

    GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    Button buyBtn;


    void Start()
    {
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        int len = ShopItemsList.Count;
        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].Image;
            g.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = ShopItemsList[i].Price.ToString();
            g.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = ShopItemsList[i].Name;
            g.transform.GetChild(2).GetComponent<Button>().onClick = ShopItemsList[i].Prefab;
            buyBtn = g.transform.GetChild(2).GetComponent<Button>();
            buyBtn.interactable = !ShopItemsList[i].IsPurchased;
            buyBtn.AddEventListener(i, OnShopBtnItemClicker);

        }

        Destroy(ItemTemplate);
    }

    void OnShopBtnItemClicker(int itemIndex)
    {
        if (Game.Instance.HasEnoughCoins(ShopItemsList[itemIndex].Price))
        {
            Game.Instance.UseCoins(ShopItemsList[itemIndex].Price);
            //purchase item
            ShopItemsList[itemIndex].IsPurchased = true;

            //disable the button
            buyBtn = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            buyBtn.interactable = false;
            buyBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Comprado";

            //change ui text:coins
            Game.Instance.UpdateAllCoinsUIText();

            //add exp
            LevelSystem.Instance.AddExperience(ShopItemsList[itemIndex].Experience);

            //add coinspersecond
            Game.Instance.AddCoinsPerSecond(ShopItemsList[itemIndex].CoinsPerSecond);

            CloseShop();


        }
        else
        {
            NoCoinsAnimation.SetTrigger("NoCoins");
            GridBuildingSystem.current.destroyObject();
        }
    }


    /*---Open and Close shop*/
    public void OpenShop()
    {
        gameObject.SetActive(true);

    }
    public void CloseShop()
    {
        gameObject.SetActive(false);
    }
}
