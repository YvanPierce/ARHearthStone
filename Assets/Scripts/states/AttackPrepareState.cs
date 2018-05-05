using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.states
{
    public class AttackPrepareState : IAttackState
    {
        public void handleState(God god, string input = "")
        {
            if("empty" == input)    // 返回空白状态
            {
                god.ClearSelect();  // 清除所有点击记录
                god.setState(God.emptyState);
            } else if ("attack" == input)   // 进入攻击完成状态
            {
                god.setState(God.attackCompletedState);
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
                    Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                    god.currentplayer.hitobject = hitInfo.transform.gameObject;

                    if (god.currentplayer.hitobject.tag == "Hero")      // 选中英雄
                    {
                        Hero selectedHero = god.currentplayer.GetHero(god.currentplayer.hitobject);
                        if (null != selectedHero)   // 选中本方英雄，不切换状态
                        {
                            Debug.Log("当前选中英雄为: " + god.currentplayer.SelectedHero1.GetName() + " 生命值为" +
                                god.currentplayer.SelectedHero1.GetHp() + " 攻击力为" + god.currentplayer.SelectedHero1.GetDamage());
                            god.currentplayer.SelectedHero1 = selectedHero;     // 更新选中
                        }
                        else      // 选中敌方英雄，进入下一状态
                        {
                            god.currentplayer.SelectedHero2 = god.currentoponent.GetHero(god.currentplayer.hitobject);
                            handleState(god, "attack");
                        }

                    }
                    else if ("Player" == god.currentplayer.hitobject.tag)   // 选中召唤师
                    {
                        if (god.currentplayer.hitobject.Equals(god.red))
                        {
                            god.currentplayer.selectedPlayer = god.playerred;
                        } else
                        {
                            god.currentplayer.selectedPlayer = god.playeryellow;
                        }
                        handleState(god, "attack");
                    }
                    else      // 选中其他物体
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
