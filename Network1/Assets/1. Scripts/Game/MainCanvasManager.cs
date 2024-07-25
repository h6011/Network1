using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    public static MainCanvasManager Instance;


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

    public Transform GetUIByName(string _name)
    {
        Transform findUI = transform.Find(_name);
        if (findUI)
        {
            return findUI;
        }
        throw new System.Exception();
    }

    public void SetActiveUIByName(string _name, bool _bool)
    {
        Transform findUI = transform.Find(_name);
        if (findUI)
        {
            findUI.gameObject.SetActive(_bool);
        }
    }

    public void SetTestTitle(string _title)
    {
        Transform findUI = transform.Find("Title1");
        if (findUI)
        {
            TMPro.TMP_Text title = findUI.GetComponent<TMPro.TMP_Text>();
            title.text = _title;
        }
    }





}
