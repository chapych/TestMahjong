using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class RemoveTiles : MonoBehaviour
{
	[SerializeField] private float duration = 0.5f;
	
	public Sequence RemoveGroup(IEnumerable<Tile> tilesToRemove)
	{
		Sequence sequence = GetRemovingSequenceFor(tilesToRemove);
		return sequence;
	}
	
	public void RemoveFromPlacedCollections(TilesTypeSO type, IEnumerable<Tile> tilesToRemove, TakenTiles takenTiles)
	{
		foreach (var tile in tilesToRemove)
		{
			takenTiles.PlacedTiles.Remove(tile);
		}
		takenTiles.OccupiedCells -= takenTiles.MaxSameTypeTiles;
		takenTiles.PlacedTypes[type] = 0;
	}
	
	private Sequence GetRemovingSequenceFor(IEnumerable<Tile> tilesToRemove)
	{
		Sequence removingTiles = DOTween.Sequence();
		foreach (Tile tile in tilesToRemove)
		{
			removingTiles.Join(Remove(tile));
		}

		return removingTiles;
	}
	
	private Tween Remove(Tile tile)
	{
		return tile.transform.DOScale(0, duration)
					  .SetEase(Ease.InBack)
					  .OnComplete(()=>
					  {
						Destroy(tile.gameObject);
					  });
	}
}
