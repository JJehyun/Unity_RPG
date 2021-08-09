using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   public QuestManager questManager;
    public TalkMenager talkMenager;
    public Animator talkBox;
    public TypeEffecter talk;
    public GameObject scanOject;
    public GameObject menuset;      //일시정지 후 메인 메뉴창
    public Animator portraitAnim;
    public Sprite prePortraint;     //과거의 초상화 이미지 저장 
    public Image portraitImg;       //초상화 이미지 컨트롤
    public bool isAction;
    public int talkIndex;
    public Text questText;
    public GameObject player;


    public void Start()
    {   GameRoad();
        questText.text = questManager.CheckQuest();
    }



    //대화창을 띄우는 함수 enter 눌렀을 때 실행되는 함수
    public void Action(GameObject scanObj){
        //Ray로 스캔한 오브젝트를 넣어줌
        scanOject = scanObj;
        //사용자가 직접만든 함수 초기화 작업
        ObjData objData = scanOject.GetComponent<ObjData>();
        //사용자가 만든 아래 함수(각 Npc맞는 데이터를 가져옴)
        Talk(objData.id,objData.isNpc);
        //대화창 활성화, 비활성화
        talkBox.SetBool("isShow",isAction);
        }


    void Update()
    {
        if (Input.GetButton("Cancel")) {
            if (menuset.activeSelf)//esc눌렀을 때 메뉴창이 켜져있다면 비활성화하겠다.
            {
                menuset.SetActive(false);
            }
            else
            menuset.SetActive(true);
        }    
    }


    //(각 Npc맞는 데이터를 가져옴)
    void Talk(int id, bool isNpc){
        int questTalkIndex;
        string talkData = "";
        if (talk.isAnim)
        {
            talk.SetMsg("");
            return;
        }
        else {
            //퀘스트 관련, 지금 말하고 있는 npc id값을 넘겨줌
            questTalkIndex = questManager.GetQuestTalkIndex(id);

            //GetTalk사용자가 만든 함수, id에 맞는 데이터를 return 한다.
            talkData = talkMenager.GetTalk(id + questTalkIndex, talkIndex);
        }

        //이야기가 끝났을 때 이야기box를 비활성화 해준다.
        if(talkData == null){
            isAction = false;
            //npc와 이야기가 끝났을 때 talkIndex를 초기화해줌
            talkIndex = 0;
            questText.text = questManager.CheckQuest(id);
            return;
        }



        if(isNpc){
                //대화창의 데이터를 넣어줌 Split 구분자를 통해서 배열로 나눠주는 지원함수
                talk.SetMsg(talkData.Split(':')[0]);

                //초상화이미지 넣기 대화창에 마지막작업!
                portraitImg.sprite = talkMenager.GetPortrait(id,int.Parse(talkData.Split(':')[1]));
                //Npc일 때 초상화 투입
                portraitImg.color = new Color(1,1,1,1);
                //과거 이미지와 현재이미지가 같지 않다면 직접만든 애니메이션 실행!
                //insprctor에서 ui넣어주고 과거와 현재가 다르다면 애니메이션 실행
                if(prePortraint != portraitImg.sprite){
                    portraitAnim.SetTrigger("doEffect");
                    prePortraint = portraitImg.sprite;
                }
        }else{
                //대화창에 데이터를 넣어줌
                talk.SetMsg(talkData);
                //Npc아닐때는 투명도 0으로 초상화 안보이게 설정함
                portraitImg.color = new Color(1,1,1,0);
        }
        isAction = true;//대화상자게 계속 보이게 true
        talkIndex++;
    }
    //게임 저장하기(퀘스트 정보, 캐릭터의 위치를 저장해야함) PlayerPrefs = 간단한 데이터 저장 기능 클래스
    //player.x , player.y ,Quest Id, Quest Action Index
    public void GameSave(){


        //player 위치 저장
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        //진행중인 퀘스트 대표 타이틀
        PlayerPrefs.SetInt("QustId", questManager.questId);
        //진행중인 퀘스트 1-2 저장
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        //사용자 레지스트리에 위 값이 저장됨 
        PlayerPrefs.Save();

        menuset.SetActive(false);
    }
    //저장한 게임 불러오기(레지스트리에 저장된 위치 정보,퀘스트 정보를 불러옴)
    public void GameRoad(){        //사용자가 한번도 save를 하지 않았다면 (hasKey) road를 하지 않겠다.
        if(!PlayerPrefs.HasKey("PlayerX"))
            return;

        
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("QustId");
        int questActionIndex = PlayerPrefs.GetInt("QuestActionIndex");


        player.transform.position = new Vector3(x,y,0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIndex;
        questManager.ControlObject();
    }




    //게임 종료하기! Application 함수 활용
    public void GameExit() {
        Application.Quit();//C#에서 지원하는 함수
    }




    }








