using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour, IClickable
{
	private Move moveComponent;
	private bool isBlocked = true;
	[SerializeField] private TakenTiles takenTiles; //Zenjec
	public TilesTypeSO Type;
	public Vector3 PositionInLine { get; private set; }
	public event EventHandler OnPlacingCompleted;
	
	private void Awake()
	{
		moveComponent = GetComponent<Move>();
		
		OnPlacingCompleted += takenTiles.OnPlacingCompletedHandle;
	}
	
	public void SetActive()
	{
		isBlocked = false;
	}

	public void ClickAction()
	{
		if(isBlocked) return;
		isBlocked = true;
		
		Vector2 final = takenTiles.GetAvailablePosition(this);
		PositionInLine = final;
		Tween moveTween = moveComponent.MoveTo(final);
		
		moveTween.OnComplete(() => {
		OnPlacingCompleted?.Invoke(this, EventArgs.Empty);
		});
	}
	
	public void MoveTo(Vector2 position) 
	{
		Debug.Log(moveComponent);
		moveComponent.MoveTo(position);
		PositionInLine = position;
	}
	
	
	
}
