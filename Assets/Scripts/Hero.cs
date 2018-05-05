using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero {
    Player theplayer;
    ModelContainer container;
    public GameObject HeroModel;

    /// <summary>
    /// 英雄的位置
    /// </summary>
    private Vector3 Pos;

    /// <summary>
    /// 当前生命值
    /// </summary>
    private int HP;

    /// <summary>
    /// 当前攻击力
    /// </summary>
    private int Damage;

    /// <summary>
    /// 最大生命值
    /// </summary>
    private int MaxHP;

    /// <summary>
    /// 英雄名字
    /// </summary>
    private string Name;

    /// <summary>
    /// 英雄特效
    /// </summary>
    private Effect HeroEffect;

    /// <summary>
    /// 本回合可攻击次数，每回合重置
    /// </summary>
    private int AttackCount;


    /// <summary>
    /// 每回合最大可攻击次数，固有属性/通过特效改变
    /// </summary>
    private int MaxAttackCount;

    public Hero(string name, int maxhp, int damage)
    {
        Name = name;
        MaxHP = maxhp;
        Damage = damage;
        HP = maxhp;
        theplayer = GameObject.Find("Main Camera").GetComponent<Player>();
        container = GameObject.Find("Main Camera").GetComponent<ModelContainer>();
        HeroModel = container.HeroModel;
    }

    /// <summary>
    /// 施展英雄特效，通过HeroEffect.CastEffect()实现
    /// </summary>
    public void CastEffect()
    {

    }

    /// <summary>
    /// 攻击目标英雄
    /// </summary>
    /// <param name="target">攻击目标</param>
    public void Attack(Hero target)
    {

    }

    /// <summary>
    /// 攻击目标玩家
    /// </summary>
    /// <param name="target">攻击目标</param>
    public void Attack(Player target)
    {

    }

    public string GetName()
    {
        return Name;
    }

    public int GetHp()
    {
        return HP;
    }

    public int GetDamage()
    {
        return Damage;
    }
}
