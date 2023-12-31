using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomEventSys
{
    public class BallFeedBackSource : MonoBehaviour
    {
        //TODO: OnHitBrick
        public static event System.Action CameraShakeNotify;
        public static event System.Action<Vector2> BrickParticleNotify;
        //public static event System.Action HitBrickSFXNotify, HitPlateSFXNotify;

        private void Start()
        {
            //HitBrickSFXNotify +=

        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(Common.Constant.BRICK_NORMAL_TAG) || other.gameObject.CompareTag(Common.Constant.BRICK_DONT_HIT_TAG))
            {
                CameraShakeNotify?.Invoke();
                BrickParticleNotify?.Invoke(other.transform.position);
                PlayHitBrickSFX();
            }

            if (other.gameObject.CompareTag(Common.Constant.PLATE_TAG) || other.gameObject.CompareTag(Common.Constant.WALL_TAG) || other.gameObject.CompareTag(Common.Constant.PLAYER_TAG) )
            {
                PlayHitPlateSFX();   
            }
        }

        private void Update()
        {
#if UNITY_EDITOR
            DebugUpdate();
#endif
        }

        private void DebugUpdate()
        {
            var keyboard = UnityEngine.InputSystem.Keyboard.current;

            if(keyboard.vKey.wasPressedThisFrame)
            {
                CameraShakeNotify?.Invoke();

            }


        }

        #region ActionFunc

        private void PlayHitBrickSFX()
        {
            AudioMgr.AudioManager.Instance.PlayHitBrickSFX();
        }

        private void PlayHitPlateSFX()
        {
            AudioMgr.AudioManager.Instance.PlayHitPlateSFX();
        }

        #endregion
    }
}