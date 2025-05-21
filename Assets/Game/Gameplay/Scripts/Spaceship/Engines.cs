using UnityEngine;
using Utilities;
[RequireComponent(typeof(Rigidbody))]
public class Engines : MonoBehaviour
{
    private Rigidbody rb;
    [HideInInspector] public float afterBurnerSpeed;
    [HideInInspector] public float maxSpeed;
    [HideInInspector] public float acceleration;
    [HideInInspector] public float adjustedSpeed;
    [HideInInspector] public bool afterburner;
    private void Start() => rb = GetComponent<Rigidbody>();

    public void AdjustThrottle(float input)
    {
        adjustedSpeed = MathHelpers.SmoothDamp(adjustedSpeed, (Singleton.instance.PlayerInput.Throttle() / maxSpeed) * maxSpeed, Time.deltaTime, acceleration);
    }


    public void ApplyAfterBurnery(bool input)
    {
        afterburner = input;
        if (afterburner)
        {
            maxSpeed = afterBurnerSpeed;

        }
    }
    private void FixedUpdate()
    {



        rb.AddRelativeForce(Vector3.forward * adjustedSpeed);
        if (!afterburner)
        {
            rb.maxLinearVelocity = maxSpeed;
        }
    }
}
