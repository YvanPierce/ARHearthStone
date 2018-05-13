using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card {
    Player theplayer;
    

    /// <summary>
    /// 卡牌的模型
    /// </summary>
    public GameObject CardModel;

    /// <summary>
    /// 若是英雄牌，召唤出的英雄的模型
    /// </summary>
    public GameObject HeroModel;

    /// <summary>
    /// 卡牌名称
    /// </summary>
    private string Name;

    /// <summary>
    /// 卡牌类型：英雄牌/效果牌 0表示英雄 1表示效果
    /// </summary>
    private int CardType;

    /// <summary>
    /// 卡牌耗费
    /// </summary>
    private int Cost;

    /// <summary>
    /// 效果牌专有：卡牌效果
    /// </summary>
	private List<Effect> Effects;

    /// <summary>
    /// 英雄牌专有：英雄血量
    /// </summary>
    private int HeroHP;

    /// <summary>
    /// 英雄牌专有：英雄伤害
    /// </summary>
    private int HeroDamage;

    /// <summary>
    /// 英雄牌专有：英雄效果
    /// </summary>
    private Effect HeroEffect;

    private string CardDescription;

    public Card(string name, int cost, int herohp, int herodamage, string carddes, GameObject cardmodel, GameObject heromodel)
    {
        Name = name;
        Cost = cost;
        HeroHP = herohp;
        HeroDamage = herodamage;
        CardModel = cardmodel;
        HeroModel = heromodel;
        // 临时属性
        CardType = 0;
        CardDescription = carddes;
    }

    public Card(Card c)
    {
        Name = c.Name;
        Cost = c.Cost;
        HeroHP = c.HeroHP;
        HeroDamage = c.HeroDamage;
        CardModel = c.CardModel;
        HeroModel = c.HeroModel;
        // 临时属性
        CardType = c.CardType;
        CardDescription = c.CardDescription;
    }

	public Card(string name, int cost,string carddes,List<Effect> effects,GameObject cardmodel) {
		Name = name;
		Cost = cost;
		CardDescription = carddes;
		CardType = 1;
		Effects = effects;
		CardModel = cardmodel;
	}

    private void OnMouseDown()
    {
    }

    /// <summary>
    /// 施展效果，通过effect.CastEffect()实现
    /// </summary>
	public void CastEffect(Hero target)
    {
		int num = Effects.Count;
		if (num == 0) {
			return;
		}
		if (num == 1) {
			Effects [0].CastEffect (target);
			return;
		}
		if (num == 2) {
			if (Input.GetButtonDown("1")) {
				Effects[0].CastEffect(target);
				return;
			}
			if (Input.GetButtonDown("2")) {
				Effects[1].CastEffect(target);
				return;
			}
		}
    }

	public void CastEffect(Player target)
	{
		int num = Effects.Count;
		if (num == 0) {
			return;
		}
		if (num == 1) {
			Effects [0].CastEffect (target);
			return;
		}
		if (num == 2) {
			if (Input.GetButtonDown("1")) {
				Effects[0].CastEffect(target);
				return;
			}
			if (Input.GetButtonDown("2")) {
				Effects[1].CastEffect(target);
				return;
			}
		}
	}

    /// <summary>
    /// 根据卡牌属性生成一个英雄实例并返回
    /// </summary>
    /// <returns>英雄实例，用于被玩家召唤</returns>
    public Hero InitHero()
    {
        Hero hero = new Hero(Name, HeroHP, HeroDamage, HeroModel);
        return hero;
    }

    public string GetName()
    {
        return Name;
    }

    public int GetCost()
    {
        return Cost;
    }

    // 临时方法
    public int getCardType()
    {
        return CardType;
    }
}
