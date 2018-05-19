using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SessionInfo : NetworkBehaviour {

	[SyncVar]
	private bool isGM_exist = false;
	private NetworkConnection gmConnection = null;

	public bool GetIsGMExist()
	{
		return isGM_exist;
	}

	public void SetIsGMExist(bool flag)
	{
		isGM_exist = flag;
	}

	public void AddGMConnection(NetworkConnection conn)
	{
		gmConnection = conn;
	}

	public NetworkConnection GetGMConnection()
	{
		return gmConnection;
	}

	public void RemoveGMConnection()
	{
		gmConnection = null;
	}
}
