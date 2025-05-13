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
    public void Start()
    {
        targetUp = target.transform.up;
        camera = GetComponent<Camera>();
    }
    private float Acceleration()
    {
        currentVelocity = prevVelocity;
        prevVelocity = target.linearVelocity;
        return (currentVelocity - prevVelocity).magnitude/Time.fixedDeltaTime;
    }
    private float CalculateFOV()
    {

        float currentFov = camera.fieldOfView;
        float targetFieldView = 80;
        if (Acceleration() > 0)
        {
            return Mathf.Lerp(currentFov, targetFieldView, Time.fixedDeltaTime);
        }
        else
        {
            return Mathf.Lerp(currentFov, 60, Time.fixedDeltaTime);
        }
    }
    private void Update()
    {
        Debug.Log("1");
    }
    private void FixedUpdate()
    {
        offset = Vector3.up * height - Vector3.forward * (lookOffset);
        Vector3 cross = Vector3.Cross(transform.InverseTransformDirection( transform.forward), transform.InverseTransformDirection( target.transform.right));
      
        targetUp = Vector3.Lerp(targetUp, cross, Time.deltaTime * currectionSpeed);
        offset = target.transform.localPosition + (target.transform.localRotation * offset);
      //  transform.localPosition = MathHelpers.SmoothDamp(transform.localPosition, offset, Time.deltaTime, damping);
        Vector3 direction = transform.InverseTransformDirection(target.transform.position - transform.localPosition + transform.forward * lookOffset).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction, targetUp);
        transform.localRotation = MathHelpers.SmoothDamp(transform.localRotation, lookRotation, Time.deltaTime, lookDamping);
        camera.fieldOfView = CalculateFOV();

    }


}

