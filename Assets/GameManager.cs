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
    public TalkManager Tmanager;
    public int talkIndex=0;
    public Image portraitImage;
    public QuestManager Qmanager;
    // Start is called before the first frame update

    public void Action(GameObject scanObj)
    {
        //if (isAction == true)//talkPanel이 이미 켜져 있으면
        //{
        //    isAction = false;//talkPanel 스위치를 끔
        //}
        //else//talkPanel이 꺼져 있으면 실행
        //{
        //    isAction = true;//isAction 키워드로 talkPanel이 활성화되었다고 표시함
            scanObject = scanObj;
            ObjData objdata= scanObj.GetComponent<ObjData>();

            //talkMassage.text = "이것의 이름은 " + scanObject.name + " 입니다.";
            Talk(objdata.id, objdata.isNpc);
        //}

        talkPanel.SetActive(isAction);
    }
    private void Start()
    {
        Debug.Log(Qmanager.CheckQuest());
    }

    void Talk(int id, bool isNpc)
    {
        //Talk Data 설정
        int questTalkIndex = Qmanager.GetQuestTalkIndex(id);//NPC ID를 받아 해당하는 퀘스트 번호 지정
        string talk = Tmanager.GetTalk(id+questTalkIndex, talkIndex);//대화 데이터 ID를 퀘스트 번호+NPC ID로 지정
        //대화 끝내기
        if (talk == null)//더이상 대화의 내용이 없으면
        {
            talkIndex = 0;//talkIndex를 초기화하고
            isAction = false;//창을 끄고
            Qmanager.CheckQuest(id);
            //Debug.Log(Qmanager.CheckQuest(id));//퀘스트 이름 출력
            return;//함수 진행 끝
        }
        if (isNpc)//NPC일 때
        {
            talkMassage.text=talk.Split('/')[0];// /를 구분자로 사용해 문자열을 분리(배열을 반환함)
            portraitImage.sprite = Tmanager.GetPortrait(id, int.Parse(talk.Split('/')[1]));// 분리한 문자열을 int로 파싱.
            portraitImage.color = new Color(1, 1, 1, 1);//알파값을 1로 해 초상화를 보여준다
        }
        else//NPC가 아닐 떄
        {
            talkMassage.text = talk;
            portraitImage.color=new Color(1, 1, 1, 0);//알파값을 0으로 해 초상화를 투명하게 한다.
        }
        isAction = true;//창을 켜고
        talkIndex++;//talkIndex를 증가
    }
}
