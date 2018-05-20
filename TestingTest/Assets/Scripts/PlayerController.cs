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
    private GameObject goalObj;
    private bool isAchieved = false;

    public GameObject movementGoalPref;

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
            if (goalObj != null)
                Destroy(goalObj);
            isAchieved = false;
			wasPressed = true;
			Camera playerCam = gameObject.GetComponent<Camera>();
			Vector2 pos = Input.mousePosition;
			targetPos = playerCam.ScreenToWorldPoint(pos);
            goalObj = GameObject.Instantiate(movementGoalPref, targetPos, movementGoalPref.transform.rotation);
            dirVector = targetPos - (Vector2)transform.position;
		}
        if (wasPressed)
            rgb2d.AddForceAtPosition(dirVector, targetPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == goalObj)
        {
            Destroy(goalObj);
            rgb2d.velocity = Vector3.zero;
            wasPressed = false;
            isAchieved = true;
        }
    }
}
