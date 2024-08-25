using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    public int myIndex=-1;
    public bool isActionable;
    Animator anim;

    void OnEnable()
    {
        
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnMouseEnter() {
        if(!isActionable) return;
        // Debug.Log("커튼 - 마우스 진입");
        Hover(true);
    }

    void OnMouseUp()
    {
        if(!isActionable) return;
        DealManager.instance.Pick(myIndex);
    }

    void OnMouseExit()
    {
        if(!isActionable) return;
        // Debug.Log("커튼 - 마우스 탈출");
        Hover(false);
    }

    public void Open(){
        if(!isActionable) return;
        if(DealManager.instance.answerIndex == myIndex){
            anim.SetTrigger("doAnswer");
        }else{
            anim.SetTrigger("doWrong");
        }
    }

    public void Hover(bool b){
        anim.SetBool("isHover",b);
    }
}
