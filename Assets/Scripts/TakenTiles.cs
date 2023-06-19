using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class TakenTiles : MonoBehaviour
{
	RemoveTiles removeTiles;
	private Dictionary<TilesTypeSO, int> placedTypes = new Dictionary<TilesTypeSO, int>();
	public LinkedList<Tile> PlacedTiles = new LinkedList<Tile>();
	public int AvailableCells = 7;
	private int occupiedCells = 0;
	public float cellsDistance = 30;
	private int maxSameTypeTiles = 3;
	
	private void Start()
	{
		removeTiles = GetComponent<RemoveTiles>();
	}

	public Vector2 GetAvailablePosition(Tile tile)
	{
		Vector3 shift = cellsDistance * new Vector3(1, 0, 0); 
		Vector2 cellPosition;
		
		TilesTypeSO type = tile.Type;
		
		if(!placedTypes.ContainsKey(type)) placedTypes[type] = 0;
			placedTypes[type]++;
			
		if (placedTypes[type]>1)
		{
			cellPosition = GetPositionWithShifting(tile, shift, type);
		}
		else
		{
			cellPosition = GetPositionWithoutShifting(tile, shift);
		}
		occupiedCells++;
		
		return cellPosition;
	}

	private Vector2 GetPositionWithoutShifting(Tile tile, Vector3 shift)
	{
		Vector2 cellPosition = transform.position + occupiedCells * shift;
		PlacedTiles.AddLast(tile);
		return cellPosition;
	}

	private Vector2 GetPositionWithShifting(Tile tile, Vector3 shift, TilesTypeSO type)
	{
		Vector2 cellPosition;
		LinkedListNode<Tile> lastMatching = PlacedTiles.Nodes()
															.Where(HasSameType(type))
															.LastOrDefault();
		PlacedTiles.ActionStarting(lastMatching.Next, x => x.MoveTo(x.PositionInLine + shift));
		PlacedTiles.AddAfter(lastMatching, tile);

		cellPosition = (Vector2)(lastMatching.Value.PositionInLine + shift);
		return cellPosition;
	}

	private Func<LinkedListNode<Tile>, bool> HasSameType(TilesTypeSO type)
	{
		return x => x.Value.Type == type;
	}
	public void OnPlacingCompletedHandle(object sender, EventArgs e)
	{
		TilesTypeSO type = (sender as Tile).Type;
		
		if(placedTypes[type] == maxSameTypeTiles)
		{
			Vector3 shift = cellsDistance * new Vector3(maxSameTypeTiles, 0, 0);
			var tilesToRemove = PlacedTiles.Nodes()
										   .Where(HasSameType(type))
										   .ToList();
			LinkedListNode<Tile> nextTile = tilesToRemove.Last().Next;
			foreach(var tile in tilesToRemove)
			{
				PlacedTiles.Remove(tile);
			}
			occupiedCells -= maxSameTypeTiles;
			placedTypes[type] = 0;
			
			Sequence removingTiles = DOTween.Sequence();
			
			foreach(Tile tile in tilesToRemove.Select(x => x.Value))
			{
				removingTiles.Join(removeTiles.Remove(tile));
			}
			
			if(nextTile == null) return;
			removingTiles.OnComplete(() => PlacedTiles.ActionStarting(nextTile, 
																		x => x.MoveTo(x.PositionInLine - shift)));
		}
	}
}
