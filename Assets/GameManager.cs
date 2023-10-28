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
        //if (isAction == true)//talkPanel�� �̹� ���� ������
        //{
        //    isAction = false;//talkPanel ����ġ�� ��
        //}
        //else//talkPanel�� ���� ������ ����
        //{
        //    isAction = true;//isAction Ű����� talkPanel�� Ȱ��ȭ�Ǿ��ٰ� ǥ����
            scanObject = scanObj;
            ObjData objdata= scanObj.GetComponent<ObjData>();

            //talkMassage.text = "�̰��� �̸��� " + scanObject.name + " �Դϴ�.";
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
        //Talk Data ����
        int questTalkIndex = Qmanager.GetQuestTalkIndex(id);//NPC ID�� �޾� �ش��ϴ� ����Ʈ ��ȣ ����
        string talk = Tmanager.GetTalk(id+questTalkIndex, talkIndex);//��ȭ ������ ID�� ����Ʈ ��ȣ+NPC ID�� ����
        //��ȭ ������
        if (talk == null)//���̻� ��ȭ�� ������ ������
        {
            talkIndex = 0;//talkIndex�� �ʱ�ȭ�ϰ�
            isAction = false;//â�� ����
            Qmanager.CheckQuest(id);
            //Debug.Log(Qmanager.CheckQuest(id));//����Ʈ �̸� ���
            return;//�Լ� ���� ��
        }
        if (isNpc)//NPC�� ��
        {
            talkMassage.text=talk.Split('/')[0];// /�� �����ڷ� ����� ���ڿ��� �и�(�迭�� ��ȯ��)
            portraitImage.sprite = Tmanager.GetPortrait(id, int.Parse(talk.Split('/')[1]));// �и��� ���ڿ��� int�� �Ľ�.
            portraitImage.color = new Color(1, 1, 1, 1);//���İ��� 1�� �� �ʻ�ȭ�� �����ش�
        }
        else//NPC�� �ƴ� ��
        {
            talkMassage.text = talk;
            portraitImage.color=new Color(1, 1, 1, 0);//���İ��� 0���� �� �ʻ�ȭ�� �����ϰ� �Ѵ�.
        }
        isAction = true;//â�� �Ѱ�
        talkIndex++;//talkIndex�� ����
    }
}
