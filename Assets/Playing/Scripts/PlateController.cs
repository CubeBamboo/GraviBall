using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playing
{
    public class PlateController : MonoBehaviour
    {
        private Camera mainCamera;
        private Vector2 mouseScreenPos;
        private Vector2 targetPos;

        private bool isPause;

        //Input
        private bool fireInputHeld;

        [Header("Settings")]
        public float moveScale = 0.12f;
        public float rotateScale = 0.12f;
        public float rotateVal = 20f;
        private float PosY => transform.position.y;

        private void Awake()
        {
            //Init camera
            var cameraArr = GameObject.FindGameObjectsWithTag("MainCamera");
            foreach (var camera in cameraArr)
            {
                if(camera.name == "Playing Camera")
                {
                    mainCamera = camera.GetComponent<Camera>(); break;
                }
            }
            Debug.Assert(mainCamera != null);

            //
        }

        private void Start()
        {
            targetPos = transform.position;

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

        private void FixedUpdate()
        {
            if (isPause) return;
            
            if (fireInputHeld)
            {
                float offset = 150;
                mouseScreenPos = new Vector2(Mathf.Clamp(mouseScreenPos.x, 0+offset, 1920-offset), mouseScreenPos.y);
                targetPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
            }
            Move(targetPos.x);
            MoveRotation(targetPos.x);

        }

        #region InputCallbackFunc


        public void OnMoveInput(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            mouseScreenPos = ctx.ReadValue<Vector2>();
        }

        public void OnFireInput(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
        {
            //if(ctx.ReadValue<float>() != 0f)
            switch(ctx.phase)
            {
                case UnityEngine.InputSystem.InputActionPhase.Started:
                    fireInputHeld = true;
                    break;
                case UnityEngine.InputSystem.InputActionPhase.Canceled:
                    fireInputHeld = false;
                    break;
            }
        }

        #endregion

        private void Move(float targetX)
        {
            //do position
            if(Mathf.Abs(transform.position.x-targetX) < 0.01f)
            {
                transform.position = new Vector2(targetX, PosY);
                return;
            }

            //transform.position = posNextFrame
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, targetX, moveScale), PosY);
        }

        private void MoveRotation(float targetX)
        {
            //no input
            if (!fireInputHeld || Mathf.Abs(transform.position.x - targetX) < 1f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, rotateScale);
                return;
            }

            //else do rotation
            if (targetX < transform.position.x) //move left
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -rotateVal), rotateScale);
            }
            else if (targetX > transform.position.x) //move right
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, rotateVal), rotateScale);
            }
        }

        #region GamePauseLogic

        private void EnterGamePause()
        {
            isPause = true;
        }

        private void ExitGamePause()
        {
            isPause = false;
        }

        #endregion
    }
}