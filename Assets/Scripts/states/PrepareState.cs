using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.states
{
    /*
     * 准备状态
     */ 
    public class PrepareState : IAttackState
    {
        /*
         * 初始化场上对阵信息
         * 转到空白状态
         */
        bool Init = false;

        public void handleState(God god, string input = "")
        {

            god.setState(God.emptyState);
        }

        public void update(God god)
        {
            handleState(god);
        }
    }
}
