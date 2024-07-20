using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UIElements;

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
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        lobbyCanvasManager.VisibleOnly("Main");
        Debug.Log("Joined Lobby");
    }

    

    public override void OnJoinedRoom()
    {
        lobbyCanvasManager.VisibleOnly("CurrentRoom");
        lobbyCanvasManager.currentRoomNameText.text = PhotonNetwork.CurrentRoom.Name;
        //lobbyCanvasManager.setCurrentRoomNameText(PhotonNetwork.CurrentLobby.Name);

        addAllPlayerListItemPrefab();

        lobbyCanvasManager.startGameBtn.gameObject.SetActive(PhotonNetwork.IsMasterClient);
    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        lobbyCanvasManager.visibleError(message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        addPlayerListItemPrefab(newPlayer);
    }

    

    public override void OnLeftRoom()
    {
        lobbyCanvasManager.VisibleOnly("Main");
        clearRoomList();
        //clearRoomList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.LogWarning("OnRoomListUpdate");

        int roomListCount = roomList.Count;
        for (int i = 0; i < roomListCount; i++)
        {
            RoomInfo _roomInfo = roomList[i];

            bool RemovedFromList = _roomInfo.RemovedFromList;

            Debug.Log($"Name: {_roomInfo.Name}, visible: {_roomInfo.IsVisible}, RemovedFromList: {_roomInfo.RemovedFromList}, IsOpen: {_roomInfo.IsOpen}");


            if (RemovedFromList)
            {
                Debug.Log("RemovedFromList");
                int count1 = lobbyCanvasManager.roomListContent.childCount;
                for (int i2 = 0; i2 < count1; i2++)
                {
                    Transform _child = lobbyCanvasManager.roomListContent.GetChild(i2);
                    RoomListItem roomListItem = _child.GetComponent<RoomListItem>();
                    if (roomListItem.RoomInfo.Name == _roomInfo.Name)
                    {
                        Destroy(_child.gameObject);
                        Debug.Log("Destroyed");
                    }

                }
            }
            else
            {
                GameObject newItem = Instantiate(lobbyCanvasManager.roomListItemPrefab, lobbyCanvasManager.roomListContent);
                newItem.name = _roomInfo.Name;
                RoomListItem roomListItem = newItem.GetComponent<RoomListItem>();
                roomListItem.SetUp(_roomInfo);
            }




        }





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
    public void StartGame()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        lobbyCanvasManager.VisibleOnly("Loading");
    }

    public void JoinRoom(RoomInfo _roomInfo)
    {
        PhotonNetwork.JoinRoom(_roomInfo.Name);
        lobbyCanvasManager.VisibleOnly("Loading");

        //addAllPlayerListItemPrefab();

    }


    

    private void clearRoomList()
    {
        int count1 = lobbyCanvasManager.roomListContent.childCount;
        for (int i2 = 0; i2 < count1; i2++)
        {
            Transform _child = lobbyCanvasManager.roomListContent.GetChild(i2);
            Destroy(_child.gameObject);

        }
    }

    

    private void addPlayerListItemPrefab(Player newPlayer)
    {
        Debug.Log("addPlayerListItemPrefab");
        GameObject newPlayerListItem = Instantiate(lobbyCanvasManager.playerListItemPrefab, lobbyCanvasManager.playerListContent);
        PlayerListItem playerListItem = newPlayerListItem.GetComponent<PlayerListItem>();
        playerListItem.SetUp(newPlayer);
    }

    private void addAllPlayerListItemPrefab()
    {
        Player[] players = PhotonNetwork.PlayerList;
        int count = players.Length;

        Debug.Log(count);

        for (int i = 0; i < count; i++)
        {
            Player _player = players[i];
            addPlayerListItemPrefab(_player);
        }
    }
    













}
