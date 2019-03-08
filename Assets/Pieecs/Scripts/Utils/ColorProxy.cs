using System;
using MoonSharp.Interpreter;
using UnityEngine;


namespace Pieecs.Scripts.Utils
{
    [MoonSharpUserData]
    public class BetterColor
    {
        [MoonSharpHidden]
        public Color color
        {
            get { return new Color(R,G,B);}
        }

        public BetterColor(float r, float g, float b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }
        
        public BetterColor(Color color) : this(color.r,color.g,color.b)
        {
            
        }



        public float R;

        public float G;

        public float B;

        
        
        
        
        public static BetterColor New(float r, float g, float b)
        {
            return new BetterColor(r,g,b);
        }

        public static void Setup(Script script)
        {
            script.Globals["Color"] = (Func<float,float,float,BetterColor >) New;
        }
        
        
        
        
    }
}