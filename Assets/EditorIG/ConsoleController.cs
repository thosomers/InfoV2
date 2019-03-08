using System;
using System.Collections;
using System.Collections.Generic;
using MoonSharp.Interpreter;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ConsoleController : MonoBehaviour
{

	public GameObject ConsoleTextPrefab;

	public Transform ConsoleContent;

	public Color NormalColor;
	public Color ErrorColor;



	void print(string str)
	{
		var go = GameObject.Instantiate<GameObject>(ConsoleTextPrefab, ConsoleContent);

		var text = go.GetComponent<Text>();
		text.color = NormalColor;
		text.text = str;
	}
	
	void print(object[] objs)
	{
		foreach (var o in objs)
		{
			print(o.ToString());
		}
	}

	void print(DynValue val)
	{
		if (val.Type == DataType.Tuple)
		{
			foreach (var dynValue in val.Tuple)
			{
				print(dynValue);
			}
		}
		else
		{
			print(val.ToPrintString());
		}
	}

	public void Setup(Script script)
	{
		script.Globals["print"] = (Action<DynValue>) print;
	}
	
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
