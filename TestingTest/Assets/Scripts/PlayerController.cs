using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerController : NetworkBehaviour 
{
	public float movement_speed = 2;
	private Vector2 targetPos;
	private bool wasPressed = false;

	void Start()
	{
		if (!isLocalPlayer) 
		{
            gameObject.GetComponent<Camera>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isLocalPlayer)
			return;
		
		if (Input.GetMouseButtonDown(1))
		{
			wasPressed = true;
			Camera playerCam = gameObject.GetComponent<Camera>();
			Vector2 pos = Input.mousePosition;
			targetPos = playerCam.ScreenToWorldPoint(pos);
		}
        if (wasPressed)
            MovePlayer();

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
	}

	void MovePlayer()
	{
		transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * movement_speed);
	}
}
