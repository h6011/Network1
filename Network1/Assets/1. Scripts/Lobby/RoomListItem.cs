using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    Launcher launcher;

    [SerializeField] TMP_Text titleText;
    Button btn;

    RoomInfo roomInfo;

    public RoomInfo RoomInfo => roomInfo;

    private void btnCheck()
    {
        if (btn == null)
        {
            btn = transform.GetComponent<Button>();
        }
    }

    private void launcherCheck()
    {
        if (launcher == null)
        {
            launcher = Launcher.Instance;
        }
    }


    public void SetUp(RoomInfo _roomInfo)
    {
        btnCheck();
        launcherCheck();
        roomInfo = _roomInfo;
        titleText.text = $"[{_roomInfo.PlayerCount}/{_roomInfo.MaxPlayers}] {_roomInfo.Name}";

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => {
            launcher.JoinRoom(roomInfo);
        });

    }



}
