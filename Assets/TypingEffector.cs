using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffector : MonoBehaviour
{
    // Start is called before the first frame update

    public int CPS;//CharPerSeconds
    string targetMsg;
    Text msgText;
    AudioSource audioSource;
    int index;
    public GameObject EndCursors;
    public bool isAnim;

    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }
    public void SetMsg(string msg)
    {
        if (isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        msgText.text = "";//시작 공백 처리
        index = 0;
        EndCursors.SetActive(false);
        

        Invoke("Effecting",1/CPS);//시간차 반복 호출
        isAnim = true;
    }

    void Effecting()
    {
        if (msgText.text == targetMsg)//글자가 다 채워졌으면
        {
            EffectEnd();//효과 끝
            return;
        }
        msgText.text += targetMsg[index];//한 글자씩 추가 후

        if (targetMsg[index] != ' ' || targetMsg[index] != '.')
            audioSource.Play();

        index++;//인덱스 증가
        

        float interval = 1.0f / CPS;
        Debug.Log(interval);
        Invoke("Effecting", interval);//시간차 반복 호출
    }

    void EffectEnd()
    {
        isAnim = false;
        EndCursors.SetActive(true);
        
    }
}
