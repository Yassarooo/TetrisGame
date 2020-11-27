using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Shop : MonoBehaviour
{
    [System.Serializable]
    public class Shopitem
    {
        public Sprite image = null;
        public int price = 0;
        public bool isPurchased = false;

    }

    #region Singleton
    public static Shop instance;
    public Shop () {
        instance = this;
    }
    #endregion



    public  List<Shopitem> shopitemlist;
    [SerializeField] RectTransform scrollview;
    public Text text;
    Button btn;

    private void Start()
    {

        GameObject m = scrollview.GetChild(0).gameObject;

        for (int i = 0; i < shopitemlist.Count; i++)
        {
            if (Player.item[i]==1&&i!=0&&i!=1)
            {
             GameObject gg = Instantiate(m, scrollview);
            gg.transform.GetChild(0).GetComponent<Image>().sprite = shopitemlist[i].image;
            gg.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = shopitemlist[i].price.ToString();
            btn = gg.transform.GetChild(2).GetComponent<Button>();
            btn.transform.GetChild(0).GetComponent<Text>().text="Ownd";
             btn.interactable = false;
             continue;
            }
            GameObject g = Instantiate(m, scrollview);
            g.transform.GetChild(0).GetComponent<Image>().sprite = shopitemlist[i].image;
            g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = shopitemlist[i].price.ToString();
            btn = g.transform.GetChild(2).GetComponent<Button>();
            if (i==0||i==1)
            btn.AddEvent(i, clickbomb);
            else
             btn.AddEvent(i,click);
            
        }
        text.text=Player.coin.ToString();
        Destroy(m);
    }
    
    private void Update()
    {
        Button btn;
        for (int i = 0; i < shopitemlist.Count; i++)
        {
            btn = scrollview.GetChild(i).GetChild(2).GetComponent<Button>();
            if (btn.transform.GetChild(0).GetComponent<Text>().text == "Not enough money")
            {
                if(Player.coin >= shopitemlist[i].price)
                {
                    btn.transform.GetChild(0).GetComponent<Text>().text = "Buy";
                }
            }
        }
        text.text=Player.coin.ToString();
    }
    
    public void click(int index)
    {
        Button btn = scrollview.GetChild(index).GetChild(2).GetComponent<Button>();
        if (shopitemlist[index].price <= Player.coin)
        {
            shopitemlist[index].isPurchased = true;
            btn.interactable = false;
            btn.transform.GetChild(0).GetComponent<Text>().text = "Owned";
            Player.coin -= shopitemlist[index].price;
            Player.item[index]=1;
           // Player.avatar.Add(scrollview.GetChild(index).GetChild(0).GetComponent<Image>().sprite);
        }
        else
        {
            btn.transform.GetChild(0).GetComponent<Text>().text = "Not enough money";
        }
    }
    public void clickbomb(int index)
    {
        Button btn = scrollview.GetChild(index).GetChild(2).GetComponent<Button>();
        if (shopitemlist[index].price<=Player.coin)
        {
            if (index==0)
            Player.item[0]++;
            else
            Player.item[1]++;
            Player.coin -=shopitemlist[index].price;
        
        }
        else
        {
              btn.transform.GetChild(0).GetComponent<Text>().text = "Not enough money";
        }
    }

}
