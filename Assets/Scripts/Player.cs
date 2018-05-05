using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public string SelectState = "Selected Nothing";
    PosContainer poscontainer;
    public Hero SelectedHero1 = null;
    public Hero SelectedHero2 = null;
    public Card SelectedCard = null;
    public GameObject hitobject = null;

    // 新加字段(选中的召唤师)
    public Player selectedPlayer = null;
    
    /// <summary>
    /// 当前生命值
    /// </summary>
    private int HP = 10;

    /// <summary>
    /// 当前能量值
    /// </summary>
    private int MP;

    /// <summary>
    /// 最大生命值
    /// </summary>
    private int MaxHP = 10;

    /// <summary>
    /// 最大能量值
    /// </summary>
    private int MaxMP;

    /// <summary>
    /// 卡组，由玩家在对局外自行组合
    /// </summary>
    private List<Card> CardsCombination;

    /// <summary>
    /// 牌库，对局开始时打乱卡组获得
    /// </summary>
    private Stack<Card> CardStack;

    /// <summary>
    /// 手牌，玩家从牌库中获得
    /// </summary>
    public List<Card> CardsInHand;

    /// <summary>
    /// 卡牌类Card对应的游戏物体
    /// </summary>
    private List<GameObject> CardsInHand_Obejct;

    /// <summary>
    /// 场上的英雄，玩家召唤而来
    /// </summary>
    private List<Hero> HeroesOnCourt;

    /// <summary>
    /// 英雄Hero类实例对应的游戏物体
    /// </summary>
    private List<GameObject> HeroesOnCourt_Object;



    // Use this for initialization
    void Start () {
        poscontainer = this.GetComponent<PosContainer>();
        CardStack = new Stack<Card>();
        CardsInHand = new List<Card>();
        CardsInHand_Obejct = new List<GameObject>();
        HeroesOnCourt = new List<Hero>();
        HeroesOnCourt_Object = new List<GameObject>();

        Card FirstCard = new Card("FirstHero", 0, 300, 300);
        CardStack.Push(FirstCard);

	}
	
	// Update is called once per frame
	void Update () {

	}



    /// <summary>
    /// 根据英雄游戏物体来返回对应的英雄类实例
    /// </summary>
    /// <param name="hero_gobj">游戏场景中的英雄物体</param>
    /// <returns></returns>
    public Hero GetHero(GameObject hero_gobj)
    {
        int index = 0;
        for (; index < HeroesOnCourt_Object.Count; index++)
            if (HeroesOnCourt_Object[index] == hero_gobj)
                return HeroesOnCourt[index];
        return null;
    }

    /// <summary>
    /// 根据卡牌游戏物体来返回对应的卡牌类实例
    /// </summary>
    /// <param name="card_gobj">游戏场景中的卡牌物体</param>
    /// <returns></returns>
    public Card GetCard(GameObject card_gobj)
    {
        int index = 0;
        for (; index < CardsInHand_Obejct.Count; index++)
            if (CardsInHand_Obejct[index] == card_gobj)
                return CardsInHand[index];
        return null;
    }

    /// <summary>
    /// 使用卡牌，根据卡牌类型不同决定是召唤英雄还是施展卡牌效果，使用后卡牌从手牌中删去
    /// </summary>
    /// <param name="card">使用的那一张卡牌</param>
    public void UseCard(Card card, Vector3 HeroSpawnPos)
    {
        SummonHero( card.InitHero(), HeroSpawnPos);
        GameObject cardObject = this.getCardObject(this.SelectedCard);
        Destroy(cardObject, .5f);
    }

    /// <summary>
    /// 玩家召唤英雄，召唤后英雄加入HeroesOnCourt中，并出现在场上
    /// 英雄的获取通过调用card.InitHero();
    /// </summary>
    /// <param name="hero">召唤的英雄</param>
    public void SummonHero(Hero hero, Vector3 SpawnPos)
    {
        GameObject SummonedHero = GameObject.Instantiate(hero.HeroModel, SpawnPos, Quaternion.identity);
        HeroesOnCourt_Object.Add(SummonedHero);
        HeroesOnCourt.Add(hero);
        SummonedHero.name = "SummonedHero1";
        SummonedHero.tag = "Hero";
    }

    /// <summary>
    /// 玩家结束回合，导演进行回合结算后，将出牌权转移给另一玩家
    /// </summary>
    public void EndTurn()
    {
        
    }

    /// <summary>
    /// 修改玩家当前HP，注意不能溢出上下界，返回是否修改成功
    /// </summary>
    /// <param name="hp">正数则增，负数则减</param>
    /// <returns></returns>
    public bool AlterHP(int hp)
    {
        return false;
    }

    /// <summary>
    /// 修改玩家当前MP，注意不能溢出上下界，返回是否修改成功
    /// </summary>
    /// <param name="mp">正数则增，负数则减</param>
    /// <returns></returns>
    public bool AlterMP(int mp)
    {
        return false;
    }

    /// <summary>
    /// 判断玩家当前MP是否大于mp
    /// </summary>
    /// <param name="mp"></param>
    /// <returns></returns>
    public bool EnoughMP(int mp)
    {
        return false;
    }

    /// <summary>
    /// 玩家从牌库栈顶获得牌，加入手牌中
    /// </summary>
    public void DrawCard()
    {
        Debug.Log("Draw Card");
        Card CardToDraw = CardStack.Peek();
        CardStack.Pop();
        CardsInHand.Add(CardToDraw);

        GameObject card = GameObject.Instantiate(CardToDraw.CardModel, poscontainer.CardsPos[0], Quaternion.identity);
        CardsInHand_Obejct.Add(card);
        card.name = "HeroCard1";
        card.tag = "Card";
    }

    /// <summary>
    /// 玩家认输
    /// </summary>
    /// <returns>投降成功/失败</returns>
    public bool Yield()
    {
        return false;
    }

    /// <summary>
    /// 将手牌弃至牌库
    /// </summary>
    /// <param name="card">要丢弃的卡牌</param>
    /// <returns>弃牌成功/失败</returns>
    public bool DropCard(Card card)
    {
        return false;
    }

    /// <summary>
    /// 指挥英雄attacker攻击英雄目标target
    /// </summary>
    /// <param name="Attacker">攻击英雄</param>
    /// <param name="target">受击英雄</param>
    /// <returns></returns>
    public bool CommandHeroAttackHero(Hero Attacker, Hero target)
    {
        Debug.Log("英雄" + Attacker.GetName() + "向" + "英雄" + target.GetName() + "发起攻击");

        return false;
    }

    /// <summary>
    /// 指挥英雄attacker攻击玩家目标target
    /// </summary>
    /// <param name="Attacker">攻击英雄</param>
    /// <param name="target">受击玩家</param>
    /// <returns></returns>
    public bool CommandHeroAttackPlayer(Hero Attacker, Player target)
    {

        Debug.Log("英雄" + Attacker.GetName() + "向" + "召唤师发起攻击");
        return false;
    }

    /// <summary>
    /// 将效果施用在英雄目标上
    /// </summary>
    /// <param name="target">英雄目标</param>
    /// <returns></returns>
    public bool CastEffectOnTarget(Hero target)
    {
        return false;
    }

    /// <summary>
    /// 将效果施用在玩家目标上
    /// </summary>
    /// <param name="target">玩家目标</param>
    /// <returns></returns>
    public bool CastEffectOnTarget(Player target)
    {
        return false;
    }

    /*
     * 临时方法
     */
    public int getHp()
    {
        return HP;
    }

     /*
     * 临时方法
     */
    public int getMp()
    {
        return MP;
    }

    /*
     * 临时方法
     */
     public GameObject getCardObject(Card card)
    {
        int index = 0;
        for (; index < CardsInHand.Count; index++)
            if (CardsInHand[index] == card)
                return CardsInHand_Obejct[index];
        return null;
    } 
}
