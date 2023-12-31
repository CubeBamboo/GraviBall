using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioMgr
{
    public class AudioManager : Framework.MonoSingletons<AudioManager>
    {
        private AudioSource bgmSource;
        private AudioSource sfxSource;
        public ScriptableObj.AudioClipsData clipsData;

        public float minSfxPlayTime; //min sfxPlay Interval.
        private float lastSfxPlayTime; //sfxPlaying Timer.

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            bgmSource = gameObject.AddComponent<AudioSource>();
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        private void Start()
        {
            PlayBgm();
        }

        //private void Update()
        //{
        //    //SfxSourceVolumnUpdate();
        //}

        #region PlayAudio

        /// <summary>
        /// To play bgm. Call On GameStart.
        /// </summary>
        private void PlayBgm()
        {
            bgmSource.clip = clipsData.bgm;
            bgmSource.loop = true;
            bgmSource.Play();
        }

        private void PlaySfxSound(AudioClip audioClip, float pitch = 1f)
        {
            if (Time.time - lastSfxPlayTime < minSfxPlayTime) return;

            sfxSource.clip = audioClip;
            sfxSource.pitch = pitch;
            sfxSource.Play();
            lastSfxPlayTime = Time.time;
        }
        
        public void PlayHitBrickSFX()
        {
            PlaySfxSound(clipsData.hitBrickSFX, Random.Range(0.8f, 1.2f));
        }

        public void PlayHitPlateSFX()
        {
            PlaySfxSound(clipsData.hitPlateSFX, Random.Range(0.8f, 1.2f));
        }

        //public void PlayButtonHighlightedSFX()
        //{
        //    PlaySfxSound(clipsData.buttonHighlightedSFX);
        //}

        //public void PlayButtonPressedSFX()
        //{
        //    PlaySfxSound(clipsData.buttonPressedSFX);
        //}

        #endregion

        #region Other

        public bool IsSFXPlaying()
        {
            return sfxSource.isPlaying;
        }

        #endregion
    }
}