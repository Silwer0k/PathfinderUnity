using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GMController : NetworkBehaviour {

	public float camSpeed = 10f;
	public float borderThickness = 10f;
	
	// Update is called once per frame
	void Update () 
	{
		if (!isLocalPlayer)
			return;

		Vector3 pos = transform.position;

		if (Input.mousePosition.y >= Screen.height - borderThickness) 
		{
			pos.y += camSpeed * Time.deltaTime;
		}
		if (Input.mousePosition.y <= borderThickness) 
		{
			pos.y -= camSpeed * Time.deltaTime;
		}
		if (Input.mousePosition.x >= Screen.width - borderThickness) 
		{
			pos.x += camSpeed * Time.deltaTime;
		}
		if (Input.mousePosition.x <= borderThickness) 
		{
			pos.x -= camSpeed * Time.deltaTime;
		}

		transform.position = pos;
	}
}
