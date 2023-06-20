using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TileUnblocking : MonoBehaviour
{
	[SerializeField] private Tile[] blockedTiles;
	
	public void Unblock()
	{
		foreach(var tile in blockedTiles)
			tile.SetActive();
	}
}