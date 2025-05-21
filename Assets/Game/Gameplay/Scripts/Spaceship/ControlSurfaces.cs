using UnityEngine;
using Utilities;
[RequireComponent (typeof(Engines))]
public class ControlSurfaces : MonoBehaviour
{
    //NOTE: Do not use this on the AI AI uses it's own controls and thrusting
    [HideInInspector] public float currentRoll;
    [HideInInspector] public float currentPitch;
    [HideInInspector] public float currentYaw;
    [HideInInspector] public float RollSpeed;
    [HideInInspector] public float YawSpeed;
    [HideInInspector] public float PitchSpeed;
    [SerializeField] private Transform plane;
    Quaternion upRotation;
    public void PitchYawRoll(float pitchInput,float yawInput,float rollInput)
    {
        
        currentRoll = MathHelpers.SmoothDamp(currentRoll, rollInput * RollSpeed, Time.deltaTime, YawSpeed);
        currentPitch = MathHelpers.SmoothDamp(currentPitch, pitchInput * PitchSpeed, Time.deltaTime, YawSpeed);
        currentYaw = MathHelpers.SmoothDamp(currentYaw, yawInput * YawSpeed, Time.deltaTime, YawSpeed);
        Quaternion PitchYaw = Quaternion.AngleAxis(currentPitch, Vector3.right) * Quaternion.AngleAxis(currentYaw, Vector3.up);
        Quaternion Roll = Quaternion.AngleAxis(currentRoll, Vector3.forward);





        upRotation = Quaternion.Slerp(plane.localRotation, Quaternion.Euler(PitchYaw.eulerAngles.x, PitchYaw.eulerAngles.y, plane.localEulerAngles.z), 0.8f);
        Quaternion eulerPlane = Quaternion.Euler(plane.localEulerAngles.x, plane.localEulerAngles.y, Roll.eulerAngles.z);
        transform.rotation *= upRotation;
        plane.localRotation = eulerPlane;

    }
}
