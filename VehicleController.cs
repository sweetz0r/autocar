using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;

public class VehicleController : MonoBehaviour
{
    public float maxSteeringAngle = 30f; // Максимальный угол поворота колес
    public float maxMotorTorque = 1500f; // Максимальный крутящий момент
    public float maxBrakeTorque = 3000f; // Максимальная сила торможения
    public WheelCollider[] wheelColliders; // Коллайдеры колес
    public GameObject[] visualWheels;
    public CircularDrive SteeringWheel;
    private Rigidbody rigidBody;

    public float motorTorque = 2000;
    public float brakeTorque = 2000;
    public float maxSpeed = 20;
    public float steeringRange = 30;
    public float steeringRangeAtMaxSpeed = 10;
    public float centreOfGravityOffset = -1f;

    private void Start()
    {
        rigidBody = GetComponentInParent<Rigidbody>();
        rigidBody.centerOfMass += Vector3.up * centreOfGravityOffset;
        StartCoroutine("EndTest");
    }

    private void Update()
    {
        for(int i = 0; i < 2; i++)
        {
            visualWheels[i].transform.localRotation = Quaternion.Euler(90f, 0f, 90-map(SteeringWheel.outAngle, -900, 900, -45, 45));
        }
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");

        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.velocity);


        float speedFactor = Mathf.InverseLerp(0, maxSpeed, forwardSpeed);

        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);

        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        bool isAccelerating = vInput > 0;


        for (int i = 0; i < 4; i++)
        {
            if(i <= 1) wheelColliders[i].steerAngle = map(SteeringWheel.outAngle, -900, 900, -45, 45);
            if (isAccelerating)
            {
                wheelColliders[i].motorTorque = vInput * currentMotorTorque;
                wheelColliders[i].brakeTorque = 0;
            }
            else
            {
                wheelColliders[i].brakeTorque = Mathf.Abs(vInput) * brakeTorque;
                wheelColliders[i].motorTorque = 0;
            }
        }
    }

    IEnumerator EndTest()
    {
        yield return new WaitForSeconds(60);
        StartCoroutine(GoToGarage());
    }

    IEnumerator GoToGarage()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    float map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    public void Steer(float steeringInput)
    {
        float steering = maxSteeringAngle * steeringInput;
        wheelColliders[0].steerAngle = steering;
        wheelColliders[1].steerAngle = steering;
    }

    public void ApplyDrive(float motorInput, float brakeInput)
    {
        float motor = maxMotorTorque * motorInput;
        float brake = maxBrakeTorque * brakeInput;

        foreach (WheelCollider wheel in wheelColliders)
        {
            if (motor != 0f)
            {
                wheel.motorTorque = motor;
            }

            if (brakeInput < 0)
            {
                wheel.brakeTorque = brake;
            }
            else
            {
                wheel.brakeTorque = 0;
            }
        }
    }
}