using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour 
{
	[SerializeField] private InputReaderSO inputReader;
	[SerializeField] private Camera mainCamera;
	private float detectingRadius = float.Epsilon;
	
	private void Start()
	{
		inputReader.ActionMapEnable();
		inputReader.PressEvent+=OnPressHandle;
	}
	
	private void OnPressHandle()
	{
		Vector3 click = mainCamera.ScreenToWorldPoint(inputReader.Position);
		Collider2D[] colliders = Physics2D.OverlapCircleAll(click, detectingRadius);
		if(colliders.Length == 0) return;
		foreach(var collider in colliders)
		{
			if(collider.TryGetComponent<IClickable>(out IClickable clickable))
			{
				bool hasClicked = clickable.ClickAction();
				if(hasClicked) break;
			}
		}
		
	}
	
	private void OnDisable() 
	{
		inputReader.PressEvent-=OnPressHandle;	
	}
	
}
