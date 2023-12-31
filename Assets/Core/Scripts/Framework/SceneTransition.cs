using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Framework
{
    [RequireComponent(typeof(Image))]
    public class SceneTransition : MonoSingletons<SceneTransition>
    {
        private Image transPanel;

        [Header("Panel Settings")]
        public Color targetColor;
        public float enterInterval = 0.35f, exitInterval = 0.45f;

        //public Image transPanel;
        private Sequence transSequence;
        //private bool isLoading;

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            transPanel = GetComponent<Image>();
        }

        private void Start()
        {
            InitTransPanel();
        }

        private void InitTransPanel()
        {
            transPanel.raycastTarget = false;
            transPanel.color = new Color(targetColor.r, targetColor.g, targetColor.b, 0);
        }

        //
        public static void DoTransition(System.Action onCompelete)
        {
            if(!Instance)
            {
                onCompelete();
                return;
            }

            Instance.P_DoTransition(onCompelete);
        }

        private void P_DoTransition(System.Action onCompelete)
        {
            if (transSequence.IsActive()) return;

            transSequence = DOTween.Sequence(transPanel);
            transPanel.color = new Color(targetColor.r, targetColor.g, targetColor.b, 0);
            transSequence.Append(transPanel.DOFade(1, enterInterval)); //fade in
            transSequence.AppendCallback(() => onCompelete());
            transSequence.AppendInterval(0.1f);
            //TODO: fix bug, callback and the tweening are not in the same sequence, so they play separatly.
            transSequence.Append(transPanel.DOFade(0, exitInterval)); //fade out
        }

        public static void LoadScene(int sceneBuildIndex)
        {
            if(!Instance)
            {
                SceneManager.LoadSceneAsync(sceneBuildIndex);
                return;
            }

            Instance.P_DoTransition(() => Instance.StartCoroutine(Instance.P_LoadSceneCorou(sceneBuildIndex)));
        }

        private IEnumerator P_LoadSceneCorou(int sceneBuildIndex)
        {
            var asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);
            //asyncLoad.allowSceneActivation = false;
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        //private IEnumerator P_LoadScene(int sceneBuildIndex)
        //{
        //    if (transSequence.IsActive()) yield break;

        //    transSequence = DOTween.Sequence(transPanel);
        //    transPanel.gameObject.SetActive(true);
        //    transPanel.color = new Color(targetColor.r, targetColor.g, targetColor.b, 0);
        //    transSequence.Append(transPanel.DOFade(1, 0.35f)); //fade in

        //    var asyncLoad = SceneManager.LoadSceneAsync(sceneBuildIndex);
        //    while (!asyncLoad.isDone)
        //    {
        //        yield return null;
        //    }

        //    yield

        //    transSequence.Append(transPanel.DOFade(0, 0.45f)); //fade out
        //    transSequence.AppendCallback(() => transPanel.gameObject.SetActive(false));
        //}
    }
}