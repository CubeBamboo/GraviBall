using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UILogic
{
    public class MenuCanvasMgr : Framework.MonoSingletons<MenuCanvasMgr>
    {
        public Image transPanel;
        public GameObject menuPanel, selectPanel;
        public GameObject creditPanel;

        public void SwitchToMenu()
        {
            DoTransition(P_SwitchToMenu);
        }

        public void SwitchToSelect()
        {
            DoTransition(P_SwitchToSelect);
        }

        private void P_SwitchToMenu()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name == "TransPanel") continue;
                transform.GetChild(i).gameObject.SetActive(false);
            }

            menuPanel.SetActive(true);
        }

        private void P_SwitchToSelect()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name == "TransPanel") continue;
                transform.GetChild(i).gameObject.SetActive(false);
            }

            selectPanel.SetActive(true);
        }

        public void EnterCredit()
        {
            DoTransition(() => { creditPanel.SetActive(true); });
        }

        public void ExitCredit()
        {
            DoTransition(() => { creditPanel.SetActive(false); });
        }

        private void DoTransition(System.Action onComplete)
        {
            transPanel.gameObject.SetActive(true);

            var sequence = DOTween.Sequence();
            sequence.Append(transPanel.DOFade(1, 0.15f));
            sequence.AppendCallback(() => onComplete?.Invoke());
            sequence.Append(transPanel.DOFade(0, 0.2f));
            sequence.AppendCallback(() => { transPanel.gameObject.SetActive(false); });
        }
    }
}