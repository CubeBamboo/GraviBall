using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playing
{
    public class BrickLogic : MonoBehaviour
    {
        public MyEnum.BrickType brickType;
        public bool needToBeat = true;
        public bool interactable = true;

        //private static int totalBrickCnt;
        //public static int TotalBrickCnt
        //{
        //    get => totalBrickCnt;
        //    private set
        //    {
        //        totalBrickCnt = value;
        //        Core.LevelManager.Instance.DetectBrick();
        //    }
        //}

        private void Start()
        {
            //Init();
        }

        //private void Init()
        //{
        //    if (!needToBeat) return;
        //    if (!interactable) return;

        //    TotalBrickCnt++;

        //}

        public void OnHit(Playing.BallLogic other)
        {
            if (!interactable) return;

            switch(brickType)
            {
                case MyEnum.BrickType.Normal:
                    Core.LevelManager.Instance.OnMeetHitBrick();
                    break;
                case MyEnum.BrickType.Sepaprate:
                    //not elegant :<
                    float xMin = -10f, xMax = 10f, yMin = -2f, yMax = 10f;
                    Core.LevelManager.Instance.CreateANewBall(transform.position, new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax)));
                    Core.LevelManager.Instance.OnMeetHitBrick();
                    break;
                case MyEnum.BrickType.Die:
                    Core.LevelManager.Instance.DestroyBall(other);
                    break;
            }
            
            Destroy(gameObject);
        }
    }
}