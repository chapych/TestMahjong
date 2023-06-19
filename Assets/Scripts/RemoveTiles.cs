using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RemoveTiles : MonoBehaviour
{
	private LinkedList<Tile> placedTiles;
	[SerializeField] private float duration = 0.5f;
	
	private void Start()
	{
		placedTiles = GetComponent<TakenTiles>().PlacedTiles;
	}
	public Tween Remove(Tile tile)
	{
		return tile.transform.DOScale(0, duration)
					  .SetEase(Ease.InBack)
					  .OnComplete(()=>
					  {
						
						// placedTiles.Remove(tile);
						//Debug.Log(tile.DOKill());
						Destroy(tile.gameObject);
					  });
	}
}
