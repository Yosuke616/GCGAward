using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �X�R�A�Ǘ�
public class CScore : MonoBehaviour
{
    // �ϐ��錾
    #region variable
    private int g_nScore;       // �X�R�A
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        g_nScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(g_nScore);
    }

    /*
      * @brief �X�R�A�̕ύX
      * @param num ���Z���鐔(�}�C�i�X���\)
      * @sa �G��|�����Ƃ�
      * @details �����̐��l���X�R�A�ɉ��Z����
    */
    public void addScore(int num)
    {
        g_nScore += num;
    }
}
