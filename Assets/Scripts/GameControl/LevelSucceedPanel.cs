using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSucceedPanel : Panel
{
	private int max;
	private void Start()
	{
		max = SceneManager.sceneCount;
	}
	public void PlayRandomLevel()
	{
		int current = SceneManager.GetActiveScene().buildIndex;
		while(true)
		{
			int index = UnityEngine.Random.Range(0, max + 1);
			if(index != current) 
			{
				current = index;
				break;
			}
		}
		SceneManager.LoadScene(current);
	}
}
