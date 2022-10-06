using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSceneManager : MonoBehaviour
{
    //[SerializeField] private CSceneName.SCENENAME LoadSceneNum;
    [SerializeField] private string LoadScene;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // Enterキー→キー遷移
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(LoadScene);
        }
    }
}
