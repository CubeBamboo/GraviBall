using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class GameManager : Framework.MonoSingletons<GameManager>
    {
        [Header("Assets")]
        public Texture2D cursorTex;

        private void Start()
        {
            InitApplication();
        }

        private void InitApplication()
        {
            Cursor.SetCursor(cursorTex, Vector2.zero, CursorMode.Auto);
        }
    }
}