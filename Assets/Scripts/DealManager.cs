using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealManager : MonoBehaviour
{
    public GameObject[] curtains;
    public int answerIndex=-1; // 게임중이 아니면 -1 인덱스는 0,1,2 사용

    void Start()
    {
        Invoke("GameStart",3f);
    }

    public void GameStart(){
        Debug.Log("Game started");
        answerIndex = Random.Range(0,3);

        // 게임 진행자에게 선택하라는 문구 출력
        // 게임 중으로 불변수 변경

        
    }

    public void GameEnd(){

    }
}
