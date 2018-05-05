using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public interface IAttackState
    {
        void handleState(God god, string input = "");
        void update(God god);
    }

}
