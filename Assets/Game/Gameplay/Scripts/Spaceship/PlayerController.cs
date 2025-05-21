using Unity.VisualScripting;
using UnityEngine;
[RequireComponent (typeof(ControlSurfaces),typeof(Engines))]
public class PlayerController : MonoBehaviour
{
    private ControlSurfaces controlSurfaces;
    private Engines engines;
    private PlayerInput input;
    [SerializeField] private float PitchSpeed;
    [SerializeField] private float YawSpeed;
    [SerializeField] private float RollSpeed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float AccelerationRate;
    [SerializeField] private float Speed;
    [SerializeField] private float AfterBurnerSpeed;
    void Start()
    {
       controlSurfaces = GetComponent<ControlSurfaces>();
       engines = GetComponent<Engines>();
       input = Singleton.instance.PlayerInput;
       controlSurfaces.PitchSpeed = PitchSpeed;
       controlSurfaces.RollSpeed = RollSpeed;
       controlSurfaces.YawSpeed = YawSpeed;

       engines.acceleration = AccelerationRate;
       engines.afterBurnerSpeed = AfterBurnerSpeed;
       engines.maxSpeed = MaxSpeed;
       
    }

    private void FixedUpdate()
    {
        engines.AdjustThrottle(input.Throttle());
        //Afterburner
        controlSurfaces.PitchYawRoll(input.MouseInputPitch(), input.MouseInputYaw(), input.RollInput());
       
    }

}
