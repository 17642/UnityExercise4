using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    //public GameObject talkPanel;
    public TypingEffector talkMassage;
    public GameObject scanObject;
    public bool isAction=false;
    public TalkManager Tmanager;
    public int talkIndex=0;
    public Image portraitImage;
    public Animator portraitAnim;
    public QuestManager Qmanager;
    public Animator talkPanel;
    public Sprite prevPortrait;
    public GameObject MenuSet;
    public Text questTalk;
    public GameObject Player;
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

        talkPanel.SetBool("isShow",isAction);//isShow 설정.
    }
    private void Start()
    {
        GameLoad();
        questTalk.text=Qmanager.CheckQuest();
    }

    private void Update()
    {
        SubMenuActive();
    }

    public void SubMenuActive()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (MenuSet.activeSelf)//메뉴창이 켜져 있을 떄
                MenuSet.SetActive(false);
            else
                MenuSet.SetActive(true);
        }
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;
        string talk = "";
        //Talk Data 설정
        if (talkMassage.isAnim)
        {
            talkMassage.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = Qmanager.GetQuestTalkIndex(id);//NPC ID를 받아 해당하는 퀘스트 번호 지정
            talk = Tmanager.GetTalk(id + questTalkIndex, talkIndex);//대화 데이터 ID를 퀘스트 번호+NPC ID로 지정
        }
        
        //대화 끝내기
        if (talk == null)//더이상 대화의 내용이 없으면
        {
            talkIndex = 0;//talkIndex를 초기화하고
            isAction = false;//창을 끄고
            Qmanager.CheckQuest(id);
            questTalk.text=Qmanager.CheckQuest(id);//퀘스트 이름 출력
            return;//함수 진행 끝
        }
        if (isNpc)//NPC일 때
        {
            talkMassage.SetMsg(talk.Split('/')[0]);// /를 구분자로 사용해 문자열을 분리(배열을 반환함)
            portraitImage.sprite = Tmanager.GetPortrait(id, int.Parse(talk.Split('/')[1]));// 분리한 문자열을 int로 파싱.
            portraitImage.color = new Color(1, 1, 1, 1);//알파값을 1로 해 초상화를 보여준다
            //Animation Portrait
            if (prevPortrait != portraitImage.sprite)
            {//이전 스프라이트와 지금 스프라이트가 다르면
                portraitAnim.SetTrigger("doEffect");//doEffect 트리거 작동
                prevPortrait = portraitImage.sprite;
            }
        }
        else//NPC가 아닐 떄
        {
            talkMassage.SetMsg(talk);
            portraitImage.color=new Color(1, 1, 1, 0);//알파값을 0으로 해 초상화를 투명하게 한다.
        }
        isAction = true;//창을 켜고
        talkIndex++;//talkIndex를 증가
    }

    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX",Player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", Player.transform.position.y);
        PlayerPrefs.SetInt("QuestID", Qmanager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", Qmanager.questTalkIndex);
        //player.x;
        //player.y
        //quest id
        //Quest Action Index
        PlayerPrefs.Save();
        MenuSet.SetActive(false);
    }

    public void GameLoad() {
        if (!PlayerPrefs.HasKey("PlayerX"))//최소 시작시 저장 데이터가 없으므로
            return; //예외 설정

        float x=PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questid = PlayerPrefs.GetInt("QuestID");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");

        Player.transform.position=new Vector3(x,y,0);
        Qmanager.questId=questid;
        Qmanager.questTalkIndex=questActionIndex;
    }
    public void GameExit()
    {
        Application.Quit();//게임 종료(에디터에서는 작동 X)
    }
}
