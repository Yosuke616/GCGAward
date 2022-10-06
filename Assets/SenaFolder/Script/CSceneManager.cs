using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSceneManager : MonoBehaviour
{
    [SerializeField] private CSceneName.SCENENAME LoadSceneNum;
    // Start is called before the first frame update
    void Start()
    {
        switch(LoadSceneNum)
        {
            //case CSceneName.SCENENAME.TITLESCENE:
            //    LoadS
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Enterキー→キー遷移
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //SceneManager.LoadScene(LoadScene);
        }
    }
}
