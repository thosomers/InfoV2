using System;
using System.Collections;
using System.Collections.Generic;
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

	public void setColor(Color col)
	{
		renderer.color = col;
	}

	public Color getColor()
	{
		return renderer.color;
	}
	
	
	
	
	
	// Use this for initialization
	void Start ()
	{
		renderer = this.GetComponent<SpriteRenderer>();
		
		setEnabled(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
