using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Move : MonoBehaviour
{
	private Vector3 originalScale;
	Sequence moveAndScaling;
	[SerializeField] private int scailing = 2;
	[SerializeField] private float duration; 
	private void Start()
	{
		originalScale = transform.localScale;
	}
	
	public Sequence MoveTo(Vector3 position)
	{	
		moveAndScaling = DOTween.Sequence();
		
		return moveAndScaling.Append(transform.DOScale(originalScale * scailing, duration))
					  		 .Append(transform.DOMove(position, duration)
		 					 				  .SetEase(Ease.InOutQuad))
					  		 .Append(transform.DOScale(originalScale, duration))	
							 .SetId(gameObject.name);
		
	}
	
	private void OnDestroy()
	{
		DOTween.Kill(gameObject.name);
	}
}
