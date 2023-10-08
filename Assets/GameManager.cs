using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkMassage;
    public GameObject scanObject;
    public bool isAction=false;
    // Start is called before the first frame update

    public void Action(GameObject scanObj)
    {
        if (isAction == true)//talkPanel이 이미 켜져 있으면
        {
            isAction = false;//talkPanel 스위치를 끔
        }
        else//talkPanel이 꺼져 있으면 실행
        {
            isAction = true;//isAction 키워드로 talkPanel이 활성화되었다고 표시함
            scanObject = scanObj;
            talkMassage.text = "이것의 이름은 " + scanObject.name + " 입니다.";
        }

        talkPanel.SetActive(isAction);
    }
}
