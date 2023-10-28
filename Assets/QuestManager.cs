using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public int questId;
    public int questTalkIndex;
    Dictionary<int, QuestData> questList;
    public GameObject[] questObject;//����Ʈ�� �� ������Ʈ �迭

    // Start is called before the first frame update
    void Awake()
    {
        questList=new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("���� ������ �̾߱�", new int[] {1000,2000 }));
        questList.Add(20, new QuestData("�絵�� ���� ã���ֱ�", new int[] {5000,2000 }));//���� ���� �� NPC 1 ����
        questList.Add(30, new QuestData("����Ʈ ��� �ذ�", new int[] { 0 }));
    }

    public int GetQuestTalkIndex(int id)//NPC�� ID�� �޾� Quest Number�� ��ȯ
    {
        return questId+questTalkIndex;//����Ʈ ID + ��ȭ �ε���
    }

    public string CheckQuest(int id)
    {
       

        if (id == questList[questId].npcID[questTalkIndex])//������ �°� ��ȭ�ߴ��� Ȯ��
        {
            questTalkIndex++;
        }

        ControlObject();   

        if (questTalkIndex == questList[questId].npcID.Length)//������ ��ȭ���� �Ѿ��
        {
            NextQuest();//���� ����Ʈ�� �Ѿ��.
        }

        return questList[questId].questName;//����Ʈ �̸� ��ȯ
    }

    public string CheckQuest()//�Լ� �����ε�
    {
        return questList[questId].questName;
    }

    void NextQuest()//���� ����Ʈ
    {
        questId += 10;//����Ʈ ID ����
        questTalkIndex = 0;//��ȭ ���� �ʱ�ȭ
    }

    void ControlObject()//����Ʈ�� ���� ��ü ����
    {
        switch (questId)
        {
            case 10://ù ��° ����Ʈ����
                if (questTalkIndex == 2)//2��° ��ȭ�� �� ��
                {
                    questObject[0].SetActive(true);//����Ʈ ���� 0��(����)�� Ȱ��ȭ�Ѵ�.
                }
                break;
            case 20://�� ��° ����Ʈ����
                if (questTalkIndex == 1)//ù��° ��ȭ(������ ������ ��ȭ)������
                {
                    questObject[0].SetActive(false);//����Ʈ ���� 0���� ����(������ �ֿ����Ƿ�)
                }
                break;
        }
    }
    // Update is called once per frame
}
