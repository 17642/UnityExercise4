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

        talkPanel.SetBool("isShow",isAction);//isShow ����.
    }
    private void Start()
    {
        Debug.Log(Qmanager.CheckQuest());
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;
        string talk = "";
        //Talk Data ����
        if (talkMassage.isAnim)
        {
            talkMassage.SetMsg("");
            return;
        }
        else
        {
            questTalkIndex = Qmanager.GetQuestTalkIndex(id);//NPC ID�� �޾� �ش��ϴ� ����Ʈ ��ȣ ����
            talk = Tmanager.GetTalk(id + questTalkIndex, talkIndex);//��ȭ ������ ID�� ����Ʈ ��ȣ+NPC ID�� ����
        }
        
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
            talkMassage.SetMsg(talk.Split('/')[0]);// /�� �����ڷ� ����� ���ڿ��� �и�(�迭�� ��ȯ��)
            portraitImage.sprite = Tmanager.GetPortrait(id, int.Parse(talk.Split('/')[1]));// �и��� ���ڿ��� int�� �Ľ�.
            portraitImage.color = new Color(1, 1, 1, 1);//���İ��� 1�� �� �ʻ�ȭ�� �����ش�
            //Animation Portrait
            if (prevPortrait != portraitImage.sprite)
            {//���� ��������Ʈ�� ���� ��������Ʈ�� �ٸ���
                portraitAnim.SetTrigger("doEffect");//doEffect Ʈ���� �۵�
                prevPortrait = portraitImage.sprite;
            }
        }
        else//NPC�� �ƴ� ��
        {
            talkMassage.SetMsg(talk);
            portraitImage.color=new Color(1, 1, 1, 0);//���İ��� 0���� �� �ʻ�ȭ�� �����ϰ� �Ѵ�.
        }
        isAction = true;//â�� �Ѱ�
        talkIndex++;//talkIndex�� ����
    }
}
