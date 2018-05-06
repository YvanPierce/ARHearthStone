using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CardsManager {
    private List<string[]> CardsData; // 从CSV文件读取的数据项

    public List<Card> CardsInGame;

    static CardsManager CM;

    private CardsManager()   //单例，构造方法为私有
    {
        CardsData = new List<string[]>();
        CardsInGame = new List<Card>();
    }

    public static CardsManager GetInstance()   //单例方法获取对象
    {
        if (CM == null)
        {
            CM = new CardsManager();
        }
        return CM;
    }

    public void SetCardsData(List<string[]> para)
    {
        CardsData = para;
    }

    public void InstansitateCards()
    {
        int count = 0;
        foreach(string[] CardDescription in CardsData) // CardsData数组的每一个数据项都是一张卡牌的一组描述
        {
            if (count++ == 0) // 跳过csv文件第一行
                continue;

            int cardtype = CardDescription[0] == "效果牌" ? 1 : 0;
            string cardname = CardDescription[1];
            int cardcost = Int32.Parse(CardDescription[2]);
            string carddescription = CardDescription[3];
            int cardhp = Int32.Parse(CardDescription[4]);
            int carddamage = Int32.Parse(CardDescription[5]);

            Card newcard = new Card(cardname, cardcost, cardhp, carddamage, carddescription);

            CardsInGame.Add(newcard);
        }
        return;
    }
}
