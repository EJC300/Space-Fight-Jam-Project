using UnityEngine;
using Utilities;

public class PlayerShip : MonoBehaviour
{

    //One Big Monolithic Script For the Player since this game will not be touched again and many of it's code will be ripped from this to be put into other projects.
    [SerializeField] private float MaxShield;

    [SerializeField] private Transform plane;
    [SerializeField] private float PitchSpeed;
    [SerializeField] private float YawSpeed;
    [SerializeField] private float RollSpeed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float AccelerationRate;
    [SerializeField] private float Speed;
    float currentRoll = 0;
    float currentPitch = 0;
    float currentYaw = 0;
    float currentSpeed = 0;
    private float reachageRate = 1f;
    private float currentPower;
    private float shieldLeft;
    private float shieldRight;
    private float shieldUp;
    private float shieldDown;
    private float weaponPower;
    private bool powerToShields;
    private bool powerToWeapons;
    private float reactorPowerRate;
    private float weaponChargeRate;
    private Rigidbody rb;

    Quaternion upRotation;

    private void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    private void ShieldBehavior()
    {
        shieldDown = MathHelpers.SmoothDamp(shieldDown,MaxShield,Time.deltaTime,reachageRate);
        shieldUp = MathHelpers.SmoothDamp(shieldUp, MaxShield, Time.deltaTime, reachageRate);
        shieldLeft = MathHelpers.SmoothDamp(shieldLeft, MaxShield, Time.deltaTime, reachageRate);
        shieldRight = MathHelpers.SmoothDamp(shieldDown, MaxShield, Time.deltaTime, reachageRate);
      
        Debug.Log("ShieldDown " + shieldDown + "ShieldUp" + shieldUp + "ShieldLeft" + shieldLeft + "ShieldRight " + shieldRight);
    }

    private void WeaponPowerCharge()
    {
        weaponPower = MathHelpers.SmoothDamp(weaponPower,MaxShield,Time.deltaTime,weaponChargeRate);
        Debug.Log("Weapon Power" + weaponPower);
    }
    private void ReactorBehavior()
    {
        float powerDrain = (shieldDown + shieldLeft + shieldRight + shieldUp + weaponPower);
        powerDrain = Mathf.Clamp(powerDrain, 0.0f, 1);
        float maxPowerFromDrain = 100 - powerDrain;
      
            currentPower =  MathHelpers.SmoothDamp(currentPower, 100 - powerDrain, Time.deltaTime, reactorPowerRate) - powerDrain * Time.deltaTime;
        Debug.Log("currentPower" + currentPower);
    }
    private void ThrottleToThrust()
    {
        currentSpeed = MathHelpers.SmoothDamp(currentSpeed, Singleton.instance.PlayerInput.Throttle()/ Speed, Time.deltaTime, AccelerationRate);

        rb.AddRelativeForce(Vector3.forward * currentSpeed);


        rb.maxLinearVelocity = MaxSpeed;
    }
   private void ApplyAfterburner()
    {
       
    }

 
    public void PitchYawRoll()
    {
       currentRoll =  MathHelpers.SmoothDamp(currentRoll, Singleton.instance.PlayerInput.RollInput() * RollSpeed, Time.deltaTime, YawSpeed);
       currentPitch = MathHelpers.SmoothDamp(currentPitch, Singleton.instance.PlayerInput.MouseInputPitch() * YawSpeed, Time.deltaTime, YawSpeed);
       currentYaw = MathHelpers.SmoothDamp(currentYaw, Singleton.instance.PlayerInput.MouseInputYaw() * YawSpeed, Time.deltaTime, YawSpeed);
        Quaternion PitchYaw = Quaternion.AngleAxis(currentPitch,Vector3.right) * Quaternion.AngleAxis(currentYaw, Vector3.up);
        Quaternion Roll = Quaternion.AngleAxis(currentRoll , Vector3.forward);
        Quaternion eulerPlane = Quaternion.Euler(plane.localEulerAngles.x, plane.localEulerAngles.y, Roll.eulerAngles.z);
     
      
           

        upRotation = Quaternion.Slerp(plane.localRotation, Quaternion.Euler(PitchYaw.eulerAngles.x, PitchYaw.eulerAngles.y, plane.localEulerAngles.z), 0.8f);
        transform.rotation *= upRotation;
        plane.localRotation = eulerPlane;
    }

    public void SetMaxVelocity()
    {

    }

    public void SelectFireWeapons()
    {

    }

    public void SecondaryWeapons()
    {

    }
    //Very Basic Power Management
    public void PowerManagement()
    {
        //Power To shields
        if (Singleton.instance.PlayerInput.PowerToShields())
        {
            powerToShields = true;


        }
        if(Singleton.instance.PlayerInput.PowerToWeapons())
        {
            powerToWeapons = true;
        }

        if(Singleton.instance.PlayerInput.ResetPower())
        {
            powerToShields=  false;

            reactorPowerRate = 1f;
        }

        if (powerToShields)
        {
            reachageRate = 10;

            reactorPowerRate = 0.5f;

           
        
         
        }
        if(powerToWeapons)
        {
            reactorPowerRate = 0.3f;
            weaponChargeRate = 10;
        }
    }
    private void Update()
    {
        ShieldBehavior();
        ReactorBehavior();
        WeaponPowerCharge();
        PowerManagement();
    }
    private void FixedUpdate()
    {
       
        ThrottleToThrust();
        PitchYawRoll();
       
    }
}
