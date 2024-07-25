using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    MainCanvasManager mainCanvasManager;

    PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public bool PVIsMine()
    {
        return photonView.IsMine;
    }

    private void Start()
    {
        if (PVIsMine())
        {
            mainCanvasManager = MainCanvasManager.Instance;

            createController();
        }
    }

    private void Update()
    {
        if (PVIsMine())
        {
            //mainCanvasManager.SetTestTitle(Time.fixedDeltaTime.ToString());
        }
    }

    private void createController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), new Vector3(0, 10, 0), Quaternion.identity);
    }


}
