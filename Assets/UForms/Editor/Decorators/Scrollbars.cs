﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

using UForms.Controls;

namespace UForms.Decorators
{
    public class Scrollbars : Decorator
    {
        public bool VerticalScrollbar { get; set; }
        public bool HorizontalScrollbar { get; set; }
        public bool HandleMouseWheel { get; set; }

        private Vector2 m_scrollPosition;
        private Rect m_scrollableRect;
        private bool m_doScrollbars;

        private const float SCROLLBAR_SIZE = 16.0f;


        public Scrollbars()
            : base()
        {
            VerticalScrollbar = true;
            HorizontalScrollbar = true;
            HandleMouseWheel = true;
        }


        public Scrollbars( bool verticalScrollbar, bool horizontalScrollbar, bool handleMouseWheel )
            : base()
        {
            VerticalScrollbar = verticalScrollbar;
            HorizontalScrollbar = horizontalScrollbar;
            HandleMouseWheel = handleMouseWheel;
        }


        protected override void OnLayout()
        {
            m_boundControl.ResetPivotRoot = m_boundControl.ResetPivotRoot || m_doScrollbars;
        }

        protected override void OnBeforeDraw()
        {
            m_doScrollbars = false;

            Rect content = m_boundControl.GetContentBounds();

            // Hack for Control.GetContentBounds breaking with children with negative values...
            float xMax = Mathf.Max( content.xMax, m_boundControl.ScreenRect.xMax );
            float yMax = Mathf.Max( content.yMax, m_boundControl.ScreenRect.yMax );

            content.Set(
                content.xMin,
                content.yMin,
                xMax - content.xMin, 
                yMax - content.yMin
            );

            if ( content.xMin < m_boundControl.ScreenRect.xMin || content.yMin < m_boundControl.ScreenRect.yMin || content.xMax > m_boundControl.ScreenRect.xMax - m_boundControl.MarginLeftTop.x - m_boundControl.MarginRightBottom.x || content.yMax > m_boundControl.ScreenRect.yMax - m_boundControl.MarginLeftTop.y - m_boundControl.MarginRightBottom.y )
            {
                m_doScrollbars = true;
            }

            if ( m_doScrollbars )
            {
                float w,h;
                w = ( HorizontalScrollbar ? content.width : m_boundControl.Size.x - m_boundControl.MarginLeftTop.x - m_boundControl.MarginRightBottom.x - SCROLLBAR_SIZE );
                h = ( VerticalScrollbar ? content.height : m_boundControl.Size.y - m_boundControl.MarginLeftTop.y - m_boundControl.MarginRightBottom.y - SCROLLBAR_SIZE );
                m_scrollableRect.Set( content.xMin, content.yMin, w, h );

                m_scrollPosition = GUI.BeginScrollView( m_boundControl.ScreenRect, m_scrollPosition, m_scrollableRect );
                m_boundControl.ViewportOffset = new Vector2( content.xMin + m_scrollPosition.x, content.yMin + m_scrollPosition.y );
            }
            else
            {
                m_boundControl.ViewportOffset = Vector2.zero;
            }
        }

        protected override void OnAfterDraw()
        {
            if ( m_doScrollbars )
            {
                GUI.EndScrollView( HandleMouseWheel );
            }
        }
    }
}