using System.Collections;
using DefaultNamespace;
using UnityEngine;

namespace Pieecs.Scripts
{
    public static class Game
    {

        private static Coroutine coroutine;
        public static ConsoleController Console { get; set; }

        public static string LastWinner;
        public static int LastTurns;


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

            Console = PlayScene.Instance.console;
            Console.Clear();
            
            
            Board.Setup();
            Player.Setup();
            Console.Setup(Player.Player1.Script);
            Console.Setup(Player.Player2.Script);
            
            Player.Player1.setText(p1Script);
            Player.Player2.setText(p2Script);


            FlyCam.EnableMove(true);
            coroutine = Board.Instance.StartCoroutine(gameIterator());
        }

        public static void End(Player winner)
        {
            Debug.Log("WINNNNN    1");
            
            Board.Instance.StopCoroutine(coroutine);

            LastWinner = (winner == Player.Player1 ? "Player 1" : "Player 2");
            LastTurns = Player.TurnCount;
            
            Debug.Log("Winner: " + LastWinner);
            
            FlyCam.EnableMove(false);
            MenuScene.Instance.Show();

            
            Debug.Log("WINNNNN");
            WinnerMenu.Instance.ShowWinner();
            
            PlayScene.Instance.Hide();
        }

        public static void Terminate()
        {
            Board.Instance.StopCoroutine(coroutine);
            
            Debug.Log("Terminated");
            
            FlyCam.EnableMove(false);
            MenuScene.Instance.Show();
            PlayScene.Instance.Hide();
        }
        
        
        
        
        
        
    }
}