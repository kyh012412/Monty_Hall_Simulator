using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int totalPlays;
    public int totalCorrect;
    public int totalIncorrect;

    void Awake()
    {
        if(instance == null){
            instance = this;
            totalPlays = PlayerPrefs.GetInt("totalPlays",0);
            totalCorrect = PlayerPrefs.GetInt("totalCorrect",0);
            totalIncorrect = PlayerPrefs.GetInt("totalIncorrect",0);            
            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
            return;
        }
    }
    

    public void ChangeScene (int sceneIndex){
        SceneManager.LoadScene(sceneIndex);
    }

    public void Save(){
        PlayerPrefs.SetInt("totalPlays",totalPlays);
        PlayerPrefs.SetInt("totalCorrect",totalCorrect);
        PlayerPrefs.SetInt("totalIncorrect",totalIncorrect);     
    }
}
