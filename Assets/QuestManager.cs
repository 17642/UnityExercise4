using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public int questId;
    public int questTalkIndex;
    Dictionary<int, QuestData> questList;
    public GameObject[] questObject;//퀘스트에 쓸 오브젝트 배열

    // Start is called before the first frame update
    void Awake()
    {
        questList=new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("마을 사람들과 이야기", new int[] {1000,2000 }));
        questList.Add(20, new QuestData("루도의 동전 찾아주기", new int[] {5000,2000 }));//동전 접근 후 NPC 1 접근
        questList.Add(30, new QuestData("퀘스트 모두 해결", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)//NPC의 ID를 받아 Quest Number를 반환
    {
        return questId+questTalkIndex;//퀘스트 ID + 대화 인덱스
    }

    public string CheckQuest(int id)
    {
       

        if (id == questList[questId].npcID[questTalkIndex])//순서에 맞게 대화했는지 확인
        {
            questTalkIndex++;
        }

        ControlObject();   

        if (questTalkIndex == questList[questId].npcID.Length)//마지막 대화까지 넘어가면
        {
            NextQuest();//다음 퀘스트로 넘어간다.
        }

        return questList[questId].questName;//퀘스트 이름 반환
    }

    public string CheckQuest()//함수 오버로딩
    {
        return questList[questId].questName;
    }

    void NextQuest()//다음 퀘스트
    {
        questId += 10;//퀘스트 ID 증가
        questTalkIndex = 0;//대화 순서 초기화
    }

    void ControlObject()//퀘스트에 따라 물체 조작
    {
        switch (questId)
        {
            case 10://첫 번째 퀘스트에서
                if (questTalkIndex == 2)//2번째 대화를 할 떄
                {
                    questObject[0].SetActive(true);//퀘스트 물건 0번(동전)을 활성화한다.
                }
                break;
            case 20://두 번째 퀘스트에서
                if (questTalkIndex == 1)//첫번째 대화(동전에 접근해 대화)했을떄
                {
                    questObject[0].SetActive(false);//퀘스트 물건 0번을 끈다(동전을 주웠으므로)
                }
                break;
        }
    }
    // Update is called once per frame
}
