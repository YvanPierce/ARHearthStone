using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.states
{
    public class RoundExchangeState : IAttackState
    {
        /*
         * 交换回合
         * 进入空白状态
         */ 
        public void handleState(God god, string input = "")
        {
            god.ExchangeRound();
            god.setState(God.emptyState);
            // MP填充
            god.currentplayer.updateMaxMP();
            // 攻击次数填充
            god.currentplayer.updateAttackCount();
        }

        public void update(God god)
        {
            handleState(god);
        }
    }
}
