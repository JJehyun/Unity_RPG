using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//어떤 대사가 들어갈지 저장
public class TalkMenager : MonoBehaviour
{   //<key,value>=> 한쌍으로 넣어줌 int에 대응하는 문자열
    Dictionary<int, string[]> talkData;
    //초상화를 넣을 Dictionary를 생성함
    Dictionary<int, Sprite> portraitData;   

    //Sprite 파일가져오는 법, 1. 배열 생성
    public Sprite[] portraitDataArr;





    
    void Awake()
    {   // 초기화 진행1,2
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        //데이터를 넣어줄 직접만든 함수를 이용해서 데이터 넣기
        GenerateData();
    }


        //npc에 넣을 데이터를 넣은 직접만든 함수
    void GenerateData()
    {   //여러문장을 넣을것이기 때문에 배열이 와야함(npc-1000 넣어줌 2개의 데이터를 넣었음)
        talkData.Add(1000, new string[] {"안녕?:0", "이곳에 처음 왔구나?:1"});
        talkData.Add(2000, new string[] {"안녕?:0", "나는 2000번째 npc다:0"});
        //100번을 가진 사물에 데이터를 넣음
        talkData.Add(100, new string[] {"평범한 사물이다."});
        //100번을 가진 사물에 데이터를 넣음
        talkData.Add(200, new string[] {"평범한 게시판이다."});
        talkData.Add(300, new string[] {"3번째 게시판이다."});



        //Quest 목록들
        talkData.Add(10+1000, new string[]{"어서와:0", "처음보는 사람이네:1" , "잘가 처음보는 사람이랑 말안해:0", "원쪽2000번째 npc에게 말을 걸어봐:2"});
        //대화가 끝났을 때 questActionIndex++
        talkData.Add(11+2000, new string[]{"어서와:0", "나는 2000번 째 npc야:1","게시판을 읽어봐:2"});

        talkData.Add(20+1000, new string[]{"게시판을 읽으라고?:0", "게시판을 찾아보자:1"});
        talkData.Add(20+2000, new string[]{"게시판은 :0", "근처에 있던거로 기억하는데...:1","한번 찾아봐:2"});
        talkData.Add(20+5000, new string[]{"게시판을 찾았다"});
        talkData.Add(21+2000, new string[]{"게시판을 찾아서 읽었구나!!!:2"});




        portraitData.Add(1000+0,portraitDataArr[0]);
        portraitData.Add(1000+1,portraitDataArr[1]);
        portraitData.Add(1000+2,portraitDataArr[2]);
        portraitData.Add(1000+3,portraitDataArr[3]);
        portraitData.Add(2000+0,portraitDataArr[4]);
        portraitData.Add(2000+1,portraitDataArr[5]);
        portraitData.Add(2000+2,portraitDataArr[6]);
        portraitData.Add(2000+3,portraitDataArr[7]);
    }

    //데이터 가져오는 함수 생성
    public Sprite GetPortrait(int id, int portraitIndex){
        return portraitData[id + portraitIndex];
    }





    public string GetTalk(int id, int talkIndex){
            // 사용자가 찾으려는 key값이 없다면!  해당 퀘스트 진행 순서 중 대사가 없을 때!퀘스트 맨 처음대사를 가져온다.
            if(!talkData.ContainsKey(id)){
                if(!talkData.ContainsKey(id - id%10)){
                    return GetTalk(id - id%100,talkIndex);//퀘스트 맨 처음 대사마저 없을 때
            }else
                    return GetTalk(id - id%10,talkIndex);

        }

            if(talkIndex == talkData[id].Length){
                return null;
            }else{
            //Dictionary함수 값 불러오기 [key] + [key값에 넣은 데이터 인덱스 값]
            return talkData[id][talkIndex];                
            } 
    }
}
