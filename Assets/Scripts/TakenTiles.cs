using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class TakenTiles : MonoBehaviour
{
	private RemoveTiles removeTiles;
	public Dictionary<TilesTypeSO, int> PlacedTypes = new Dictionary<TilesTypeSO, int>();
	public LinkedList<Tile> PlacedTiles = new LinkedList<Tile>();
	public int availableCells = 7;
	public int OccupiedCells = 0;
	public float cellsDistance = 30;
	public int MaxSameTypeTiles = 3;
	private Vector3 shift;
	public event Action OnAllCellsOccupied;
	public event Action OnEmpty;
	
	private void Start()
	{
		removeTiles = GetComponent<RemoveTiles>();
		
		shift = cellsDistance * new Vector3(1, 0, 0);
	}

	public Vector2 Register(Tile tile)
	{
		Vector2 placedPosition;
		TilesTypeSO type = tile.Type;

		if (!PlacedTypes.ContainsKey(type)) PlacedTypes[type] = 0;
		PlacedTypes[type]++;

		placedPosition = GetPosition(tile, type);
		
		OccupiedCells++;
		return placedPosition;
	}

	private Vector2 GetPosition(Tile tile, TilesTypeSO type)
	{
		Vector2 position;
		switch (PlacedTypes[type])
		{
			case > 1:
				position = GetPositionWithShifting(tile, type);
				break;
			default:
				position = GetPositionWithoutShifting(tile);
				break;
		}
		return position;
	}

	private Vector2 GetPositionWithoutShifting(Tile tile)
	{
		Vector2 cellPosition = transform.position + OccupiedCells * shift;
		PlacedTiles.AddLast(tile);
		return cellPosition;
	}

	private Vector2 GetPositionWithShifting(Tile tile, TilesTypeSO type)
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
	public void OnMovingEndedHandle(object sender, EventArgs e)
	{
		TilesTypeSO type = (sender as Tile).Type;
		Vector3 shift = cellsDistance * new Vector3(MaxSameTypeTiles, 0, 0);
		if(PlacedTypes[type] == MaxSameTypeTiles)
		{
			var nodesToRemove = PlacedTiles.Nodes()
										   .Where(HasSameType(type))
										   .Take(MaxSameTypeTiles);
			LinkedListNode<Tile> nextNode = nodesToRemove.Last().Next;

			var tilesToRemove = nodesToRemove.Select(x => x.Value)
											 .ToList();
			removeTiles.RemoveFromPlacedCollections(type, tilesToRemove, this);
			Sequence removingTiles = removeTiles.RemoveGroup(tilesToRemove);

			removingTiles.OnComplete(() => PlacedTiles.ActionStarting(nextNode,
																		x => x.MoveTo(x.PositionInLine - shift)));
		}
		else if (OccupiedCells == availableCells)
		{
			OnAllCellsOccupied?.Invoke();
		}
		if(OccupiedCells == 0) OnEmpty?.Invoke();
	}
	
	public void ChangeMaxAvailableCells(int max) => availableCells = max;
	
	private Func<LinkedListNode<Tile>, bool> HasSameType(TilesTypeSO type)
	{
		return x => x.Value.Type == type;
	}
}
