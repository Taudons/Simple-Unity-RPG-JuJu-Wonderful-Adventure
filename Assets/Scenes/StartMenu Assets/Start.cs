using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    //响应游戏开始事按钮件
    public void GameStart()
    {
        SceneManager.LoadScene(1);  //读取关卡level1
    }

    public void EndingGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}