using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class NetworkManagerCustom : NetworkManager {

	void Start()
	{
		NetworkManager.singleton.autoCreatePlayer = false;
	}

	public void StartupHost()
	{
		SetPort ();
		NetworkManager.singleton.StartHost ();
	}

	public void JoinGame()
	{
		SetIPAddress ();
		SetPort ();
		NetworkManager.singleton.StartClient ();
	}

	void SetPort()
	{
        string port = GameObject.Find("IF_Port").GetComponent<TMP_InputField>().text;
		int int_port = int.Parse (port);
		NetworkManager.singleton.networkPort = int_port;
	}

	void SetIPAddress()
	{
		string ip = GameObject.Find ("IF_IPAddress").GetComponent<TMP_InputField> ().text;
		NetworkManager.singleton.networkAddress = ip;
	}
		
	void OnLevelWasLoaded(int level)
	{
		if (level == 0) 
		{
			StartCoroutine (SetupMenuBtns());
		}
		else if (level == 1) 
		{
			GameObject.Find ("DisconnectBtn").GetComponent<Button> ().onClick.RemoveAllListeners ();
			GameObject.Find ("DisconnectBtn").GetComponent<Button> ().onClick.AddListener (NetworkManager.singleton.StopHost);
			GameObject.Find ("PauseMenu").SetActive (false);
		}
	}

	IEnumerator SetupMenuBtns()
	{
		yield return new WaitForSeconds (0.1f);
		GameObject.Find("HostBtn").GetComponent<Button> ().onClick.RemoveAllListeners ();
		GameObject.Find("HostBtn").GetComponent<Button> ().onClick.AddListener (StartupHost);

		GameObject.Find("JoinBtn").GetComponent<Button> ().onClick.RemoveAllListeners();
		GameObject.Find("JoinBtn").GetComponent<Button> ().onClick.AddListener (JoinGame);
	}

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerID)
    {
        GameObject player;
        List<Transform> startPosititons = NetworkManager.singleton.startPositions;
        if (playerControllerID == -1)
        {
            player = Instantiate(spawnPrefabs[0],startPositions[0].position,startPositions[0].rotation);
            GameObject.Find("SessionInformation").GetComponent<SessionInfo>().SetIsGMExist(true);
            GameObject.Find("SessionInformation").GetComponent<SessionInfo>().AddGMConnection(conn);
        }
        else
        {
            player = GameObject.Instantiate(spawnPrefabs[playerControllerID+1], startPositions[0].position, startPositions[0].rotation);
        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerID);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        base.OnServerDisconnect(conn);
        var sessionInfoComp = GameObject.Find("SessionInformation").GetComponent<SessionInfo>();
        var gmConn = sessionInfoComp.GetGMConnection();
        if (conn == gmConn)
        {
            sessionInfoComp.RemoveGMConnection();
            sessionInfoComp.SetIsGMExist(false);
        }
    }
}
