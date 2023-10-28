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
        talkData.Add(200, new string[] { "이 책상은 누군가 사용한 모양이다." });//퀘스트 번호 + NPC ID


        //퀘스트 대화록
        talkData.Add(10 + 1000, new string[] {"어?/0","이 곳에 처음 왔구나?/1","저 옆으로 가서 호수에 관한 이야기를 들어봐./0"});
        talkData.Add(10 + 1 + 2000, new string[] { "에?/3", "이 호수에 얽힌 이야기를 들으러 온 거야?/1", "그냥은 좀 어렵고, 내 집 근처에 있는 동전 좀 주워와 줘./0" });//퀘스트 번호 + 대화 순서 + NPC ID
        talkData.Add(20 + 1000, new string[] { "루도의 동전?/1", "돈을 흘리고 다니면 못쓰지!/3", "나중에 루도한테 한마디 해야겠어./3" });
        //talkData.Add(20 + 1000, new string[] { "찾으면 꼭좀 가져다줘./0" });
        talkData.Add(20 + 5000, new string[] { "근처에서 동전을 찾았다." });
        talkData.Add(20 + 1 + 2000, new string[] { "아, 찾아줘서 고마워./0" });

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
        if (!talkData.ContainsKey(id))//index의 딕셔너리 안에 데이터가 없는 경우
        {
            if (talkData.ContainsKey(id-id%10))
            {
                return GetTalk(id-id%10,talkindex); //GET FIRST QUEST TALK
                //if (talkindex == talkData[id - id % 10].Length)
                //    return null;
                //else
                //    return talkData[id - id % 10][talkindex];//10으로 나눈 나머지(대화 순서)를 뺀 값을 리턴<퀘스트 맨 처음 대사>
            }
            else
            {
                return GetTalk(id - id % 100, talkindex);//GET FIRST TALK//함수에 리턴값이 있으므로 리턴을 써줘야 작동함.
                //if (talkindex == talkData[id - id % 100].Length)//만약 퀘스트 대화도 없을 경우 기본 대사를 가져온다.
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
