using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("-------[ Game ]")]
    public int answerIndex;
    public GameObject[] curtains;

    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
            return;
        }
    }
    

    public void ChangeScene (int sceneIndex){
        SceneManager.LoadScene(sceneIndex);
    }
}
