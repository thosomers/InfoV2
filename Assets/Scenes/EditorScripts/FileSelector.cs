using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace.Scenes;
using Pieecs.Scripts;
using UnityEngine;

public class FileSelector : MonoBehaviour
{
	public GameObject ItemParent;

	public SelectableFile Current = null;

	public GameObject ItemPrefab;

	public EditorPanel Editor;

	// Use this for initialization
	void Start ()
	{
		Refresh();

	}

	public void Refresh()
	{
		var ccount = ItemParent.transform.childCount;

		for (int i = 0; i < ccount; i++)
		{
			GameObject.Destroy(ItemParent.transform.GetChild(i).gameObject);
		}
		
		foreach (var file in Directory.GetFiles(Path.Combine(Application.dataPath, "lua"), "*.lua"))
		{
			GameObject obj = GameObject.Instantiate(ItemPrefab, ItemParent.transform);

			SelectableFile select = obj.GetComponent<SelectableFile>();
			select.Setup(file);

		}
		
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setCurrent(SelectableFile selectableFile)
	{
		Current = selectableFile;

		Editor.Open(Current.MyFile);

	}
}
