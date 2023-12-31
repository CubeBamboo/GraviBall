using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class CoroutineMgr : MonoSingletons<CoroutineMgr>
    {
        private bool isPause;

        private void Start()
        {
            //event
            if (Core.LevelManager.Instance != null)
            {
                Core.LevelManager.Instance.enterGamePause += EnterGamePause;
                Core.LevelManager.Instance.exitGamePause += ExitGamePause;
            }
        }

        private void OnDestroy()
        {
            //event
            if (Core.LevelManager.Instance != null)
            {
                Core.LevelManager.Instance.enterGamePause -= EnterGamePause;
                Core.LevelManager.Instance.exitGamePause += ExitGamePause;
            }
        }

        private void EnterGamePause()
        {
            isPause = true;
        }

        private void ExitGamePause()
        {
            isPause = false;
        }

        public void FuncDelay(System.Action Func, float delayTime)
        {
            StartCoroutine(P_FuncDelay(Func, delayTime));
        }

        private IEnumerator P_FuncDelay(System.Action Func, float delayTime)
        {
            yield return new WaitForSeconds(delayTime);
            while(isPause) yield return null; //TODO: ...i dont know how to explain it, but it's not a right way to pause
            Func();
        }
    }
}