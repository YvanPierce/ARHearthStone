using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.states
{
    public class AttackCompletedState : IAttackState
    {
        public void handleState(God god, string input = "")
        {
            if (null == god.currentplayer.SelectedHero2)
                // 攻击对方召唤师
            {
                god.currentplayer.CommandHeroAttackPlayer(god.currentplayer.SelectedHero1, god.currentplayer.selectedPlayer);
            } else
                // 攻击对方英雄
            {
                god.currentplayer.CommandHeroAttackHero(god.currentplayer.SelectedHero1, god.currentplayer.SelectedHero2);
            }
            
            if (god.isOver())   // 结束
            {
                god.setState(God.gameOverState);
            } else      // 进入空白状态
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
