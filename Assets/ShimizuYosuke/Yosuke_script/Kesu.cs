using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Kesu : MonoBehaviour
{
    [SerializeField] private GameObject kemuri; 

    private const string key_isWalk = "isWalk";
    private const string key_isRun = "isRun";
    private const string key_isFrontWalk = "isFrontWalk";
    private const string key_isBackWalk = "isBackWalk";
    private const string key_isRightWalk = "isRightWalk";
    private const string key_isLeftkey = "isLeftWalk";
    private const string key_isCharge = "isCharge";
    private const string key_isShot = "isShot";
    private const string key_isDamage = "isDamage";
    private const string key_isDeath = "isDeath";
    private Animator animator;
    private bool charge;

    private int shotTime = 0;
    private int DamageTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        charge = false;
        shotTime = 0;
        DamageTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.anyKey) {
            this.animator.SetBool(key_isWalk,false);
            this.animator.SetBool(key_isRun,false);
            this.animator.SetBool(key_isFrontWalk, false);
            this.animator.SetBool(key_isBackWalk, false);
            this.animator.SetBool(key_isRightWalk, false);
            this.animator.SetBool(key_isLeftkey, false);
            this.animator.SetBool(key_isCharge, false);
            this.animator.SetBool(key_isShot, false);
            this.animator.SetBool(key_isDamage, false);
            this.animator.SetBool(key_isDeath, false);
        }

        if (Input.GetMouseButton(0)) {
            this.animator.SetBool(key_isCharge, true);
            charge = true;
            if (!(Input.GetKey(KeyCode.W)) && !(Input.GetKey(KeyCode.A)) &&
                !(Input.GetKey(KeyCode.S)) && !(Input.GetKey(KeyCode.D)))
            {
                this.animator.SetBool(key_isWalk, false);
                this.animator.SetBool(key_isRun, false);
            }

        } else if (!Input.GetMouseButton(0)) {
            charge = false;
            this.animator.SetBool(key_isCharge, false);
            this.animator.SetBool(key_isFrontWalk, false);
            this.animator.SetBool(key_isBackWalk, false);
            this.animator.SetBool(key_isRightWalk, false);
            this.animator.SetBool(key_isLeftkey, false);
            this.animator.SetBool(key_isShot, false);
        }

        if (!charge)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A)||
                    Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
                {
                    this.animator.SetBool(key_isRun, true);
                }
                else if (!(Input.GetKey(KeyCode.W)) && !(Input.GetKey(KeyCode.A) &&
                         !(Input.GetKey(KeyCode.S)) && !(Input.GetKey(KeyCode.D))))
                {
                    this.animator.SetBool(key_isWalk, false);
                    this.animator.SetBool(key_isRun, false);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                    Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    this.animator.SetBool(key_isWalk, true);
                }
                this.animator.SetBool(key_isRun, false);
            }
        }
        else {
            if (Input.GetKey(KeyCode.LeftShift)) {
                this.animator.SetBool(key_isRun, true);
            }

            if (Input.GetKey(KeyCode.A)) {
                this.animator.SetBool(key_isLeftkey, true);
            } else if (!Input.GetKey(KeyCode.A)) {
                this.animator.SetBool(key_isLeftkey, false);
            }
            if (Input.GetKey(KeyCode.D)) {
                this.animator.SetBool(key_isRightWalk, true);
            }
            else if (!Input.GetKey(KeyCode.D)) {
                this.animator.SetBool(key_isRightWalk, false);
            }
            if (Input.GetKey(KeyCode.S)) {
                this.animator.SetBool(key_isBackWalk, true);
            } else if (!Input.GetKey(KeyCode.S)) {
                this.animator.SetBool(key_isBackWalk, false);
            }
            if (Input.GetKey(KeyCode.W)) {
                this.animator.SetBool(key_isFrontWalk, true);
            } else if (!Input.GetKey(KeyCode.W)) {
                this.animator.SetBool(key_isFrontWalk, false);
            }

            if (shotTime <= 0)
            {
                if (Input.GetMouseButton(1))
                {
                    this.animator.SetBool(key_isShot, true);
                    shotTime = 180;
                }
            }
            else {
                this.animator.SetBool(key_isShot, false);
                shotTime--;
            }

        }

        if (DamageTime <= 0)
        {
            //ダメージをくらったかどうか
            if (Input.GetKey(KeyCode.F1))
            {
                this.animator.SetBool(key_isDamage, true);
                DamageTime = 180;
            }
        }
        else {
            this.animator.SetBool(key_isDamage, false);
            DamageTime--;
        }

        if (Input.GetKey(KeyCode.F2)) {
            this.animator.SetBool(key_isDeath, true);
        }
    }

    public void KEMURI_R() {
        GameObject unity = GameObject.Find("IdlePlayer");

        GameObject obj =  Instantiate(kemuri,new Vector3(0,0,0),Quaternion.identity);

        obj.transform.position = unity.transform.position;
        obj.transform.position -= this.transform.up * 0.15f;
        obj.transform.position -= this.transform.forward * 0.25f;
        obj.transform.position += this.transform.right * 0.15f;

        obj.transform.localScale = new Vector3(0.03f,0.03f,0.03f);
        obj.AddComponent<KesuDeath>();
    }

    public void KEMURI_L()
    {
        GameObject unity = GameObject.Find("IdlePlayer");

        GameObject obj = Instantiate(kemuri, new Vector3(0, 0, 0), Quaternion.identity);

        obj.transform.position = unity.transform.position;
        obj.transform.position -= this.transform.up * 0.15f;
        obj.transform.position -= this.transform.forward * 0.25f;
        obj.transform.position -= this.transform.right * 0.15f;

        obj.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        obj.AddComponent<KesuDeath>();
    }

}
