using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.states
{
    public class CardCastedState : IAttackState
    {
        public void handleState(God god, string input = "")
        {
            // 使用卡牌
            //god.currentplayer.UseCard(god.currentplayer.SelectedCard);

            

            if (god.isOver())   // 结束
            {
                god.setState(God.gameOverState);
            }
            else      // 进入空白状态
            {
                god.ClearSelect();
                god.setState(God.emptyState);
            }
        }

        public void update(God god)
        {
            handleState(god);
        }
    }
}
