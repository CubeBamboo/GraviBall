using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UILogic
{
    public class GamingButton : MonoBehaviour
    {
        #region Gaming

        public void Restart()
        {
            Core.LevelManager.Instance.IsPause = false;
            Framework.SceneTransition.LoadScene(Core.LevelManager.CurrActiveSceneIndex);
            Debug.Log("Restart");
        }

        public void EnterNextLevel()
        {
            Framework.SceneTransition.LoadScene(Core.LevelManager.NextLevelSceneIndex);
        }

        public void BackToMenu()
        {
            Core.LevelManager.Instance.IsPause = false;
            Framework.SceneTransition.LoadScene(Common.Constant.MAIN_MENU_INDEX);
            Debug.Log("Exit");
        }

        public void EnterPausePanel()
        {
            Core.LevelManager.Instance.IsPause = true;
            UIManager.Instance.EnterPausePanel();
        }

        public void ExitPausePanel()
        {
            Core.LevelManager.Instance.IsPause = false;
            UIManager.Instance.ExitPausePanel();
        }

        #endregion

        #region MainMenu

        public void SwitchToSelect()
        {
            UILogic.MenuCanvasMgr.Instance.SwitchToSelect();
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void EnterCredit()
        {
            MenuCanvasMgr.Instance.EnterCredit();
        }

        public void ExitCredit()
        {
            MenuCanvasMgr.Instance.ExitCredit();
        }

        #endregion

        #region SelectLevel

        public void SwitchToMenu()
        {
            UILogic.MenuCanvasMgr.Instance.SwitchToMenu();
        }

        public void EnterLevel1()
        {
            Framework.SceneTransition.LoadScene(Common.Constant.LEVEL_1_INDEX);
        }

        public void EnterLevel2()
        {
            Framework.SceneTransition.LoadScene(Common.Constant.LEVEL_2_INDEX);
        }

        public void EnterLevel3()
        {
            Framework.SceneTransition.LoadScene(Common.Constant.LEVEL_3_INDEX);
        }

        public void EnterLevel4()
        {
            Framework.SceneTransition.LoadScene(Common.Constant.LEVEL_4_INDEX);
        }

        public void EnterLevel5()
        {
            Framework.SceneTransition.LoadScene(Common.Constant.LEVEL_5_INDEX);
        }

        #endregion
    }
}