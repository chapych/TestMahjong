using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReaderSO : ScriptableObject, Controls.IMapActions
{
	public Controls controls;
	public event Action PressEvent;
	public event Action PositionEvent;
	public Vector2 Position
	{
		get => GetPosition();
	}

	private Vector2 GetPosition()
	{
		return controls.Map.Position.ReadValue<Vector2>();
	}

	private void OnEnable()
	{
		if (controls == null)
		{
			controls = new Controls();
			controls.Map.SetCallbacks(this);
			ActionMapEnable();
		}
	}
	
	public void ActionMapEnable()
	{
		controls.Map.Enable();
	}

	public void OnPress(InputAction.CallbackContext context)
	{
		if (context.started)
			PressEvent?.Invoke();
	}
	
	public void OnPosition(InputAction.CallbackContext context)
	{
		PositionEvent?.Invoke();
	}
}