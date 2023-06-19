using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleted : MonoBehaviour
{
	[SerializeField] private IPanel panel;
	private void OnLevelCompleted()
	{
		panel.Show();
	}
}
