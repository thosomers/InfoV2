using System;
using System.Collections;
using System.Collections.Generic;
using Pieecs.Scripts.Utils;
using UnityEngine;

public class Selection : MonoBehaviour
{

	public SpriteRenderer renderer;

	public bool enabled { get; private set; }


	public void setEnabled(bool en)
	{
		enabled = en;
		renderer.enabled = en;
	}
	
	public bool getEnabled()
	{
		return enabled;
	}

	public void setColor(BetterColor col)
	{
		renderer.color = new Color(col.R,col.G,col.B,0.6f);
	}

	public BetterColor getColor()
	{
		return new BetterColor(renderer.color);
	}
	
	
	
	
	
	// Use this for initialization
	void Awake ()
	{
		renderer = this.GetComponent<SpriteRenderer>();
		
		setEnabled(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
