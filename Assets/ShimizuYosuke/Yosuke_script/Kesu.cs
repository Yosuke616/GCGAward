using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class Kesu : MonoBehaviour
{
    [Header("‰¹‚ÌŽí—Þ")]
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip run;

    private AudioSource AS;

    [Header("ƒsƒbƒ`‚Ì•ÏX")]
    [SerializeField] private float pitchRange = 0.1f;

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

    private bool ControllInputFlg = false;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
        charge = false;
        shotTime = 0;
        DamageTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (DamageTime <= 0)
        {
            this.animator.SetBool(key_isDamage, false);
        }
        else {
            DamageTime--;
        }

        if (!PlayerInputTest.GetControllerUse())
        {
            if (!Input.anyKey)
            {
                this.animator.SetBool(key_isWalk, false);
                this.animator.SetBool(key_isRun, false);
                this.animator.SetBool(key_isFrontWalk, false);
                this.animator.SetBool(key_isBackWalk, false);
                this.animator.SetBool(key_isRightWalk, false);
                this.animator.SetBool(key_isLeftkey, false);
                this.animator.SetBool(key_isCharge, false);
                this.animator.SetBool(key_isShot, false);
            }

            if (PlayerInputTest.GetChargeMode())
            {
                this.animator.SetBool(key_isCharge, true);
                charge = true;
                if (!(Input.GetKey(KeyCode.W)) && !(Input.GetKey(KeyCode.A)) &&
                    !(Input.GetKey(KeyCode.S)) && !(Input.GetKey(KeyCode.D)))
                {
                    this.animator.SetBool(key_isWalk, false);
                    this.animator.SetBool(key_isRun, false);
                }

            }
            else 
            {
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
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
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
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    //this.animator.SetBool(key_isRun, true);
                }

                if (Input.GetKey(KeyCode.A))
                {
                    this.animator.SetBool(key_isLeftkey, true);
                }
                else if (!Input.GetKey(KeyCode.A))
                {
                    this.animator.SetBool(key_isLeftkey, false);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    this.animator.SetBool(key_isRightWalk, true);
                }
                else if (!Input.GetKey(KeyCode.D))
                {
                    this.animator.SetBool(key_isRightWalk, false);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    this.animator.SetBool(key_isBackWalk, true);
                }
                else if (!Input.GetKey(KeyCode.S))
                {
                    this.animator.SetBool(key_isBackWalk, false);
                }
                if (Input.GetKey(KeyCode.W))
                {
                    this.animator.SetBool(key_isFrontWalk, true);
                }
                else if (!Input.GetKey(KeyCode.W))
                {
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
                else
                {
                    this.animator.SetBool(key_isShot, false);
                    shotTime--;
                }

            }
        }
        else
        {
            if (!ControllInputFlg)
            {
                this.animator.SetBool(key_isWalk, false);
                this.animator.SetBool(key_isRun, false);
                this.animator.SetBool(key_isFrontWalk, false);
                this.animator.SetBool(key_isBackWalk, false);
                this.animator.SetBool(key_isRightWalk, false);
                this.animator.SetBool(key_isLeftkey, false);
                this.animator.SetBool(key_isCharge, false);
                this.animator.SetBool(key_isShot, false);
            }

            if (PlayerInputTest.GetChargeMode())
            {
                this.animator.SetBool(key_isCharge, true);
                charge = true;
                if (!Player_Walk.WalkFlg)
                {
                    this.animator.SetBool(key_isWalk, false);
                    this.animator.SetBool(key_isRun, false);
                }

            }
            else
            {
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
                if (Player_Walk.DashFlg)
                {
                    if (Player_Walk.WalkFlg)
                    {
                        this.animator.SetBool(key_isRun, true);
                    }
                    else if (!Player_Walk.WalkFlg)
                    {
                        this.animator.SetBool(key_isWalk, false);
                        this.animator.SetBool(key_isRun, false);
                    }
                }
                else
                {
                    if (Player_Walk.WalkFlg)
                    {
                        this.animator.SetBool(key_isWalk, true);
                    }
                    this.animator.SetBool(key_isRun, false);
                }
            }
            else
            {
                if (Player_Walk.DashFlg)
                {
                    this.animator.SetBool(key_isRun, true);
                }

                if(Player_Walk.WalkFlg)
                {
                    this.animator.SetBool(key_isFrontWalk, true);

                }
                else
                {
                    this.animator.SetBool(key_isFrontWalk, false);

                }
 
                if (shotTime <= 0)
                {
                    if (Gamepad.current.bButton.isPressed)
                    {
                        this.animator.SetBool(key_isShot, true);
                        shotTime = 180;
                    }
                }
                else
                {
                    this.animator.SetBool(key_isShot, false);
                    shotTime--;
                }

            }
        }

        //if (Input.GetKey(KeyCode.F2)) {
        //    this.animator.SetBool(key_isDeath, true);
        //}
    }

    public void SetDamageAnim() {
        this.animator.SetBool(key_isDamage, true);
        DamageTime = 30;
    }

    public void SetDeathAnim() {
        this.animator.SetBool(key_isDeath, true);
    }

    public void KEMURI_R() {
        GameObject unity = GameObject.Find("idle beushup");

        GameObject obj =  Instantiate(kemuri,new Vector3(0,0,0),Quaternion.identity);

        obj.transform.position = unity.transform.position;
        obj.transform.position -= this.transform.up * 0.1f;
        obj.transform.position += this.transform.forward * 1.0f;
        obj.transform.position += this.transform.right * 0.15f;

        obj.transform.localScale = new Vector3(0.03f,0.03f,0.03f);
        obj.AddComponent<KesuDeath>();
    }

    public void KEMURI_L()
    {
        GameObject unity = GameObject.Find("idle beushup");

        GameObject obj = Instantiate(kemuri, new Vector3(0, 0, 0), Quaternion.identity);

        obj.transform.position = unity.transform.position;
        obj.transform.position -= this.transform.up * 0.1f;
        obj.transform.position += this.transform.forward * 1.0f;
        obj.transform.position -= this.transform.right * 0.15f;

        obj.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        obj.AddComponent<KesuDeath>();
    }

    public void WalkSound()
    {
        AS.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
        AS.volume = 0.1f;
        AS.PlayOneShot(walk);
    }

    public void RunSound()
    {
        AS.pitch = 2.0f + Random.Range(-pitchRange, pitchRange);
        AS.volume = 0.1f;
        AS.PlayOneShot(run);
    }

}
