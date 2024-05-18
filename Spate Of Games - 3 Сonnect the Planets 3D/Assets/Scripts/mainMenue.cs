using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenue : MonoBehaviour
{
    public TextMeshProUGUI Coins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Coins.text = Progress.Instance.PlayerInfo.score.ToString();
    }

    public void playGame()
    {
        SceneManager.LoadScene(1);
    }
}
