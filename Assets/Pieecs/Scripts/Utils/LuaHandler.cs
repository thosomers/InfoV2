using MoonSharp.Interpreter;
using UnityEngine;

namespace Pieecs.Scripts.Utils
{
    public class LuaHandler
    {

        public static Script NewScript()
        {
            Script scr = new Script();
            
            VectorProxy.Setup(scr);
            BetterColor.Setup(scr);
            Board.Setup(scr);
            
            return scr;
        }

        public static void RegisterProxies()
        {
            UserData.RegisterAssembly();
            
            
            UserData.RegisterProxyType<TileProxy, Tile>(v => new TileProxy(v));
            UserData.RegisterProxyType<RobotProxy, Robot>(v => new RobotProxy(v));
            UserData.RegisterProxyType<PlayerProxy, Player>(v => new PlayerProxy(v));
        }
        
        
        
        
    }
}