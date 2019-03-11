using System.Collections;
using System.Collections.Generic;


public class PlayScene : GameScene
{

	public static PlayScene Instance;
	
	public Board gameBoard;

	
	
	// Use this for initialization
	void Start ()
	{
		Instance = this;
	}
	
}