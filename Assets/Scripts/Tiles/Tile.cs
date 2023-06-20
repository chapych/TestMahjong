using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour, IClickable
{
	private Move moveComponent;
	private TileUnblocking tileUnblocking;
	public bool isBlocked = true;
	[SerializeField] public TakenTiles TakenTilesBoard; 
	public TilesTypeSO Type;
	public Vector3 PositionInLine { get; private set; }
	public event EventHandler OnMovingEnded;
	
	private void Start()
	{
		moveComponent = GetComponent<Move>();
		tileUnblocking = GetComponent<TileUnblocking>();
		TileGraphic graphic = GetComponentInChildren<TileGraphic>();

		if(!isBlocked) graphic.TurnBackToNormal();
		OnMovingEnded += TakenTilesBoard.OnMovingEndedHandle;
	}
	
	public void SetActive()
	{
		TileGraphic graphic = GetComponentInChildren<TileGraphic>();
		graphic.TurnBackToNormal();
		isBlocked = false;
	}

	public bool ClickAction()
	{
		if(isBlocked) return false;
		isBlocked = true;
		
		Vector2 final = TakenTilesBoard.Register(this);
		PositionInLine = final;
		Tween moveTween = moveComponent.MoveTo(final);
		
		moveTween.OnComplete(() => 
		{
			OnMovingEnded?.Invoke(this, EventArgs.Empty);
			tileUnblocking.Unblock();
		});
		return true;
	}
	
	public void MoveTo(Vector2 position) 
	{
		var m = moveComponent.MoveTo(position);
		PositionInLine = position;
	}	
}
