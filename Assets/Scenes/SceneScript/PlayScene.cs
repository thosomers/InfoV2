using System.Collections;
using System.Collections.Generic;


public class PlayScene : GameScene
{

	public static PlayScene Instance;
	
	public Board gameBoard;
	
	public ConsoleController console;

	
	
	// Use this for initialization
	void Start ()
	{
		Instance = this;
	}
	
}