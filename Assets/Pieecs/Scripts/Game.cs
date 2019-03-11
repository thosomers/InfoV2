using System.Collections;
using UnityEngine;

namespace Pieecs.Scripts
{
    public static class Game
    {

        private static Coroutine coroutine;
        public static ConsoleController Console { get; set; }


        public static IEnumerator gameIterator()
        {
            bool first = true;
            
            
            for (;;)
            {
                if (first)
                {
                    first = false;
                    yield return new WaitForSeconds(1f);
                }
                
                
                
                if (!Player.IsRunning)
                {
                    Player.DoTurn();
                    yield return new WaitForSeconds(0.2f);
                }

                yield return null;

            }

        }


        public static void Start(string p1Script, string p2Script)
        {
            if (coroutine != null) Board.Instance.StopCoroutine(coroutine);
            
            Board.Setup();
            Player.Setup();
            Console.Setup(Player.Player1.Script);
            Console.Setup(Player.Player2.Script);
            
            Player.Player1.setText(p1Script);
            Player.Player2.setText(p2Script);
            
            
            coroutine = Board.Instance.StartCoroutine(gameIterator());
        }

        public static void End(Player winner)
        {
            Board.Instance.StopCoroutine(coroutine);
            
            Debug.Log("Winner: Player" + (winner == Player.Player1 ? "1" : "2"));
        }
        
        
        
        
        
        
        
    }
}