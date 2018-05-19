using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class PlayerController : NetworkBehaviour 
{
	private Vector2 targetPos;
	private bool wasPressed = false;
    private Vector2 dirVector;
    private Rigidbody2D rgb2d;

	void Start()
	{
        rgb2d = GetComponent<Rigidbody2D>();

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
            dirVector = targetPos - (Vector2)transform.position;
		}
        if (wasPressed)
            rgb2d.AddForce(dirVector,ForceMode2D.Force);

        if (Vector2.Distance((Vector2)transform.position, targetPos) < 0.1)
        {
            rgb2d.isKinematic = true;
            rgb2d.velocity = Vector2.zero;
            rgb2d.angularVelocity = 0f;
            rgb2d.isKinematic = false;
        }
            
    }
}
