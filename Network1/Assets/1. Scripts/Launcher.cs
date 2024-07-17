using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    LobbyCanvasManager lobbyCanvasManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        lobbyCanvasManager = LobbyCanvasManager.Instance;


        PhotonNetwork.NickName = $"Player {Random.Range(1000, 9999).ToString()}";

        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        lobbyCanvasManager.VisibleOnly("Main");
        Debug.Log("Joined Lobby");
    }

    public void CreateRoom(string _roomName)
    {
        if (string.IsNullOrEmpty(_roomName))
        {
            return;
        }
        PhotonNetwork.CreateRoom(_roomName);
        lobbyCanvasManager.VisibleOnly("Loading");

    }

    public override void OnJoinedRoom()
    {
        lobbyCanvasManager.VisibleOnly("CurrentRoom");
        lobbyCanvasManager.currentRoomNameText.text = PhotonNetwork.CurrentRoom.Name;
        //lobbyCanvasManager.setCurrentRoomNameText(PhotonNetwork.CurrentLobby.Name);
    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        lobbyCanvasManager.visibleError(message);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        lobbyCanvasManager.VisibleOnly("Loading");
    }

    public override void OnLeftRoom()
    {
        lobbyCanvasManager.VisibleOnly("Menu");
    }


}
