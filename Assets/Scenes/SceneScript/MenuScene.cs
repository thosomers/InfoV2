using System.Collections;
using System.Collections.Generic;
using System.IO;
using Pieecs.Scripts;
using TMPro;


public class MenuScene : GameScene
{

	public static MenuScene Instance;
	
	// Use this for initialization
	void Start ()
	{
		Instance = this;
		
		foreach (var gameScene in Scenes)
		{
			gameScene.Hide();
		}
        
		MenuScene.Instance.Show();
	}
	
}