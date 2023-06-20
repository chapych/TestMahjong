using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Panel : MonoBehaviour, IPanel
{
	private CanvasGroup group;
	
	private void Awake()
	{
		group = GetComponent<CanvasGroup>();
		Hide();
	}
	public void Hide()
	{
		group.alpha = 0;
		group.interactable = false;
		group.blocksRaycasts = false;
	}

	public void Replay()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Show()
	{
		group.alpha = 1;
		group.interactable = true;
		group.blocksRaycasts = true;
	}
}
