using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Field : MonoBehaviour
{
	private Tile[] tiles;
	private int tilesCount;
	[SerializeField] TakenTiles takenTiles;
	public event Action OnFieldIsEmpty;
	
	private void Awake()
	{
		tiles = GetComponentsInChildren<Tile>().ToArray();
		tilesCount = tiles.Length;
		takenTiles.OnEmpty += OnEmptyHandle;
		foreach (Tile tile in tiles)
		{
			tile.OnMovingEnded += OnMovingEndedHandle;
			tile.TakenTilesBoard = takenTiles;
		}
	}
	
	private void OnEmptyHandle()
	{
		Debug.Log(tilesCount);
		if(tilesCount == 0)
			OnFieldIsEmpty?.Invoke();
	}
	
	private void OnMovingEndedHandle(object sender, EventArgs e) => tilesCount--;
}
