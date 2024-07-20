using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LobbyCanvasManager : MonoBehaviour
{
    public static LobbyCanvasManager Instance;

    Launcher launcher;

    [Header("Text")]
    [SerializeField] TMP_Text errorText;
    public TMP_Text currentRoomNameText;

    [Header("Button")]
    [SerializeField] Button findRoomBtn;
    [SerializeField] Button createRoomBtn;
    [SerializeField] Button quitGameBtn;

    [Space]
    [SerializeField] Button createRoomCreateBtn;

    [Space]
    public Button leaveRoomBtn;
    public Button startGameBtn;

    [Header("Input Field")]
    [SerializeField] TMP_InputField createRoomNameInputField;

    [Space]
    public Transform roomListContent;
    public GameObject roomListItemPrefab;

    [Space]
    public Transform playerListContent;
    public GameObject playerListItemPrefab;





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

        VisibleOnly("Loading");
        autoBackBtnAction();
        buttonClickAction();
    }

    private void Start()
    {
        launcher = Launcher.Instance;
    }

    private void buttonClickAction()
    {
        addListenerToBtn(findRoomBtn, () => {
            VisibleOnly("FindRoom");
        });
        addListenerToBtn(createRoomBtn, () => {
            VisibleOnly("CreateRoom");
        });
        addListenerToBtn(quitGameBtn, () => {
            QuitGame();
        });


        addListenerToBtn(createRoomCreateBtn, () => {
            launcher.CreateRoom(createRoomNameInputField.text);
        });

        addListenerToBtn(leaveRoomBtn, () => {
            launcher.LeaveRoom();
        });

        addListenerToBtn(startGameBtn, () => {
            launcher.StartGame();
        });

    }

    public void addListenerToBtn(Button _btn, UnityEngine.Events.UnityAction _action)
    {
        _btn.onClick.RemoveAllListeners();
        _btn.onClick.AddListener(_action);
    }

    public void VisibleOnly(string name)
    {
        int count = transform.childCount;
        for (int iNum = 0; iNum < count; iNum++)
        {
            Transform child = transform.GetChild(iNum);
            if (child.name == name)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void setCurrentRoomNameText(string _text)
    {
        currentRoomNameText.text = _text;
    }


    public void visibleError(string errorReason)
    {
        VisibleOnly("Error");
        errorText.text = "Room Creation Failed: " + errorReason;
    }
    private void autoBackBtnAction()
    {
        int count = transform.childCount;
        for (int iNum = 0; iNum < count; iNum++)
        {
            Transform child = transform.GetChild(iNum);
            Transform findBackBtn = child.Find("BackBtn");
            if (findBackBtn)
            {
                Button _backBtn = findBackBtn.GetComponent<Button>();
                addListenerToBtn(_backBtn, () => {
                    VisibleOnly("Main");
                });
            }
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }


}
