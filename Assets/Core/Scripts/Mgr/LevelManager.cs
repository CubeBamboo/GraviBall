using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class LevelManager : Framework.MonoSingletons<LevelManager>
    {
        private float loopTimer;
        private float loopTimeBuffer = 3f;
        private bool isCreatingABall;

        [Header("LevelSetting")]
        
        public ScriptableObj.Numerical numerical;
        public GameObject ballPrefab;

        //data on single play
        private int ballRemainCnt;
        private int ballOnSceneCnt;
        private int brickNeedHitCnt;
        private bool isGameEnd;

        //private float timer;

        //global Setting
        private bool isPause;
        public bool IsPause
        {
            get => isPause;
            set
            {
                switch(value)
                {
                    case true:
                        enterGamePause?.Invoke(); break;
                    case false:
                        exitGamePause?.Invoke(); break;
                }
            }
        }

        public event System.Action enterGamePause, exitGamePause;

#if UNITY_EDITOR
        //Debug
        public Vector2 scrollPosition;
#endif


        public static int CurrActiveSceneIndex => SceneManager.GetActiveScene().buildIndex;
        public static int NextLevelSceneIndex
        {
            get
            {
                var currIndex = SceneManager.GetActiveScene().buildIndex;
                if (currIndex + 1 < Common.Constant.LEVEL_OUT_OF_RANGE_INDEX)
                {
                    return currIndex + 1;
                }
                else
                {
                    return Common.Constant.MAIN_MENU_INDEX;
                }
            }
        }

        #region UnityEventFunc

        private void Start()
        {
            InitPlayingData();
            InitGame();
        }

        private void Update()
        {
#if UNITY_EDITOR
            DebugUpdate();
#endif
        }

        private void FixedUpdate()
        {
            LoopFunc();
        }

#if UNITY_EDITOR

        private void OnGUI()
        {
            GUIStyle leftTopStyle = new GUIStyle();
            leftTopStyle.normal.background = null;
            leftTopStyle.normal.textColor = Color.black;
            leftTopStyle.fontSize = 20;

            // An absolute-positioned example: We make a scrollview that has a really large client
            // rect and put it in a small rect on the screen.
            scrollPosition = GUI.BeginScrollView(new Rect(10, 10, 300, 400), scrollPosition, new Rect(0, 0, 400, 600));


            GUI.Label(new Rect(10, 10, 80, 200), "vKey: ResetBall", leftTopStyle);
            GUI.Label(new Rect(10, 60, 80, 200), "ballOnSceneCnt: " + ballOnSceneCnt, leftTopStyle);
            GUI.Label(new Rect(10, 110, 80, 200), "brickNeedHitCnt: " + brickNeedHitCnt, leftTopStyle);
            //GUI.Label(new Rect(10, 60, 80, 200), "ballNormal:" + ball.normalDebug, leftTopStyle);

            // End the scroll view that we began above.
            GUI.EndScrollView();
        }

#endif

        #endregion

        private void LoopFunc()
        {
            loopTimer -= Time.fixedDeltaTime;
            if(loopTimer <= 0)
            {
                loopTimer = loopTimeBuffer;
                //Debug.Log("loop");
                
                //Loop Func
                DetectBall();
                DetectBrick();
            }
        }

        private void InitPlayingData()
        {
            isGameEnd = false;
            ballRemainCnt = numerical != null ? numerical.initBallLeftCnt : 0;

            var balls = GameObject.FindGameObjectsWithTag(Common.Constant.PLAYER_TAG);
            ballOnSceneCnt = balls.Length;

            DetectBall();
            InitBrickCnt();
        }

        private void InitGame()
        {
            UILogic.BGCanvasMgr.Instance?.UpdateNum(ballRemainCnt);
        }

        private void InitBrickCnt()
        {
            var bricks = GameObject.FindGameObjectsWithTag(Common.Constant.BRICK_NORMAL_TAG); //TODO: Tag Name
            brickNeedHitCnt = bricks.Length;
        }

        #region CallbackFunc

        public void OnMeetHitBrick()
        {
            Instance.brickNeedHitCnt--;
            Instance.DetectBrick();
        }

        public void DetectBrick()
        {
            if (isGameEnd) return; //TODO: it's shit.

            //Debug.Log("BrickDetect");

            if(brickNeedHitCnt <= 0)
            {
                OnGameWin();
            }
        }

        public void DestroyBall(Playing.BallLogic ball)
        {
            if (isGameEnd) return; //TODO: it's shit.

            Destroy(ball.gameObject);
            //Debug.Log("CurrTotalBallLeft:" + Playing.BallLogic.totalBallLeft);
            ballOnSceneCnt--;
            
            DetectBall();
        }

        private void DetectBall()
        {
            if (ballOnSceneCnt <= 0) //we need create a new ball
            {
                if (ballRemainCnt - 1 < 0) //game over detect
                {
                    OnGameFail();
                    return;
                }
                else //wait 2 seconds and relese a new ball
                {
                    if (isCreatingABall) return;

                    Debug.Log("ANewBall");
                    isCreatingABall = true;
                    Framework.CoroutineMgr.Instance.FuncDelay(() => {
                        ballRemainCnt--;
                        CreateANewBall(new Vector2(0, 7.5f), Vector2.zero);
                        UILogic.BGCanvasMgr.Instance?.UpdateNum(ballRemainCnt);
                        isCreatingABall = false;
                    }, 2f);
                }
            }
        }

        #endregion

        public void CreateANewBall(Vector2 position, Vector2 initSpeed)
        {
            var ball = Instantiate(ballPrefab, position, Quaternion.identity).GetComponent<Playing.BallLogic>();
            ball.SetVelocity(initSpeed);
            ballOnSceneCnt++;
        }

        private void OnGameWin()
        {
            //Framework.SceneTransitionController.Instance.transPanel.DOFade(0.5f, 0.6f);
            //Framework.CoroutineMgr.Instance.FuncDelay(UILogic.UIManager.Instance.OnGameWin, 0.6f);
            //Framework.SceneTransitionController.DoTransition(() => {  });
            if (isGameEnd) return;

            isGameEnd = true;
            //Destroy ball
            var ball = GameObject.FindWithTag("Player"); if(ball != null) Destroy(ball.gameObject);
            UILogic.UIManager.Instance.OnGameWin();
        }

        private void OnGameFail()
        {
            //Framework.SceneTransitionController.Instance.transPanel.DOFade(0.5f, 0.6f);
            //Framework.CoroutineMgr.Instance.FuncDelay(UILogic.UIManager.Instance.OnGameFail, 0.6f);
            //Framework.SceneTransitionController.DoTransition(() => {  });
            if (isGameEnd) return;

            isGameEnd = true;
            
            UILogic.UIManager.Instance.OnGameFail();
        }

        private void DebugUpdate()
        {
            var keyboard = UnityEngine.InputSystem.Keyboard.current;

            if(keyboard.vKey.wasPressedThisFrame)
            {
                Framework.SceneTransition.DoTransition(() => { Debug.Log("End"); });
            }

            //if (keyboard.bKey.wasPressedThisFrame)
            //{
            //    Framework.SceneTransition.DoTransition(
            //        () => { Debug.Log("transition"); });

            //}
        }
    }
}