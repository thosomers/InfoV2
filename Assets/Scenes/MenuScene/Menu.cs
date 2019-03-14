using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

	public GameObject MenuObject;

	public string Name;

	public static HashSet<Menu> menus = new HashSet<Menu>();

	public Menu()
	{
		menus.Add(this);
	}
	
	
	
	
	public virtual void ShowOnly()
	{
		foreach (var menu in menus)
		{
			if (menu == this) menu.Show();
			else menu.Hide();
		}
	}
	
	
	
	
	
	
	public virtual void Show()
	{
		this.MenuObject.SetActive(true);
	}

	public virtual void Hide()
	{
		this.MenuObject.SetActive(false);
	}
	
	
	
}

