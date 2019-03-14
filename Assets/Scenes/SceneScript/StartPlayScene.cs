using System.Collections;
using System.Collections.Generic;
using System.IO;
using Pieecs.Scripts;
using TMPro;


public class StartPlayScene : GameScene
{

	public static StartPlayScene Instance;

	public TMP_Text P1Script;
	public TMP_Text P2Script;
	
	
	
	private string p1;
	private string p2;

	public void SetP1(FileInfo i)
	{
		P1Script.text = i.Name;
		p1 = i.FullName;
	}
	
	public void SetP2(FileInfo i)
	{
		P2Script.text = i.Name;
		p2 = i.FullName;
	}


	public void StartGame()
	{

		if (File.Exists(p1) && File.Exists(p2))
		{
			PlayScene.Instance.Show();
			this.Hide();
			Game.Start(File.ReadAllText(p1),File.ReadAllText(p2));
		}
		
	}
	
	
	// Use this for initialization
	void Start ()
	{
		Instance = this;
	}
	
}