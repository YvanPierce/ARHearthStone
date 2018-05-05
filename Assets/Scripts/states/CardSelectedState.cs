using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.states
{
    public class CardSelectedState : IAttackState
    {
        public void handleState(God god, string input = "")
        {
            if ("empty" == input)
            {
                god.ClearSelect();
                god.setState(God.emptyState);
            } else if ("selected" == input)
            {
                if (god.currentplayer.SelectedCard.getCardType() == 0)  // 英雄牌，转到卡牌施放状态
                {
                    god.setState(God.targetSelectedState);
                } else      // 效果牌， 转到选择目标状态
                {
                    god.setState(God.targetSelectedState);
                }
            } 
        }

        public void update(God god)
        {
            if (Input.GetKeyDown(KeyCode.Space))    // 空格键 跳转回空白状态
            {
                handleState(god, "empty");
            }
            else if (Input.GetMouseButtonDown(0))     // 检测点击事件
            {
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

                if (hit)
                {
                    Debug.Log("CardSelectedState： Hit " + hitInfo.transform.gameObject.name);
                    god.currentplayer.hitobject = hitInfo.transform.gameObject;

                    if (god.currentplayer.hitobject.tag == "Card")      // 选中卡片
                    {
                        // 当前点击的卡片
                        Card card = god.currentplayer.GetCard(god.currentplayer.hitobject);

                        // 特殊情况：卡片是A键召出来的，不是点击选中时的处理
                        if (null == god.currentplayer.SelectedCard) god.currentplayer.SelectedCard = card;

                        if (god.currentplayer.SelectedCard.Equals(card))    // 两次点击相同，确认使用
                        {
                            handleState(god, "selected");
                        }
                        else      // 点击不同卡片， 不切换状态
                        {
                            god.currentplayer.SelectedCard = card;
                            Debug.Log("当前选中卡牌为: " + god.currentplayer.SelectedCard.GetName() + " 耗费为" + god.currentplayer.SelectedCard.GetCost());
                        }

                    } else  // 选中其他对象
                    {
                        handleState(god, "empty");
                    }
                }
                else    // 点击空白区
                {
                    handleState(god, "empty");
                }
            }

        }
    }
}
