using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.states
{
    public class TargetSelectedState : IAttackState
    {
        public void handleState(God god, string input = "")
        {
            if ("empty" == input)
            {
                god.ClearSelect();
                god.setState(God.emptyState);
            } else if (input == "SelectSlot")
            {

            }
        }

        public void update(God god)
        {
            if (Input.GetMouseButtonDown(0))     // 检测点击事件
            {
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

                if (hit)
                {
                    Debug.Log("CardSelectedState： Hit " + hitInfo.transform.gameObject.name);
                    god.currentplayer.hitobject = hitInfo.transform.gameObject;
                    GameObject hitobject = god.currentplayer.hitobject;

                    if (hitobject.tag == "Slot")      // 选中卡槽
                    {
                        Vector3 SlotPos = hitobject.transform.position;
                        if (hitobject.transform.parent.name == "YellowHeroSlots" && god.currentplayer == god.playeryellow)
                        {
                            god.currentplayer.UseCard(god.currentplayer.SelectedCard, SlotPos);
                        } else if (hitobject.transform.parent.name == "RedHeroSlots" && god.currentplayer == god.playerred)
                        {
                            god.currentplayer.UseCard(god.currentplayer.SelectedCard, SlotPos);
                        }
                        handleState(god, "empty");
                    }
                    else  // 选中其他对象
                    {
                        handleState(god, "empty");
                    }
                }

                else    // 未击中任何物体
                {
                    handleState(god, "empty");
                }
            }
        }
    }
}
