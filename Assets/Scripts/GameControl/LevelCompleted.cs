using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleted : MonoBehaviour
{
	[SerializeField] private Panel panel; //Zenject
	[SerializeField] private TakenTiles takenTiles;
	[SerializeField] private Field field;
	private void Awake()
	{
		field.OnFieldIsEmpty += OnFieldIsEmptyHandler;
	}
	
	private void OnFieldIsEmptyHandler()
	{
		if(takenTiles.OccupiedCells == 0)
			OnLevelCompleted();
	}
	
	private void OnLevelCompleted()
	{
		panel.Show();
	}
}
