using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinProp : MonoBehaviour
{
    [SerializeField] private GameObject Propeller;
    [SerializeField] private Quaternion TargetRotation;
    [SerializeField] private float TargetAngle;
    [SerializeField, Range(0,1000)]
    private float RotationRate;
    // Start is called before the first frame update
    void Start()
    {
        TargetAngle = 0f;
        TargetRotation = Quaternion.AngleAxis(TargetAngle, Vector3.forward);
        Propeller.transform.localRotation = TargetRotation;
    }

    // Update is called once per frame
    void Update()
    {
        TargetAngle += RotationRate * Time.fixedDeltaTime;
        TargetRotation = Quaternion.AngleAxis(TargetAngle, Vector3.forward);
        Propeller.transform.localRotation = TargetRotation;
        //if (TargetAngle >= 360f)
        //{
            //TargetAngle -= 360f;
        //}
    }
}
