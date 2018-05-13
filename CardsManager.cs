using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CardsManager {
    private List<string[]> CardsData; // 从CSV文件读取的数据项

    public List<Card> CardsInGame;

    public List<ModelContainer> CardModel;

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
		Effect eff = new Effect1 ();
		List<Effect> effects = new List<Effect>();
		effects.Add (eff);
        int count = 0;
        foreach(string[] CardDescription in CardsData) // CardsData数组的每一个数据项都是一张卡牌的一组描述
        {
            if (count++ == 0) // 跳过csv文件第一行
                continue;

            int cardtype = CardDescription[0] == "效果牌" ? 1 : 0;
			if (cardtype == 0) {
				string cardname = CardDescription [1];
				int cardcost = Int32.Parse (CardDescription [2]);
				string carddescription = CardDescription [3];
				int cardhp = Int32.Parse (CardDescription [4]);
				int carddamage = Int32.Parse (CardDescription [5]);
				string cardmodelpath = CardDescription [7];
				string heromodelpath = CardDescription [8];
				GameObject cardmodel = (GameObject)Resources.Load (cardmodelpath);
				GameObject heromodel = (GameObject)Resources.Load (heromodelpath);

				Card newcard = new Card (cardname, cardcost, cardhp, carddamage, carddescription, cardmodel, heromodel);
				ModelContainer newcardmodel = new ModelContainer(cardmodel, heromodel);

				CardsInGame.Add(newcard);
			} else {
				string cardname = CardDescription [1];
				int cardcost = Int32.Parse (CardDescription [2]);
				string carddescription = CardDescription [3];
				string cardmodelpath = CardDescription [7];
				GameObject cardmodel = (GameObject)Resources.Load (cardmodelpath);
				Card newcard = new Card (cardname, cardcost, carddescription, effects, cardmodel);
				CardsInGame.Add(newcard);
			}

           
        }
        return;
    }
}
