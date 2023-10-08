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

    void Talk(int id, bool isNpc)
    {
        string talk = Tmanager.GetTalk(id, talkIndex);

        if (talk == null)//���̻� ��ȭ�� ������ ������
        {
            talkIndex = 0;//talkIndex�� �ʱ�ȭ�ϰ�
            isAction = false;//â�� ����
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
