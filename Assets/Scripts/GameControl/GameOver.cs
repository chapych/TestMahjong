using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
	[SerializeField] private Panel panel; //Zenject
	[SerializeField] private TakenTiles takenTiles;
	
	private void Start()
	{
		takenTiles.OnAllCellsOccupied += OnAllCellsOccupiedHandler;
	}
	
	private void OnAllCellsOccupiedHandler()
	{
		OnGameOver();
	}
	private void OnGameOver()
	{
		panel.Show();
	}
}
