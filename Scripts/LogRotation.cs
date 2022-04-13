using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRotation : MonoBehaviour
{
    [System.Serializable]
    public class RotationElement
    {
        public float Speed;
        public float Duration;
    }

    [SerializeField]
    private RotationElement[] rotationPattern;

    [SerializeField]
    private GameObject[] logGO;

    private WheelJoint2D wheelJoint;
    private JointMotor2D motor;


    private void Awake()
    {
        var GO = Instantiate(logGO[Random.Range(0, logGO.Length)], this.transform);
        wheelJoint = GetComponent<WheelJoint2D>();
        wheelJoint.connectedBody = GO.GetComponent<Rigidbody2D>();
        motor = new JointMotor2D();

        StartCoroutine("PlayRotationPattern");
    }

    private IEnumerator PlayRotationPattern()
    {
        int rotationIndex = 0;
        while (true)
        {
            yield return new WaitForFixedUpdate();

            motor.motorSpeed = rotationPattern[rotationIndex].Speed;
            motor.maxMotorTorque = 1;
            wheelJoint.motor = motor;

            yield return new WaitForSecondsRealtime(rotationPattern[rotationIndex].Duration);
            rotationIndex = Random.Range(0, rotationPattern.Length-1);

        }
    }
}
