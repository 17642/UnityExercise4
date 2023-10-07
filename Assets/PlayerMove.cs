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
        if (Input.GetButtonDown("Jump")&&scanObj!=null)//스페이스바를 누르면&&scanObj에 무언가가 있으면
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
        Debug.DrawRay(rigid.position,direVec*0.7f,new Color(1,0,0));//캐릭터가 보는 방향으로 Ray가 그려짐.
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, direVec, 0.7f,LayerMask.GetMask("Object"));
        //DrawRay와 비슷하지만 방향과 크기를 따로 지정함. LayerMask를 이용해 감지할 레이어를 결정할 수 있음.

        if (rayHit.collider != null)//무언가(null이 아닌 것)이 닿았을 때
        {
            scanObj=rayHit.collider.gameObject;//ScanObj는 닿은 물체
        }else
            scanObj = null;//닿은게 없으면 scanObj는 NULL

    }
}
