using UnityEngine;
using UnityEngine.Rendering;
using Utilities;
[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    //Fly in a 6DOF way of movement like vertical and side strafing boosting moving forward and backward.
    //Shoot your autocannon at asteroids or saucers
    [SerializeField] private float acceleration;
    [SerializeField] private float maneuveringAcceleration;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float currentPitch, currentYaw,currentRoll;
    [SerializeField] private Transform plane;
    [SerializeField] private Vector3 muzzlePos;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireRate;
    private float nextFire;
    void MoveShip()
    {
        Vector3 forwardDirection = Vector3.forward * acceleration * Singleton.instance.PlayerInput.Throttle();
        Vector3 upDirection = Vector3.up * acceleration * Singleton.instance.PlayerInput.StrafeUP();
        Vector3 leftDirection = Vector3.right * acceleration * Singleton.instance.PlayerInput.Strafe();
        rb.AddRelativeForce(forwardDirection + upDirection + leftDirection);
    }
    public void Shoot()
    {
        if( Time.time > nextFire  && Singleton.instance.PlayerInput.FireSelectedWeapons())
        {
            Instantiate(bullet, transform.TransformPoint(muzzlePos), transform.localRotation);
            nextFire = fireRate + Time.time;
        }
    }
    public void PitchYawRoll(float pitchInput, float yawInput, float rollInput)
    {

        currentRoll = MathHelpers.SmoothDamp(currentRoll, rollInput * rotationSpeed * 25, Time.deltaTime, rotationSpeed);
        currentPitch = MathHelpers.SmoothDamp(currentPitch, pitchInput * rotationSpeed, Time.deltaTime, rotationSpeed);
        currentYaw = MathHelpers.SmoothDamp(currentYaw, yawInput * rotationSpeed, Time.deltaTime, rotationSpeed);
        Quaternion PitchYaw = Quaternion.AngleAxis(currentPitch, Vector3.right) * Quaternion.AngleAxis(currentYaw, Vector3.up);
        Quaternion Roll = Quaternion.AngleAxis(currentRoll, Vector3.forward);





        Quaternion upRotation = Quaternion.Slerp(plane.localRotation, Quaternion.Euler(PitchYaw.eulerAngles.x, PitchYaw.eulerAngles.y, plane.localEulerAngles.z), 0.8f);
        Quaternion eulerPlane = Quaternion.Euler(plane.localEulerAngles.x, plane.localEulerAngles.y, Roll.eulerAngles.z);
        transform.rotation *= upRotation;
        plane.localRotation = eulerPlane;

    }
    
    private void Start()=> rb = GetComponent<Rigidbody>();
    private void Update()
    {
        Shoot();
        
    }
    private void FixedUpdate()
    {
        MoveShip();
        PitchYawRoll(Singleton.instance.PlayerInput.MouseInputPitch(), Singleton.instance.PlayerInput.MouseInputYaw(), Singleton.instance.PlayerInput.RollInput());
      
    }


}
