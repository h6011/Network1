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
             * OnjoinRoom �� ����ǰ�
             * onsceneloaded �� �Ǿ�
             * 
             * �ؿ��� PhotonNetwork.Instantiate() �� �� �Ǵµ�
             * 
             * �̹� �����濡 ���� onsceneloaded �� ����ǰ� �� 0.1����
             * onjoinroom�� ����Ǽ� �ؿ� �÷��̾ ������ �ȵ�
             * RaiseEvent() �� �ȵ�
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
