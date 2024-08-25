using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DealManager : MonoBehaviour
{
    public static DealManager instance;
    public Curtain[] curtains;
    public bool[] isOpened;
    public int answerIndex=-1; // 게임중이 아니면 -1 인덱스는 0,1,2 사용
    public int myIndex=-1;
    public Text presenter;

    public bool isTriedFirst;
    public bool isLocked;

    void Awake()
    {
        if(instance == null){
            instance = this;
            for(int i=0;i<curtains.Length;i++){
                curtains[i].myIndex = i;
            }
            isOpened = new bool[curtains.Length];
        }
    }

    void Start()
    {
        StartCoroutine(GameStart());
    }

    IEnumerator Talk(String line){
        yield return null;
        presenter.text = line;
        yield return null;
    }

    IEnumerator GameStart(){
        // 준비를 알리는 문구 필요
        curtains[0].Hover(true);
        StartCoroutine(Talk("게임이 3초후 시작합니다."));
        yield return new WaitForSeconds(1f);
        
        
        curtains[1].Hover(true);
        StartCoroutine(Talk("게임이 2초후 시작합니다."));
        yield return new WaitForSeconds(1f);
        
        
        curtains[2].Hover(true);
        StartCoroutine(Talk("게임이 1초후 시작합니다."));
        yield return new WaitForSeconds(1f);

        Debug.Log("Game started");
        answerIndex = Random.Range(0,curtains.Length);
        for(int i=0;i<curtains.Length;i++){
            curtains[i].isActionable=true;
            curtains[i].Hover(false);
        }

        // 게임 진행자에게 선택하라는 문구 출력
        StartCoroutine(Talk("예상되는 타겟의 위치를 골라주세요."));

        // 게임 중으로 불변수 변경        
    }

    public void Pick(int index){
        Debug.Log("start of pick with "+index);
        if(isLocked) return;
        isLocked =true;
        myIndex = index;
        if(!isTriedFirst){
            isTriedFirst=true;
            PickFirst();
            return;
        }else{
            PickSecond();
        }
    }

    public void PickFirst(){
        StartCoroutine(PickFirstRoutine());
    }

    IEnumerator PickFirstRoutine(){
        int randomIndex = -1; // 오픈할 커튼의 index (오답이여야 만함)
        while(true){
            randomIndex = Random.Range(0,3);
            if(randomIndex != answerIndex && randomIndex != myIndex){
                break;
            }else{
                Debug.Log("retry randomIndex");
            }
        }

        isOpened[randomIndex] = true;
        curtains[randomIndex].Open();
        curtains[randomIndex].isActionable =false;
        StartCoroutine(Talk(curtains[randomIndex].myIndex+1+ "번 째는 정답이 아니였습니다.\n 선택지를 바꿀 기회를 드리겠습니다.\n 바꾸시겠습니까?\n (최종 선택 할 커튼을 별도로 눌러주세요)"));

        yield return new WaitForSeconds(0.5f);
        
        isLocked = false;
    }

    public void PickSecond(){
        StartCoroutine(PickSecondRoutine());
    }

    IEnumerator PickSecondRoutine(){
        yield return new WaitForSeconds(0.3f);

        if(myIndex == answerIndex){
            Debug.Log("정답 ! 이펙트 필요?");
        }else{
            Debug.Log("오답 ! 다른 이펙트 필요?");
        }
        StartCoroutine(Talk("정답 공개"));

        for(int i=0;i<curtains.Length;i++){
            if(!isOpened[i]){
                curtains[i].Open();
                yield return new WaitForSeconds(1f);
            }
        }

        yield return new WaitForSeconds(0.5f);
        
        isLocked = false;
        CalScore();
        GameEnd();
    }

    public void CalScore(){
        GameManager.instance.totalPlays++;
        if(myIndex == answerIndex){
            GameManager.instance.totalCorrect++;
        }else{
            GameManager.instance.totalIncorrect++;
        }
        GameManager.instance.Save();
    }

    public void GameEnd(){
        Debug.Log("게임 엔드");
        // Replay 버튼이 올라와야 함
    }

    public void Reset(){

    }
}
