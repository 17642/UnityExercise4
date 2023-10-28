using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;//int�� Ű�� string �迭�� ������ ����
    Dictionary<int, Sprite> portraitData;//�ʻ�ȭ ������
    public Sprite[] portraitArr;//�ʻ�ȭ �迭. ��������Ʈ�� ������ �������� ������ ������ �� ����.
    // Start is called before the first frame update
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "�ȳ�?/0", "�̰��� ó�� �Ա���?/1" });//���� �ѹ��� �� ���� �̻� �� �� ����
        talkData.Add(2000, new string[] { "���� �����ۿ� �Ⱥ���.../3" });// /�� �����ڷ� ����� �ʻ�ȭ �������� �ε����� ���� .
        talkData.Add(100, new string[] { "����� ���� ���ڴ�." });
        talkData.Add(200, new string[] { "�� å���� ������ ����� ����̴�." });//����Ʈ ��ȣ + NPC ID


        //����Ʈ ��ȭ��
        talkData.Add(10 + 1000, new string[] {"��?/0","�� ���� ó�� �Ա���?/1","�� ������ ���� ȣ���� ���� �̾߱⸦ ����./0"});
        talkData.Add(10 + 1 + 2000, new string[] { "��?/3", "�� ȣ���� ���� �̾߱⸦ ������ �� �ž�?/1", "�׳��� �� ��ư�, �� �� ��ó�� �ִ� ���� �� �ֿ��� ��./0" });//����Ʈ ��ȣ + ��ȭ ���� + NPC ID
        talkData.Add(20 + 1000, new string[] { "�絵�� ����?/1", "���� �긮�� �ٴϸ� ������!/3", "���߿� �絵���� �Ѹ��� �ؾ߰ھ�./3" });
        //talkData.Add(20 + 1000, new string[] { "ã���� ���� ��������./0" });
        talkData.Add(20 + 5000, new string[] { "��ó���� ������ ã�Ҵ�." });
        talkData.Add(20 + 1 + 2000, new string[] { "��, ã���༭ ����./0" });

        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkindex)//������ id�� string �迭�� index�� ������.
    {
        if (!talkData.ContainsKey(id))//index�� ��ųʸ� �ȿ� �����Ͱ� ���� ���
        {
            if (talkData.ContainsKey(id-id%10))
            {
                return GetTalk(id-id%10,talkindex); //GET FIRST QUEST TALK
                //if (talkindex == talkData[id - id % 10].Length)
                //    return null;
                //else
                //    return talkData[id - id % 10][talkindex];//10���� ���� ������(��ȭ ����)�� �� ���� ����<����Ʈ �� ó�� ���>
            }
            else
            {
                return GetTalk(id - id % 100, talkindex);//GET FIRST TALK//�Լ��� ���ϰ��� �����Ƿ� ������ ����� �۵���.
                //if (talkindex == talkData[id - id % 100].Length)//���� ����Ʈ ��ȭ�� ���� ��� �⺻ ��縦 �����´�.
                //    return null;
                //else
                //    return talkData[id - id % 100][talkindex];
            }
        }
        if (talkindex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkindex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id+portraitIndex];
    }
}
