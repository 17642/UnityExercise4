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
        msgText.text = "";//���� ���� ó��
        index = 0;
        EndCursors.SetActive(false);
        

        Invoke("Effecting",1/CPS);//�ð��� �ݺ� ȣ��
        isAnim = true;
    }

    void Effecting()
    {
        if (msgText.text == targetMsg)//���ڰ� �� ä��������
        {
            EffectEnd();//ȿ�� ��
            return;
        }
        msgText.text += targetMsg[index];//�� ���ھ� �߰� ��

        if (targetMsg[index] != ' ' || targetMsg[index] != '.')
            audioSource.Play();

        index++;//�ε��� ����
        

        float interval = 1.0f / CPS;
        Debug.Log(interval);
        Invoke("Effecting", interval);//�ð��� �ݺ� ȣ��
    }

    void EffectEnd()
    {
        isAnim = false;
        EndCursors.SetActive(true);
        
    }
}
