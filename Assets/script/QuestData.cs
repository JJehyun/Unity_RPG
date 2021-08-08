using System.Collections;
using System.Collections.Generic;

//퀘스트 관련 script 파일
public class QuestData 
{
    public string questName;
    public int[] npcId;


    //class 생성할 때 변수를 받기 위한 생성자 선언하기
    public QuestData(string name , int[] npc){
        questName = name;
        npcId = npc;
    }
}
