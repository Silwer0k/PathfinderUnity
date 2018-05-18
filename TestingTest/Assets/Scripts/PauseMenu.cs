using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	private bool isActive = false;
	public GameObject pauseMenu;

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			isActive = !isActive;
            pauseMenu.SetActive (isActive);
		}
	}
}
