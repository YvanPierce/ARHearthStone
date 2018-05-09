using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.states
{
    public class GameOverState : IAttackState
    {
        private bool flag = false;
        public void handleState(God god, string input = "")
        {
            Text text = GameObject.Find("Canvas/Text").GetComponent<Text>();
            text.text = "Game Over";
        }

        public void update(God god)
        {
            if (!false)
            {
                handleState(god);
            }
        }
    }
}
