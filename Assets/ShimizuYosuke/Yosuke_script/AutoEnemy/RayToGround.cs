using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayToGround : MonoBehaviour
{
    //���������Ԗڂ̎ˏo�n�_����F�����Ă������߂̔ԍ�
    private int Number;

    [Header("���C�̋���")]
    [SerializeField] private int Distance = 100;

    private GameObject CreateEnemy;

    private List<int> numlist = new List<int>();

    private NumPower NP;

    private WaveManager WM;

    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();
        NP = GameObject.Find("Spawn").GetComponent<NumPower>();

        //CreateEnemy = WM.GetEnemyObj();

        //�G�̐���
        //CreateEnemy = GameObject.Find("Enemy");

        ///�v���C���[�̐���
        //�ŏ��̈�񂾂��v���C���[�̏ꏊ�̓����_���ɂ���
        if (WM.GetWave() == 1) {

            GameObject player = WM.GetPlayerObj();

            if (Number == NP.GetPlayerPos()) {
                //���g�̐^���Ƀ��C���΂�
                this.transform.eulerAngles = new Vector3(90.0f, 0, 0);
                Vector3 direction = new Vector3(0, -1, 0);
                Ray ray = new Ray(this.transform.position, direction);
                //int layerMask = ~(1 << 13);
                if (Physics.Raycast(ray, out hit, Distance))
                {
                    Instantiate(player,new Vector3(hit.point.x, hit.point.y+0.5f, hit.point.z), Quaternion.identity);
                }
            }

        }

        ReCreateEnemy();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���������Ԗڂ���ݒ肷��֐�
    public void SetNumber(int number) {
        Number = number;
    }

    //�G�𐶐����Ȃ����֐�
    public void ReCreateEnemy() {
        //�}�l�[�W���[�ɓG�̐��𑗂�
        WaveManager WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();

        //�G�̃^�O��G�ɂ���
        //CreateEnemy.tag = "Enemy";

        //���X�g�̐���������
        numlist = NP.GetNumList();

        //���ݏo���ꂽ�G�̐����J�E���g����ϐ�
        int cnt = 0;

        foreach (int i in numlist)
        {
            if (Number == i)
            {
                //���g�̐^���Ƀ��C���΂�
                this.transform.eulerAngles = new Vector3(90.0f, 0, 0);
                Vector3 direction = new Vector3(0, -1, 0);
                Ray ray = new Ray(this.transform.position, direction);
                //int layerMask = ~(1 << 13);
                if (Physics.Raycast(ray, out hit, Distance))
                {
                    //���ɓG�I�u�W�F�N�g�����ݏo����Ă����琶�ݏo���Ȃ�
                    if (hit.collider.CompareTag("Enemy") || hit.collider.CompareTag("Player"))
                    {

                    }
                    else
                    {
                        //�������g���č��G�̎�ނ����߂�
                        int rnd = Random.Range(0,2);
                        WM = GameObject.Find("WaveManager").GetComponent<WaveManager>();

                        switch (rnd) {
                            case 0: CreateEnemy = WM.GetNineEnemyObj(); break;
                            case 1: CreateEnemy = WM.GetEighteenEnemyObj(); break;
                            case 2: CreateEnemy = WM.GetWalkEnemyObj(); break;
                        }

                        //�I�u�W�F�N�g�����o����
                        GameObject Enemy = Instantiate(CreateEnemy,new Vector3(hit.point.x, hit.point.y+0.5f, hit.point.z), Quaternion.identity);
                        cnt = 1;
                    }
                }
            }
        }

        if (WM.GetWave() == 1)
        {
            WM.FirstEnemyNum(cnt);
        }
        else {
            WM.SetEnemyNum(cnt);
        }

        //�G�̃^�O��ς���
        //CreateEnemy.tag = "Parent_Enemy";

    }
}
