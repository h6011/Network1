using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }






    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Game")
        {
            /*
             * OnjoinRoom 이 실행되고
             * onsceneloaded 가 되야
             * 
             * 밑에줄 PhotonNetwork.Instantiate() 가 잘 되는데
             * 
             * 이미 열린방에 들어가면 onsceneloaded 가 실행되고 약 0.1초후
             * onjoinroom이 실행되서 밑에 플레이어가 생성이 안됨
             * RaiseEvent() 가 안됨
             * 
             */
            Debug.Log(Time.time);
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(Time.time);
        base.OnJoinedRoom();
    }






}
