using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.states
{
    public class EmptyState : IAttackState
    {
        public void handleState(God god, string input = "")
        {
            if ("exchange" == input)    // 交换回合
            {
                god.setState(God.roundExchangeState);
            } else if ("attack" == input)   // 进入攻击状态
            {
                god.setState(God.attackPrepareState);
            } else if ("card" == input)     // 进入卡牌选择状态
            {
                //god.currentplayer.DrawCard();
                god.setState(God.cardSelectedState);
            } else if (input == "targetselect")
            {
                god.setState(God.targetSelectedState);
            }
        }

        public void update(God god)
        {
            if (Input.GetKeyDown(KeyCode.Space))    // 空格键结束回合
            {
                handleState(god, "exchange");
            } else if (Input.GetMouseButtonDown(0))     // 检测点击事件
            {
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

                if (hit)
                {
                    Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                    god.currentplayer.hitobject = hitInfo.transform.gameObject;
                    GameObject hitojbect = god.currentplayer.hitobject;

                    if (god.currentplayer.hitobject.tag == "Hero")      // 选中英雄
                    {
                        god.currentplayer.SelectedHero1 = god.currentplayer.GetHero(god.currentplayer.hitobject);
                        if (null != god.currentplayer.SelectedHero1)   // 选中的是本方英雄
                        {
                            Debug.Log("当前选中英雄为: " + god.currentplayer.SelectedHero1.GetName() + " 生命值为" +
                                god.currentplayer.SelectedHero1.GetHp() + " 攻击力为" + god.currentplayer.SelectedHero1.GetDamage());

                            handleState(god, "attack");     // 跳转到攻击准备状态
                        }
                    } else if (hitojbect.tag == "Card")
                    {
                        KeyValuePair<int, Card> cardinstance = god.currentplayer.GetCard(hitojbect);
                        god.currentplayer.SelectedCard = cardinstance;
                        handleState(god, "targetselect");
                    }
                }

            }
        }

    }
}
