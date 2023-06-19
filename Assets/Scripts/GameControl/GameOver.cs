using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
	[SerializeField] private IPanel panel;
	private void OnGameOver()
	{
		panel.Show();
	}
}
