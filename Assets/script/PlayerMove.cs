using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMove : MonoBehaviour
{
    float h;
    float v;
    int Speed;
    bool isHorizontal;
    public GameManager gameManager;
    Animator ani;
    Rigidbody2D rigid;
    Vector3 dirVec;  //현재 어디를 바라보고 있는지를 확인할 변수
    GameObject scanOject;
    
//모바일에서 사용하는 변수
//버튼을 입력 받을 변수 12개 생성함
    int up_Value;
    int down_Value;
    int left_Value;
    int right_Value;

    bool up_Down;
    bool down_Down;
    bool left_Down;
    bool right_Down;

    bool up_Up;
    bool down_Up;
    bool left_Up;
    bool right_Up;        
//버튼을 입력 받을 변수 12개 생성함  끝
    void Awake()
    {   
        Speed = 3; 
        rigid = GetComponent<Rigidbody2D>();
        ani =  GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   //gameManager.isAction ? 0 : => npc 대화 중 Player 이동 금지 logic  (pc상,moblie에서 캐릭터이동 관련)
        h = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal") + right_Value + left_Value;  //수평이동
        v = gameManager.isAction ? 0 : Input.GetAxisRaw("Vertical") + up_Value + down_Value;    //수직이동

        //gameManager.isAction ? false : => npc 대화 중 Player 이동 금지 logic
        bool hDown =gameManager.isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown =gameManager.isAction ? false : Input.GetButtonDown("Vertical"); 
        bool hUP =gameManager.isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUP =gameManager.isAction ? false : Input.GetButtonUp("Vertical");

        if(hDown || vUP)
            isHorizontal = true;
        else if(vDown || hUP)
            isHorizontal = false;
        

        //상하 좌우 Animation 삽입
        if(ani.GetInteger("hAxisRaw") !=h){
        ani.SetInteger("hAxisRaw",(int)h);
        }
        else if(ani.GetInteger("vAxisRaw") !=v){
        ani.SetInteger("vAxisRaw",(int)v); 
        }
        //상하 좌우 Animation 삽입 끝 오류가 있는 듯함
        

        if(vDown && v ==1) // true = 1  (위쪽버튼을 눌렀을 때) 
            dirVec = Vector3.up;
        else if(vDown && v == -1) //true = 1 (아래쪽 버튼을 눌렀을 때)
            dirVec = Vector3.down;
        else if(hDown && h == 1) //true = 1 오른쪽으로 이동
            dirVec = Vector3.right;
        else if(hDown && h == -1)
            dirVec = Vector3.left;

        //Scan Oject
        if(Input.GetButtonDown("Jump") && scanOject != null){
            //gameManager함수의 텍스트 상자여는 함수 실행
            gameManager.Action(scanOject);  
            }

    }
    void FixedUpdate() {
        Vector2 moveVec = isHorizontal ? new Vector2(h,0) : new Vector2(0,v);   //십자이동 true -> ?뒤 실행 , false -> :뒤 실행
        rigid.velocity = moveVec * Speed;

    //Ray그리기(캐릭터 방향에 따라)
    Debug.DrawRay(rigid.position, dirVec*0.7f, new Color(0,1,0));
    RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f,LayerMask.GetMask("Object"));// ray시작위치, ray방향,ray길이, 스캔할 layer
    
    if(rayHit.collider != null){
        scanOject = rayHit.collider.gameObject;}
    else
        scanOject = null;
    }
    public void ButtonDown(string type){
        switch (type){
            case "U":
                up_Value = 1;
                up_Down = true;
                break;
            case "D": 
                down_Value = -1;
                down_Down = true;
                break;
            case "L":
                left_Value = -1;
                left_Down = true;
                break;
            case "R":
                right_Value = 1;
                right_Down = true;
                break;
        }
    }

    public void ButtonUp(string type){
        switch (type){
            case "U":
                up_Value = 0;
                up_Up=true;
                break;
            case "D": 
                down_Value = 0;
                down_Up = true;
                break;
            case "L":
                left_Value = 0;
                left_Up = true;
                break;
            case "R":
                right_Value = 0;
                right_Up = true;
                break;
        }

    }

}
