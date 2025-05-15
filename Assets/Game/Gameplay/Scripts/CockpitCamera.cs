using JetBrains.Annotations;
using System.Timers;
using UnityEngine;
using Utilities;

public class CockpitCamera : MonoBehaviour
{
    //Altered Third Person Camera for Cockpit view
    //Get reference to target rigid body
    [SerializeField] private Rigidbody target;
    //Damping 
    [SerializeField] private float damping;
    [SerializeField] private float lookDamping;
    //target up the direction of local up direction relative to world space
    private Vector3 targetUp = Vector3.zero;

    [SerializeField] private float currectionSpeed;
    [SerializeField] private float lookOffset;
    [SerializeField] private float height;
    private Camera camera;
    private Vector3 offset;
    [SerializeField] private Vector3 currentVelocity;
    private Vector3 prevVelocity;
    private Vector3 prevRotation;
    private Vector3 currentRotation;
    private Vector3 currentAngularVelocity;
    private Vector3 prevAngularVelocity;
    private Vector3 prevAngle;
    public void Start()
    {
        targetUp = target.transform.up;
        camera = GetComponent<Camera>();
    }
    public Vector3 AngularAcceleration()
    {
        return new Vector3(target.inertiaTensor.x * target.angularVelocity.x, target.inertiaTensor.y * target.angularVelocity.y, target.inertiaTensor.z * target.angularVelocity.z);
    }
    private float Acceleration()
    {
        currentVelocity = prevVelocity;
        prevVelocity = target.linearVelocity;
        return (currentVelocity - prevVelocity).magnitude/Time.deltaTime;
    }
    private float CalculateFOV()
    {

        float currentFov = camera.fieldOfView;
        float targetFieldView = 80;
        if (Acceleration() > 1)
        {
            return Mathf.Lerp(currentFov, targetFieldView, Time.fixedDeltaTime * target.linearVelocity.z);
        }
        else
        {
            return Mathf.Lerp(currentFov, 60, Time.fixedDeltaTime);
        }
    }
 
    public void FakedGeffects()
    {
        Vector3 targetAngle = Utilities.MathHelpers.SmoothDamp(transform.eulerAngles,new Vector3(AngularAcceleration().x * 0.005f, AngularAcceleration().y * 0.005f, AngularAcceleration().z * 0.005f),Time.deltaTime,lookDamping);
       Debug.Log(targetAngle);

        Quaternion targetRotation = Quaternion.Euler(targetAngle);

        transform.localRotation = Quaternion.Inverse( targetRotation) * transform.rotation;
    }
       
    
            


    
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
  
      ///  offset = Vector3.up * height - Vector3.forward * (lookOffset);
       // Vector3 cross = Vector3.Cross(transform.InverseTransformDirection( transform.forward), transform.InverseTransformDirection( target.transform.right));
      
       // targetUp = Vector3.Lerp(targetUp, cross, Time.deltaTime * currectionSpeed);
        offset = target.transform.localPosition + (target.transform.localRotation * offset);
      //  transform.localPosition = MathHelpers.SmoothDamp(transform.localPosition, offset, Time.deltaTime, damping);
        Vector3 direction = transform.InverseTransformDirection(target.transform.position - transform.localPosition + transform.forward * lookOffset).normalized;
      //  Quaternion lookRotation = Quaternion.LookRotation(direction, targetUp);
      //  transform.localRotation = MathHelpers.SmoothDamp(transform.localRotation, lookRotation, Time.deltaTime, lookDamping);
        camera.fieldOfView = CalculateFOV();
      //  FakedGeffects();
    }


}

