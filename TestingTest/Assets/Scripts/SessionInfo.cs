using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SessionInfo : NetworkBehaviour {

	[SyncVar]
	private bool isGM_exist = false;
	[SyncVar]
	private int countOfConnections = 0;
	private NetworkConnection gmConnection = null;

	public bool GetIsGMExist()
	{
		return isGM_exist;
	}

	public void SetIsGMExist(bool flag)
	{
		isGM_exist = flag;
	}

	public int GetCountOfConnections()
	{
		return countOfConnections;
	}

	public void AddConnection()
	{
		countOfConnections += 1;
	}

	public void RemoveConnection()
	{
		countOfConnections -= 1;
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
