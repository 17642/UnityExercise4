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
        if (isAction == true)//talkPanel�� �̹� ���� ������
        {
            isAction = false;//talkPanel ����ġ�� ��
        }
        else//talkPanel�� ���� ������ ����
        {
            isAction = true;//isAction Ű����� talkPanel�� Ȱ��ȭ�Ǿ��ٰ� ǥ����
            scanObject = scanObj;
            talkMassage.text = "�̰��� �̸��� " + scanObject.name + " �Դϴ�.";
        }

        talkPanel.SetActive(isAction);
    }
}
