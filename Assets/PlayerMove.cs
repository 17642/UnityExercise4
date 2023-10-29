using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float h, v;
    Rigidbody2D rigid;
    Animator anim;
    public GameManager manager;
    

    Vector3 direVec;

    float Speed = 3.0f;  

    bool isHorizonMove;

    GameObject scanObj;

    //Mobile KEy Var
    int up_value,down_value,left_value,right_value;
    bool up_Down,down_Down,left_Down,right_Down;
    bool up_Up,down_Up,left_Up,right_Up;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //scanObj = GetComponent<GameObject>();
        //manager= gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move Value
        //PC+Mobile
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal")+right_value + left_value;
        v = manager.isAction ? 0 : Input.GetAxisRaw("Vertical")+ down_value + up_value;


        //Check Button Down/Up -> isAction이 true일 경우(대화창이 켜져 있을 경우 움직임을 제한)
        bool hDown = manager.isAction ? false : Input.GetButtonDown("Horizontal")|| right_Down || left_Down;
        bool vDown = manager.isAction ? false : Input.GetButtonDown("Vertical")|| up_Down || down_Down;
        bool hUp = manager.isAction ? false : Input.GetButtonUp("Horizontal") || right_Up || left_Up;
        bool vUp = manager.isAction ? false : Input.GetButtonUp("Vertical") || up_Up || down_Up;



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
            //Debug.Log("this is "+scanObj.name);
            manager.Action(scanObj);//텍스트 창에 표시
        }

        //mobile var init
        left_Down = false;
        right_Down = false;
        up_Down = false;
        down_Down = false;
        left_Up=false; 
        right_Up=false;
        up_Up=false;
        down_Up=false;
            
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

    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "U":
                up_value = 1;
                up_Down = true;
                break;
            case "D":
                down_value = 1;
                down_Down = true;
                break;
            case "L":
                left_value = 1;
                left_Down = true;
                break;
            case "R":
                right_value = 1;
                right_Down = true;
                break;
            case "A":
                if (scanObj != null)//scanObj에 무언가가 있으면
                {
                    //Debug.Log("this is "+scanObj.name);
                    manager.Action(scanObj);//텍스트 창에 표시
                }
                break;
            case "C":
                manager.SubMenuActive();
                break;

        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "U":
                up_value = 0;
                up_Up = true;
                break;
            case "D":
                down_value = 0;
                down_Down = true;
                break;
            case "L":
                left_value = 0;
                left_Down = true;
                break;
            case "R":
                right_value = 0;
                right_Down = true;
                break;

        }
    }
}
