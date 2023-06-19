using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour 
{
	[SerializeField] private InputReaderSO inputReader;
	[SerializeField] private Camera mainCamera;
	private float detectingRadius = 0.1f;
	
	private void Start()
	{
		inputReader.ActionMapEnable();
		inputReader.PressEvent+=OnPressHandle;
	}
	
	private void OnPressHandle()
	{
		Vector3 click = mainCamera.ScreenToWorldPoint(inputReader.Position);
		Collider2D collider = Physics2D.OverlapCircle(click, detectingRadius);
		if(collider == null) return;
		if(collider.TryGetComponent<IClickable>(out IClickable clickable))
		{
			clickable.ClickAction();
		}
	}
	
	private void OnDisable() 
	{
		inputReader.PressEvent-=OnPressHandle;	
	}
	
}
