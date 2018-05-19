using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChooseRole : MonoBehaviour {

	public GameObject sessionInfo;

	public void SetupGameMaster()
	{
		ClientScene.AddPlayer (-1);
	}

	void Update()
	{	
		if (sessionInfo.GetComponent<SessionInfo> ().GetIsGMExist ())
			GameObject.Find ("GMBtn").GetComponent<Button> ().interactable = false;
		else
			GameObject.Find ("GMBtn").GetComponent<Button> ().interactable = true;
	}
}
