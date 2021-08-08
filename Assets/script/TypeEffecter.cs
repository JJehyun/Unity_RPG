using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//타이핑 효과주는 script 
public class TypeEffecter : MonoBehaviour
{   
    string targetMsg;//text들어갈 text
    public int CharperSeconds; //text 띄워질 속도 값
    Text msgText;
    public GameObject endCursur;
    int index;
    float interval;
    public bool isAnim;
    private void Awake(){
        msgText = GetComponent<Text>();// 초기화
    }

    //들어온 메세지 설정 함수
    public void SetMsg(string msg)
    {
        if (isAnim)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else {
            targetMsg = msg;
            EffectStart();
        }
    }


//시작
    void EffectStart(){
        msgText.text = "";
        index = 0;
        endCursur.SetActive(false);//endcursur 안보이게
        interval = 1.0f / CharperSeconds;  //속도 설정
        Invoke("Effecting",interval); //1글자가 나오는데 걸리는 딜레이 설정하는 함수(public)
        isAnim = true;


    }
//애니메이션 실행
    void Effecting(){
        //재귀함수 탈출하기 (문자열을 하나씩 모두 애니메이션을 이용해 호출 끝 -> 함수 탈출의 내용의 함수)
        if(msgText.text == targetMsg){
            EffectEnd();
            return ;
        }

        msgText.text = msgText.text + targetMsg[index]; //문자열은 배열을 구지 선언안해도 배열의index를 가질수 있다.
        index++;
        Invoke("Effecting",interval);   //재귀 함수
    }
//마무리! EndCursur 보이도록 설정하기
    void EffectEnd(){
        isAnim = false;
        endCursur.SetActive(true);
    }
}
