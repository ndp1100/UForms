﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace UForms.Controls.Fields
{
    public class MaskField : AbstractField<int>
    {
        public List<string> Options { get; private set; }

        public MaskField( Vector2 position, Vector2 size, int value = 0, string[] options = default(string[]), string label = "" ) : base( position, size, value, label )
        {
            Options = new List<string>();

            if ( options != null )
            {
                Options.AddRange( options );
            }
        }

        protected override int DrawAndUpdateValue()
        {            
            return EditorGUI.MaskField( m_fieldRect, Label, m_cachedValue, Options.ToArray() );
        }

        protected override bool TestValueEquality( int oldval, int newval )
        {            
            return oldval.Equals( newval );
        }
    }
}