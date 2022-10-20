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

        ///�v���C���[�̐���
        //�ŏ��̈�񂾂��v���C���[�̏ꏊ�̓����_���ɂ���
        if (!WM.GetFirstPlayer()) {

            GameObject player = GameObject.Find("Player");

            if (Number == NP.GetPlayerPos()) {
                //���g�̐^���Ƀ��C���΂�
                this.transform.eulerAngles = new Vector3(90.0f, 0, 0);
                Vector3 direction = new Vector3(0, -1, 0);
                Ray ray = new Ray(this.transform.position, direction);
                //int layerMask = ~(1 << 13);
                if (Physics.Raycast(ray, out hit, Distance))
                {
                    Instantiate(player, hit.point, Quaternion.identity);
                    WM.SetFirstPlayer(true);
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
        //�G�̐���
        CreateEnemy = GameObject.Find("Enemy");

        //���X�g�̐���������
        numlist = NP.GetNumList();

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
                        //�I�u�W�F�N�g�����o����
                        GameObject Enemy = Instantiate(CreateEnemy, hit.point, Quaternion.identity);
                    }
                }
            }
        }
    }
}
