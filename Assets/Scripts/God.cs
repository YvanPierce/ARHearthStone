using Assets.Scripts;
using Assets.Scripts.states;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour {

    public GameObject red;
    public GameObject yellow;
    public Player playerred;
    public Player playeryellow;
    public Player currentplayer;
    public Player currentoponent;
    public PosContainer redposcontainer;
    public PosContainer yellowposcontainer;
    
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
	// Use this for initialization
	void Start () {
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
        playeryellow = yellow.GetComponent<Player>();
        yellowposcontainer = yellow.GetComponent<PosContainer>();

        // 开局选边扩展点
        Debug.Log("游戏开始，黄色方先手");
        PlayerRound = 0;
        currentplayer = playeryellow;
        currentoponent = playerred;

        yellowposcontainer.CardsPos.Add(new Vector3(-.32f, -5.9f, -10.8f));
        yellowposcontainer.HeroPos.Add(new Vector3(-.14f, -2.53f, -10.97f));

        redposcontainer.HeroPos.Add(new Vector3(0, 3.54f, -10.34f));
        redposcontainer.CardsPos.Add(new Vector3(-.32f, 5.76f, -9.41f));
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
        }
        ClearSelect();
        return true;
    }

    public void ClearSelect()
    {
        currentplayer.SelectState = "Select Nothing";
        currentplayer.SelectedHero1 = null;
        currentplayer.SelectedHero2 = null;
        currentplayer.SelectedCard = null;
        currentplayer.selectedPlayer = null;
        currentoponent.SelectState = "Select Nothing";
        currentoponent.SelectedHero1 = null;
        currentoponent.SelectedHero2 = null;
        currentoponent.SelectedCard = null;
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
        if (playerred.getHp() == 0 || playeryellow.getHp() == 0) return true;
        return false;
    }
}
