using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthEXP_Show : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform playerTransform;
    private PlayerController PlayerController;
    private Transform UIHealth;
    float Maxhealth;
    private Slider UIHealthSlider;
    public Transform UIEXP;
    private Slider UIEXPSlider;
    public TextMeshProUGUI TextLv;
    private float level;
    float KilledCount;
    float UIShowEXP;
    bool flag=true;
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        UIHealth = GameObject.Find("UIHealth").transform;       
        if(UIHealth!=null)
        {
            Debug.Log("UIHealth found");
        }
        PlayerController = playerTransform.GetComponent<PlayerController>();
        Maxhealth = PlayerController.health;
        UIHealthSlider = UIHealth.GetComponent<Slider>();
        if (UIHealthSlider != null)
        {
            Debug.Log("UIHealthSlider found");
        }
        Debug.Log(Maxhealth);
        UIEXPSlider = UIEXP.GetComponent<Slider>();
        //�Եȼ���ʼ��
        level = playerTransform.GetComponentInChildren<AttackCheckFuction>().level;
        TextLv.text = "Lv:" + level;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController = playerTransform.GetComponent<PlayerController>();
        if (PlayerController != null)
        {
            // ��ȡ���µ�Healthֵ
            float health = PlayerController.health;
            
            // ��health���в���
            float UIShowHealth = health / Maxhealth;
            //��health��ʾ��ui��
            UIHealthSlider.value = UIShowHealth;

            //��ȡplayer�Ļ�ɱ��
            KilledCount = PlayerController.EnemyKilledCount%5;
            //Debug.Log(KilledCount);
            UIShowEXP = KilledCount/4;
            //Debug.Log(UIShowEXP);
            
            if(flag==true)
                UIEXPSlider.value = UIShowEXP;
            else
                UIEXPSlider.value = 0;
            if (UIShowEXP >= 1&& flag==true)
            {
                playerTransform.GetComponentInChildren<AttackCheckFuction>().level++;
                level = playerTransform.GetComponentInChildren<AttackCheckFuction>().level;
                TextLv.text = "Lv:" + level;
                PlayerController.health = Maxhealth;
                flag = false;
            }
            if(UIShowEXP < 1)
            {
                flag = true;
            }
        }
       
    }
}
