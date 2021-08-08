using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//퀘스트 관리해주는 script 파일
public class QuestManager : MonoBehaviour
{   //quest 아이디
    public int questId;
    //quest 순서 int 정하기
    public int questTalkIndex;
    public int questActionIndex;
    public GameObject[] gameobject;         //게시판 가져옴
    Dictionary<int, QuestData> questList;  //변수 타입 선언
    void Awake()
    {
        questList = new Dictionary<int, QuestData>(); //초기화, 인스턴스 생성
        GenerateData();
    }

    //데이터 넣기
    void GenerateData()
    {
        questList.Add(10,new QuestData("첫 마을 방문! 마을사람과 이야기 진행", new int[] {1000, 2000}));
        questList.Add(20,new QuestData("루미의 동전찾기", new int[] {5000, 2000}));
        questList.Add(30,new QuestData("퀘스트 올 클리어", new int[] {0}));
    }

    //Quest index 얻기
    public int GetQuestTalkIndex(int id){
        //questId(퀘스트 이름int값과)와 questActionIndex(퀘스트를 순차적으로 진행할 수있게 순서를 정해주는 변수)
        return questId+questActionIndex;
    }
    //대화가 끝이 났을 때 questActionIndex ++해준다.
    public string CheckQuest(int id){
        if(id == questList[questId].npcId[questActionIndex]){
            questActionIndex++;}

        //컨트롤 퀘스트 오브젝트 아래있음
        ControlObject();
        if(questActionIndex == questList[questId].npcId.Length){
            NextQuest();
        }
        return questList[questId].questName;
        
    }
    
    public string CheckQuest(){
        return questList[questId].questName;
    }



    //퀘스트 완료 후 다음퀘스트 진행하기
    void NextQuest(){
        questId += 10;
        questActionIndex = 0;
    }


    void ControlObject(){
        switch (questId)
        {   //10번 퀘스트 일때, 두번째npc와 대화를 모두 마쳤을 때 게시판을 보이도록 설정
            case 10:
            if(questActionIndex == 2)
                gameobject[0].SetActive(true);
            break;
            //20번 퀘스트 일 때, 게시판을 읽었을 때 게시판이 사라지도록 설정
            case 20:
                if(questActionIndex == 1)
                gameobject[0].SetActive(false);
            break;
        }
    }

}
