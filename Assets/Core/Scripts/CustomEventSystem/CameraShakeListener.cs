using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace CustomEventSys
{
    public class CameraShakeListener : MonoBehaviour
    {
        private Camera m_Camera;
        private Vector3 initPos;

        private void Awake()
        {
            m_Camera = GetComponent<Camera>();
        }

        private void Start()
        {
            initPos = transform.position;
        }

        private void OnEnable()
        {
            BallFeedBackSource.CameraShakeNotify += Shake;
        }

        private void OnDisable()
        {
            BallFeedBackSource.CameraShakeNotify -= Shake;
        }

        private void Shake()
        {
            m_Camera.DOShakePosition(0.3f, 0.1f).OnComplete(()=>{ transform.position = initPos; });
        }    
    }
}