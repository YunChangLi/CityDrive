using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAnimController : MonoBehaviour
{
    [SerializeField]
    private Animator _streeringAnim;
    [SerializeField]
    private GameObject _codeTable;
    [SerializeField]
    private RCC_CarControllerV3 rCC;
    [SerializeField]
    private GameObject _rpm;

    

    private void Update()
    {
        GetStreeringRot(Input.GetAxis("Horizontal")) ;
        GetCodeTableRot(rCC.speed);
        GetRpmRot(rCC.engineRPM);
    }

    public void GetStreeringRot(float Rot)
    {
        if (Rot > 0)
        {
            _streeringAnim.Play("Right", 0, Rot);
        }
        else if (Rot < 0)
        {
            _streeringAnim.Play("Left", 0,Mathf.Abs( Rot));
        }
        else
            _streeringAnim.Play("Idle",0);

    }

    public void GetCodeTableRot(float Rot)
    {
        float UnitAngle = 100f / 60f;
        _codeTable.transform.localRotation = Quaternion.Euler(0,0,-(Rot*UnitAngle)) ;
    }
    public void GetRpmRot(float Rot)
    {
        float UnitAngle = 260f / 7000f;
        _rpm.transform.localRotation = Quaternion.Euler(0, 0, -(Rot * UnitAngle));
    }

}
