using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGraphic : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	
	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		TurnGray();
	}
	
	public void TurnGray()
	{
		spriteRenderer.color = Color.gray;
	}
	
	public void TurnBackToNormal()
	{
		spriteRenderer.color = Color.white;
	}
}
