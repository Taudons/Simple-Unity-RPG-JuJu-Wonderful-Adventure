using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    //��Ӧ��Ϸ��ʼ�°�ť��
    public void GameStart()
    {
        SceneManager.LoadScene(1);  //��ȡ�ؿ�level1
    }

    public void EndingGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}