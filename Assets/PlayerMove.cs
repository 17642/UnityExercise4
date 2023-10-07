using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float h, v;
    Rigidbody2D rigid;
    Animator anim;

    Vector3 direVec;

    float Speed = 3.0f;  

    bool isHorizonMove;

    GameObject scanObj;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        scanObj = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move Value
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //Check Button Down/Up
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");



        //check horizontal move
        if (hDown||vUp)
            isHorizonMove = true;
        else if (vDown||hUp)
            isHorizonMove = false;

        //animation
        if (anim.GetInteger("hAxis") != (int)h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxis", (int)h);
        }else if (anim.GetInteger("vAxis") != (int)v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxis", (int)v);
        }
        else
        {
            anim.SetBool("isChange", false);
        }

        //Direction
        if (vDown && v == 1)
        {
            direVec = Vector3.up;
        }else if (vDown && v == -1)
        {
            direVec= Vector3.down;
        }else if (hDown && h == 1)
        {
            direVec = Vector3.right;
        }else if(hDown && h == -1)
        {
            direVec = Vector3.left;
        }

        //scnaobject
        if (Input.GetButtonDown("Jump")&&scanObj!=null)//�����̽��ٸ� ������&&scanObj�� ���𰡰� ������
        {
            Debug.Log("this is "+scanObj.name);
        }
            
    }

    private void FixedUpdate()
    {

        //Move
            Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
            rigid.velocity = moveVec * Speed;


        //Ray
        Debug.DrawRay(rigid.position,direVec*0.7f,new Color(1,0,0));//ĳ���Ͱ� ���� �������� Ray�� �׷���.
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, direVec, 0.7f,LayerMask.GetMask("Object"));
        //DrawRay�� ��������� ����� ũ�⸦ ���� ������. LayerMask�� �̿��� ������ ���̾ ������ �� ����.

        if (rayHit.collider != null)//����(null�� �ƴ� ��)�� ����� ��
        {
            scanObj=rayHit.collider.gameObject;//ScanObj�� ���� ��ü
        }else
            scanObj = null;//������ ������ scanObj�� NULL

    }
}
