using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.states
{
    public class AttackCompletedState : IAttackState
    {
        // 标记位，标记是否为首次调用update
        private bool flag = true;

        // 状态切换
        public void handleState(God god, string input = "")
        {   
            if (god.isOver())   // 结束
            {
                god.setState(God.gameOverState);
            } else      // 进入空白状态
            {
                god.ClearSelect();
                god.setState(God.emptyState);
            }
            flag = true;
        }

        public void update(God god)
        {
            if (flag)   // 第一次进入update：开始攻击
            {
                attack();
                flag = false;
            } else      // 攻击已开始，等待攻击过程结束，切换状态
            {
                if (god.isFree) handleState(god);
            }
        }

        /**
         * 调用player的攻击方法
         */ 
        public void attack()
        {
            if (null == God.getInstance().currentplayer.SelectedHero2)
            // 攻击召唤师
            {
                God.getInstance().currentplayer.CommandHeroAttackPlayer(
                    God.getInstance().currentplayer.SelectedHero1, God.getInstance().currentplayer.selectedPlayer);
            }
            else
            // 攻击对方英雄
            {
                God.getInstance().currentplayer.CommandHeroAttackHero(
                    God.getInstance().currentplayer.SelectedHero1, God.getInstance().currentplayer.SelectedHero2);
            }
        }

    }
}
