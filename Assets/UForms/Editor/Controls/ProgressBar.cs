﻿using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UForms.Controls
{
    public class ProgressBar : Control
    {
        public string           Text           { get; set; }
        public float            Progress       { get; set; }

        private Rect            m_boxRect;

        protected override Vector2 DefaultSize {
            get { return new Vector2( 200.0f, 16.0f ); }
        }

        public ProgressBar( string text = "", float progress = 0.0f ) : base()
        {
            Text     = text;
            Progress = progress;
        }


        public ProgressBar( Vector2 position, Vector2 size, string text = "", float progress = 0.0f ) : base( position, size )
        {            
            Text     = text;
            Progress = progress;
        }


        protected override void OnLayout()
        {
            m_boxRect.Set(
                ScreenPosition.x + MarginLeftTop.x,
                ScreenPosition.y + MarginLeftTop.y,
                Size.x,
                Size.y
            );
        }


        protected override void OnDraw()
        {
            Progress = Mathf.Clamp01( Progress );
            EditorGUI.ProgressBar( m_boxRect, Progress, Text );
        }
    }
}