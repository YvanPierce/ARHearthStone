using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }

        public void update(God god)
        {
            handleState(god);
        }
    }
}
