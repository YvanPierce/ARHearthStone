﻿using Assets.Scripts;
using Assets.Scripts.states;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour {

    // 单例引用
    private static God god;

    public GameObject red;
    public GameObject yellow;
    public Player playerred;
    public Player playeryellow;
    public Player currentplayer;
    public Player currentoponent;
    public PosContainer redposcontainer;
    public PosContainer yellowposcontainer;

    // 回合计数器
    public int roundCount;

    int PlayerRound; // 0 —— 黄玩家操控回合
                     // 1 —— 红玩家操控回合
    /*
     * 当前状态 
     */
    IAttackState currentState;

    /*
     * 一系列 静态状态
     */ 
    public static PrepareState prepareState;
    public static EmptyState emptyState;
    public static RoundExchangeState roundExchangeState;
    public static AttackPrepareState attackPrepareState;
    public static AttackCompletedState attackCompletedState;
    public static CardSelectedState cardSelectedState;
    public static TargetSelectedState targetSelectedState;
    public static CardCastedState cardCastedState;
    public static GameOverState gameOverState;

    // 动画过程等占用标记，值为false时无法处理输入
    public bool isFree;

	// Use this for initialization
	void Start () {
        
        god = this;

        // 静态状态初始化
        prepareState = new PrepareState();
        emptyState = new EmptyState();
        roundExchangeState = new RoundExchangeState();
        attackPrepareState = new AttackPrepareState();
        attackCompletedState = new AttackCompletedState();
        cardSelectedState = new CardSelectedState();
        targetSelectedState = new TargetSelectedState();
        cardCastedState = new CardCastedState();
        gameOverState = new GameOverState();
        
        // 设置初始状态
        currentState = prepareState;
        isFree = true;

        init();
    }
	
	// Update is called once per frame
	void Update () {
        currentState.update(this);
    }


    /*
     * 初始化对阵信息
     */ 
    public void init()
    {
        red = GameObject.Find("PlayerRed");
        yellow = GameObject.Find("PlayerYellow");
        playerred = red.GetComponent<Player>();
        redposcontainer = red.GetComponent<PosContainer>();
        playerred.setPlayerObject(red);
        playeryellow = yellow.GetComponent<Player>();
        yellowposcontainer = yellow.GetComponent<PosContainer>();
        playeryellow.setPlayerObject(yellow);

        // 开局选边扩展点
        Debug.Log("黄色方先手");
        PlayerRound = 0;
        currentplayer = playeryellow;
        currentoponent = playerred;
        roundCount = 1;
        Debug.Log("第" + roundCount + "回合开始");

        GameObject redcardslots = GameObject.Find("RedCardSlots");
        Transform[] rcs = redcardslots.GetComponentsInChildren<Transform>();
        GameObject yellowcardslots = GameObject.Find("YellowCardSlots");
        Transform[] ycs = yellowcardslots.GetComponentsInChildren<Transform>();

        for (int i = 0; i < rcs.Length; i++)
        {
            redposcontainer.CardsPos.Add(rcs[i].position);
            yellowposcontainer.CardsPos.Add(ycs[i].position);
        }

        CSVReader.GetInstance().loadFile(Application.dataPath + "/CardsConfigs", "卡组csv.csv"); // 读取存储卡牌描述的CSV文件
        CardsManager.GetInstance().SetCardsData(CSVReader.GetInstance().arrayData); // 将读取到的数据传入卡牌管理器
        CardsManager.GetInstance().InstansitateCards(); // 卡牌管理器根据读取的数据来实例化卡牌


        currentplayer.Shuffle();
        currentoponent.Shuffle();
        currentplayer.DrawCard();
    }

    public bool ExchangeRound()
    {
        if (PlayerRound == 0)
        {
            Debug.Log("黄方结束回合");
            PlayerRound = 1;
            currentplayer = playerred;
            currentoponent = playeryellow;
        }
        else if (PlayerRound == 1)
        {
            Debug.Log("红方结束回合");
            PlayerRound = 0;
            currentplayer = playeryellow;
            currentoponent = playerred;
            // 回合数+1
            roundCount++;
            Debug.Log("第" + roundCount + "回合开始");
        }
        currentplayer.DrawCard();
        ClearSelect();
        return true;
    }

    public void ClearSelect()
    {

        currentplayer.SelectedHero1 = null;
        currentplayer.SelectedHero2 = null;
        currentplayer.SelectedCard = new KeyValuePair<int, Card>(-1,null);
        currentplayer.selectedPlayer = null;

        currentoponent.SelectedHero1 = null;
        currentoponent.SelectedHero2 = null;
        currentoponent.SelectedCard = new KeyValuePair<int, Card>(-1, null);
        currentoponent.selectedPlayer = null;
    }

    /*
     * 状态转换
     */ 
    public void setState(IAttackState state)
    {
        Debug.Log("状态转换： 转换到" + state.ToString());
        currentState = state;
    }

    /*
     * 结束检查
     */ 
    public bool isOver()
    {
        if (playerred.HP == 0 || playeryellow.HP == 0) return true;
        return false;
    }

    /**
     * 获取导演实例
     */
    public static God getInstance()
    {
        return god;
    }
}
