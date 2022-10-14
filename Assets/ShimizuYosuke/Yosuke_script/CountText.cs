using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountText : MonoBehaviour
{
    [SerializeField] private GameObject block;

    //�X�R�A���Ǘ�����ϐ�
    private int nScore;
    [SerializeField] Text scoreText;

    //�v���C���[�̏����擾
    [SerializeField] GameObject player;
    private float old_player;
    private float Max_Z_Pos = 0;

    // Start is called before the first frame update
    void Start()
    {
        nScore = 0;
        old_player = player.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //�X�R�A�̕\��
        scoreText.text = "SCORE:" + string.Format("{0}",nScore);

        //1�u���b�N���i�񂾎��������Z����


        //Z�����Ƀv���C���[���i�񂾂Ƃ������X�R�A�����Z������
        if (old_player < player.transform.position.z) {
            //�ő�l���傫���Ȃ�������Z����
            if (old_player > Max_Z_Pos) {
                AddScore(10);
                Max_Z_Pos = old_player;
            }
        }

        //���݂̍��W��ۑ����Ă���
        old_player = player.transform.position.z;
    }

    // �X�R�A�����Z������
    public void AddScore(int Score) {
        nScore += Score;
    }
}
