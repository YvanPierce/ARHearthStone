using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero {
    Player theplayer;
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
    public int AttackCount;


    /// <summary>
    /// 每回合最大可攻击次数，固有属性/通过特效改变
    /// </summary>
    public int MaxAttackCount;

    public Hero(string name, int maxhp, int damage, GameObject heromodel)
    {
        Name = name;
        MaxHP = maxhp;
        Damage = damage;
        HP = maxhp;
        theplayer = GameObject.Find("Main Camera").GetComponent<Player>();
        HeroModel = heromodel;

        // 出生回合无法攻击
        MaxAttackCount = 0;
        AttackCount = MaxAttackCount;
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
        // 扣除攻击次数
        AttackCount--;
        Debug.Log(this.GetName() + "的攻击力：" + Damage + ", 对方" + target.GetName() + "的HP:" + target.GetHp());
        if (target.ReduceHp(this.Damage))   // 击杀目标
        {
            Debug.Log("Hero" + target.GetName() + "死亡");
            // 由对方player摧毁hero模型
            God.getInstance().currentoponent.DestroyHero(target);
        } else // 未击杀
        {
            Debug.Log("Hero" + target.GetName() + "剩余HP:" + target.GetHp());
        }
    }

    /// <summary>
    /// 攻击目标玩家
    /// </summary>
    /// <param name="target">攻击目标</param>
    public void Attack(Player target)
    {
        // 扣除攻击次数
        AttackCount--;
        Debug.Log(this.GetName() + "的攻击力：" + Damage + ", 召唤师的HP:" + target.getHp());
        target.AlterHP(-this.Damage);
        Debug.Log("召唤师剩余HP:" + target.getHp());

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

    /*
     * 受到伤害值，扣除HP;
     * 生命值为0则返回true，否则false
     */ 
    public bool ReduceHp(int damage)
    {
        this.HP -= damage;
        if (this.HP < 0)
        {
            this.HP = 0;
            return true;
        }
        return false;
    }
}
