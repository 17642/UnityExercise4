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
        talkData.Add(200, new string[] { "�� å���� ������ ����� ����̴�." });

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
        if (talkindex == talkData[id].Length)
            return null;
        return talkData[id][talkindex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id+portraitIndex];
    }
}
