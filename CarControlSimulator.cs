using UnityEngine;
using Valve.VR;

public class CarControlSimulator : MonoBehaviour
{
    //public WheelCollider[] wheelColliders;
    //public float steeringDifficulty = 0.5f;
    //public SteamVR_Action_Vector2 steeringAction;
    //public SteamVR_Action_Vector2 pedalsAction;
    //public SteamVR_Action_Vibration hapticAction;
    //public float maxMotorTorque = 1500f; // Максимальный крутящий момент
    //public float maxBrakeTorque = 3000f; // Максимальная сила торможения

    //private float originalSteeringAngle;
    //private float steeringAngle;
    //private VehicleController vehicleController;

    //void Start()
    //{
    //    InitializeController();
    //}

    //void Update()
    //{
    //    ProcessSteering();
    //    ProcessPedals();
    //}

    //private void InitializeController()
    //{
    //    vehicleController = GetComponent<VehicleController>();
    //    originalSteeringAngle = vehicleController.maxSteeringAngle;
    //}

    //private void ProcessSteering()
    //{
    //    steeringAngle = originalSteeringAngle;
    //    Vector2 steeringInput = steeringAction.GetAxis(SteamVR_Input_Sources.Any);
    //    vehicleController.maxSteeringAngle = steeringAngle;
    //    vehicleController.Steer(steeringInput.x);
    //}

    //private void ProcessPedals()
    //{
    //    Vector2 pedalInput = pedalsAction.GetAxis(SteamVR_Input_Sources.Any);
    //    float motor = maxMotorTorque * pedalInput.y;
    //    float brake = maxBrakeTorque * -pedalInput.y;

    //    foreach (WheelCollider wheel in wheelColliders)
    //    {
    //        wheel.motorTorque = motor;
    //        if (pedalInput.y < 0)
    //        {
    //            wheel.brakeTorque = brake;
    //        }
    //        else
    //        {
    //            wheel.brakeTorque = 0;
    //        }
    //    }
    //}
}