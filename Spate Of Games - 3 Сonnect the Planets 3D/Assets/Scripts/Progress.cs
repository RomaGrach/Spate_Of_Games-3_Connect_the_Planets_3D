using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int score = 0;
    
}



public class Progress : MonoBehaviour
{
    public static Progress Instance;
    public PlayerInfo PlayerInfo;
    bool Yandex = false;
    private void Awake()
    {
        /*
        Yandex = false;
#if UNITY_WEBGL
        Yandex = true;
        Debug.Log("Unity WEBGL");
#endif
#if UNITY_EDITOR
        Yandex = false;
        Debug.Log("Unity Editor");
#endif
        */

        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            //StartCoroutine(WaitTime());
        }
        else
        {
            Destroy(gameObject);
        }
        //Instance.PlayerInfo = YandexGame.savesData.PlayerInfo;
    }

    public void SaveProgres()
    {

        Debug.Log("SaveProgres" + Yandex);
        if (Yandex)
        {
            
        }
        else
        {
            SaveProgresPlayerPrefs();
        }
        Debug.Log(PlayerInfo);

    }

    public void DownloadProgress()
    {

        Debug.Log("DownloadProgress" + Yandex);
        if (Yandex)
        {
            
        }
        else
        {
            DownloadProgressPlayerPrefs();
        }
        Debug.Log(PlayerInfo);

    }

    void DownloadProgressPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("ProgresSave"))
        {
            string jsonString = PlayerPrefs.GetString("ProgresSave");
            PlayerInfo = JsonUtility.FromJson<PlayerInfo>(jsonString);
        }
    }

    void SaveProgresPlayerPrefs()
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
        PlayerPrefs.SetString("ProgresSave", jsonString);
    }

    // Start is called before the first frame update
    void Start()
    {
        DownloadProgress();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerInfo.score += 1;
            SaveProgres();
        }
    }

}
