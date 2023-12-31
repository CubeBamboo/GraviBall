using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UILogic
{
    public class UIManager : Framework.MonoSingletons<UIManager>
    {
        public GameObject gameFailPanel;
        public GameObject gameWinPanel;
        public GameObject gamePausePanel;
        public GameObject gamingPanel;
        
        public void OnGameFail()
        {
            gamingPanel.SetActive(false); //TODO: switch to foreach and disable
            gameFailPanel.SetActive(true);
        }

        public void OnGameWin()
        {
            gamingPanel.SetActive(false);
            gameWinPanel.SetActive(true);
        }

        public void EnterPausePanel()
        {
            gamingPanel.SetActive(false);
            gamePausePanel.SetActive(true);
        }

        public void ExitPausePanel()
        {
            gamePausePanel.SetActive(false);
            gamingPanel.SetActive(true);
        }
    }
}