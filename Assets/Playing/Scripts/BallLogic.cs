using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playing
{
    //provide the bounce logic and the trigger logic
    //Tips: destroy it by Core.LevelManager.DestroyBall();
    public class BallLogic : MonoBehaviour
    {
        private Rigidbody2D rb; //provide the gravity and the collision logic(?)

        //public static int totalBallLeft {  get; private set; }

        [Header("Settings")]
        public float bounceVel = 30f;
        public float reflectVel = 6f;

        //Pause Logic Related
        private Vector2 velBeforePause;

        //debug
        //public Vector2 normalDebug;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            if (Core.LevelManager.Instance != null)
            {
                Core.LevelManager.Instance.enterGamePause += EnterGamePause;
                Core.LevelManager.Instance.exitGamePause += ExitGamePause;
            }

            //totalBallLeft++;
        }

        private void OnDestroy()
        {
            if (Core.LevelManager.Instance != null)
            {
                Core.LevelManager.Instance.enterGamePause -= EnterGamePause;
                Core.LevelManager.Instance.exitGamePause -= ExitGamePause;
            }

            //totalBallLeft--;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.CompareTag(Common.Constant.PLATE_TAG))
            {
                var reflectVec = Vector2.Reflect(-other.relativeVelocity.normalized, other.contacts[0].normal);
                SetVelocity(bounceVel * reflectVec);
            }

            if (other.gameObject.CompareTag(Common.Constant.WALL_TAG) || other.gameObject.CompareTag(Common.Constant.PLAYER_TAG))
            {
                var reflectVec = Vector2.Reflect(-other.relativeVelocity.normalized, other.contacts[0].normal);
                SetVelocity(other.relativeVelocity.magnitude * reflectVec);
            }

            if (other.gameObject.CompareTag(Common.Constant.BRICK_NORMAL_TAG) || other.gameObject.CompareTag(Common.Constant.BRICK_DONT_HIT_TAG))
            {
                //brick logic
                other.gameObject.GetComponent<BrickLogic>().OnHit(this);

                //ball logic
                var reflectVec = Vector2.Reflect(-other.relativeVelocity.normalized, other.contacts[0].normal);
                SetVelocity(reflectVec * reflectVel);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.CompareTag(Common.Constant.DIE_TAG))
            {
                Core.LevelManager.Instance.DestroyBall(this);
            }
        }

        public void SetVelocity(Vector2 vel)
        {
            if(vel.y < 0)
            {
                vel = new Vector2(vel.x, vel.y * 0.6f);
            }

            rb.velocity = vel;
        }

        #region PauseLogic

        private void EnterGamePause()
        {
            velBeforePause = rb.velocity;
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        private void ExitGamePause()
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.velocity = velBeforePause;
        }

        #endregion


    }
}