using System;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    Rigidbody Rb;

    private int Lives = 10;

    public bool EnemyMode;

    [SerializeField] GameObject EnemyObject;

    [SerializeField] TextMeshProUGUI LiveTxt;

    [SerializeField] Transform Com;

    [SerializeField] WheelCollider RWC;

    private Vector3 RWpos;
    private Quaternion RWrot;

    [SerializeField] WheelCollider RWC2;

    [SerializeField] WheelCollider FWC;

    private Vector3 FWpos;
    private Quaternion FWrot;

    [SerializeField] Transform FWM;

    [SerializeField] Transform RWM;

    [SerializeField] Transform Rule;

    private int accel;

    private float steer;

    private int brake;

    private Quaternion TargetSteerPos;

    public static Action<bool> StopGame;

    private float timerCollision;

    void Awake()
    {
        Rb = gameObject.GetComponent<Rigidbody>();

        Rb.centerOfMass = Com.localPosition;
    }

    void Start()
    {
        Time.timeScale = 1;

        if(!EnemyMode)
        {
            UIInput.Muve += Muve;

            UIInput.Steer += Steer;
        }
            
    }

    void OnDisable()
    {
        if (!EnemyMode)
        {
            UIInput.Muve -= Muve;

            UIInput.Steer -= Steer;
        }
    }

    void Update()
    {
        if (Lives <= 0)
        {
            if(EnemyMode)
                StopGame?.Invoke(true);
            else
                StopGame?.Invoke(false);
        }
            

        FWC.GetWorldPose(out FWpos, out FWrot);

        FWM.position = FWpos;
        FWM.rotation = FWrot;

        RWC.GetWorldPose(out RWpos, out RWrot);

        RWM.position = RWpos;
        RWM.rotation = RWrot;

    }

    void FixedUpdate()
    {
        RWC.motorTorque = 200 * accel;

        RWC2.motorTorque = 200 * accel;

        float fwdSpeed = Vector3.Dot(Rb.velocity, transform.forward);

        if ((accel > 0 && fwdSpeed < -0.3f) || (accel < 0 && fwdSpeed > 0.3f))
            brake = 1;

        if ((accel < 0 && fwdSpeed < 0.3f) || (accel > 0 && fwdSpeed > -0.3f))
            brake = 0;

        RWC.brakeTorque = 450 * brake;

        RWC2.brakeTorque = 450 * brake;

        FWC.brakeTorque = 450 * brake;

        TargetSteerPos.eulerAngles = new Vector3(-18.527f, 20f * steer, 0);

        Rule.localRotation = Quaternion.Lerp(Rule.localRotation, TargetSteerPos, 2.3f * Time.deltaTime);

        FWC.steerAngle = Mathf.Lerp(FWC.steerAngle, 20f * steer, 2.3f * Time.deltaTime);

        if(EnemyMode)
        {
            if (!EnemyObject)
                Debug.Log("нет объекта для отслеживания");
            else
            {
                //----Направление поворота------------------------------------------------------------------------------
                Vector3 DeltaPos = EnemyObject.transform.position - transform.position;

                float PlayerBySide = Vector3.Dot(DeltaPos, transform.right);

                if (PlayerBySide > 0)
                    steer = 1;

                if (PlayerBySide < 0)
                    steer = -1;

                if (PlayerBySide <= 1 && PlayerBySide >= -1)
                    steer = PlayerBySide;

                //----Измерение дистанции лучом-------------------------------------------------------------------------
                Ray rayTorm = new Ray(transform.position, transform.forward);

                RaycastHit hit;

                if (Physics.Raycast(rayTorm, out hit, 50))
                {
                    if (hit.collider.tag == "Arena")
                    {
                        if (hit.distance < 6f)
                        {
                            accel = -1;
                        }

                        if (hit.distance > 8)
                        {
                            accel = 1;
                        }
                    }
                    else
                        accel = 1;



                    Debug.DrawLine(transform.position, hit.point, Color.red);
                }
            }  
        }
    }

    

    void Muve(int value)
    {
        accel = value;
    }

    void Steer(int value)
    {
        steer = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Arena" && timerCollision < Time.realtimeSinceStartup)
        {
            if (collision.gameObject.tag == "Spear")
            {
                Damage(4);
            }

            if (collision.gameObject.tag == "Player")
            {
                Damage(1);
            }

            if (collision.gameObject.tag == "Apt")
            {
                if (Lives < 10)
                    Lives += 5;

                if (Lives > 10)
                    Lives = 10;

                Destroy(collision.gameObject);
            }

            if (Lives >= 0)
                LiveTxt.text = Lives.ToString();
            else
                LiveTxt.text = "0";
        }
       
    }

    public void Damage(int live)
    {
        Lives -= live;

        timerCollision = Time.realtimeSinceStartup + 2f;
    }
}
