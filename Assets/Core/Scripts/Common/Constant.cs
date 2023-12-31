using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class Constant
    {
        #region Tag

        public const string PLATE_TAG = "Plate";
        public const string BRICK_NORMAL_TAG = "Brick_Normal";
        public const string BRICK_DONT_HIT_TAG = "Brick_DontHit";
        public const string WALL_TAG = "Wall";
        public const string DIE_TAG = "Die";
        public const string PLAYER_TAG = "Player";

        #endregion

        #region SceneIndex

        public const int MAIN_MENU_INDEX = 0;
        public const int LEVEL_1_INDEX = 1;
        public const int LEVEL_2_INDEX = 2;
        public const int LEVEL_3_INDEX = 3;
        public const int LEVEL_4_INDEX = 4;
        public const int LEVEL_5_INDEX = 5;
        public const int LEVEL_OUT_OF_RANGE_INDEX = 6; //TODO: switch to enum.

        #endregion

    }
}