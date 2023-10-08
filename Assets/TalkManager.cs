using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;//int를 키로 string 배열을 값으로 가짐
    Dictionary<int, Sprite> portraitData;//초상화 데이터
    public Sprite[] portraitArr;//초상화 배열. 스프라이트는 변수를 지정하지 않으면 가져올 수 없다.
    // Start is called before the first frame update
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(1000, new string[] { "안녕?/0", "이곳에 처음 왔구나?/1" });//대사는 한번에 두 문장 이상 들어갈 수 있음
        talkData.Add(2000, new string[] { "여긴 나무밖에 안보여.../3" });// /을 구분자로 사용해 초상화 데이터의 인덱스를 지정 .
        talkData.Add(100, new string[] { "평범한 나무 상자다." });
        talkData.Add(200, new string[] { "이 책상은 누군가 사용한 모양이다." });

        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
        portraitData.Add(1000 + 2, portraitArr[2]);
        portraitData.Add(1000 + 3, portraitArr[3]);
        portraitData.Add(2000 + 0, portraitArr[4]);
        portraitData.Add(2000 + 1, portraitArr[5]);
        portraitData.Add(2000 + 2, portraitArr[6]);
        portraitData.Add(2000 + 3, portraitArr[7]);
    }

    public string GetTalk(int id, int talkindex)//문장의 id와 string 배열의 index를 가져옴.
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
