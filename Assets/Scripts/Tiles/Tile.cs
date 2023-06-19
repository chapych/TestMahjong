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
	Tween moveTween;
	
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
		moveTween = moveComponent.MoveTo(final);
		moveTween.OnComplete(() => {
		OnPlacingCompleted?.Invoke(this, EventArgs.Empty);
		});
	}
	
	public void MoveTo(Vector2 position) 
	{
		var m = moveComponent.MoveTo(position);
		Debug.Log(gameObject.name + " " + m.IsActive() + " " + (position - (Vector2)transform.position).x);
		PositionInLine = position;
		m.OnComplete(() => Debug.Log(gameObject.name + " ended"));
	}
	
	// private void OnDestroy() 
	// {
	// 	DOTween.Kill(this.gameObject);
	// }
	
	
	
}
