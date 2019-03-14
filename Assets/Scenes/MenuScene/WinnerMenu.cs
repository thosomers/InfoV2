using System.Collections;
using System.Collections.Generic;
using Pieecs.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinnerMenu : Menu
{
	public static WinnerMenu Instance;
	
	
	public Text Winner;
	public Text TurnCount;
	
	
	
	
	public void ShowWinner()
	{
		this.ShowOnly();

		Winner.text = Game.LastWinner;
		TurnCount.text = Game.LastTurns.ToString();

	}

	public override void Hide()
	{
		this.MenuObject.SetActive(false);
	}


	private void Awake()
	{
		Instance = this;
	}
}

