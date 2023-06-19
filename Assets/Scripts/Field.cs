using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Field : MonoBehaviour
{
	private Tile[] tiles;
	[Header("Leave null for top layer field")]
	[SerializeField] Field upperField;
	public event Action OnEmptyField;
	
	private void Start()
	{
		tiles = GetComponentsInChildren<Tile>().Where(x => x.transform.parent == transform)
													  .ToArray();
													  
		if(upperField is null) ActivateChildTiles();
		else upperField.OnEmptyField += OnEmptyFieldHandler;
	}
	
	private void OnEmptyFieldHandler()
	{
		ActivateChildTiles();
	}

	private void ActivateChildTiles()
	{
		Debug.Log(tiles.Count());
		foreach (Tile tile in tiles)
		{
			tile.SetActive();
		}
	}
}
