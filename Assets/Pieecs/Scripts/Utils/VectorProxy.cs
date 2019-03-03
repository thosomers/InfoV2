using System;
using MoonSharp.Interpreter;
using UnityEngine;

namespace Pieecs.Scripts.Utils
{
    [MoonSharpUserData]
    public class VectorProxy
    {
        public Vector2 Vector;

        public VectorProxy(Vector2 vector)
        {
            Vector = vector;
        }

        public VectorProxy(float x, float y) : this(new Vector2(x,y))
        {
            
        }
        

        public float X
        {
            get { return Vector.x; }
            set { Vector.x = value; }
        }

        public float Y
        {
            get { return Vector.y; }
            set { Vector.y = value; }
        }

        public static VectorProxy operator +(VectorProxy a, VectorProxy b)
        {
            return new VectorProxy(a.Vector + b.Vector);
        }
        
        public static VectorProxy operator -(VectorProxy a, VectorProxy b)
        {
            return new VectorProxy(a.Vector - b.Vector);
        }
        
        public static VectorProxy operator -(VectorProxy a)
        {
            return new VectorProxy(-a.X,-a.Y);
        }

        public static VectorProxy operator *(int n, VectorProxy a)
        {
            return new VectorProxy(n*a.X,n*a.Y);
        }

        public static void Setup(Script script)
        {

            script.Globals["Vector"] = (Func<float, float, VectorProxy>) ((x,y) => new VectorProxy(x, y));
            


        }

        public override string ToString()
        {
            return "Vector(" + X + "," + Y + ")";
        }


        public float Length
        {
            get
            {
                return Math.Abs(X) + Math.Abs(Y);
            }
        }
        
    }
}