using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace UILogic
{
    public class BGCanvasMgr : Framework.MonoSingletons<BGCanvasMgr>
    {
        private TMPro.TextMeshProUGUI numText;
        private TMPro.TextMeshProUGUI tipsText;

        private RectTransform musicBg;

        [Header("Music Settings")]
        public float punchBpm = 90;
        public float PunchInterval => 60 / punchBpm;

        //TODO: not here. NOOOOOO
        private float levelStartTime;
        private float textFadeInTimeDelay = 0.4f;
        private bool isFadeIn;
        private float textFadeOutTimeDelay = 8f;
        private bool isFadeOut;

        protected override void Awake()
        {
            base.Awake();

            //TODO: ?
            numText = transform.Find("NumText").GetComponent<TMPro.TextMeshProUGUI>();
            tipsText = transform.Find("TipsText").GetComponent<TMPro.TextMeshProUGUI>();
            musicBg = transform.Find("MusicBG").GetComponent<RectTransform>();
        }

        private void Start()
        {
            levelStartTime = Time.time;
            //init Text
            InitText(tipsText);

            BgPunch();
        }

        private void FixedUpdate()
        {
            if (!isFadeIn && Time.time - levelStartTime > textFadeInTimeDelay)
            {
                TextFadeIn(tipsText);
            }

            if (!isFadeOut && Time.time - levelStartTime > textFadeOutTimeDelay)
            {
                TextFadeOut(tipsText);
            }
        }

        private void InitText(TMPro.TextMeshProUGUI text)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }

        private void TextFadeIn(TMPro.TextMeshProUGUI text)
        {
            isFadeIn = true;
            text.DOFade(1, 0.4f);
        }

        private void TextFadeOut(TMPro.TextMeshProUGUI text)
        {
            isFadeOut = true;
            text.DOFade(0, 0.7f);
        }

        public void UpdateNum(int num)
        {
            numText.text = num.ToString();
        }

        private void BgPunch()
        {
            Vector2 bgInitSizeDelta = musicBg.sizeDelta;
            var sequence = DOTween.Sequence(musicBg);
            sequence.Append(musicBg.DOSizeDelta(musicBg.sizeDelta * 1.1f, 0.1f));
            sequence.Append(musicBg.DOSizeDelta(bgInitSizeDelta, 0.3f));
            sequence.AppendInterval(PunchInterval - 0.1f - 0.3f);

            sequence.SetLoops(-1);
        }

        private void OnDestroy()
        {
            musicBg.DOKill();
        }
    }
}