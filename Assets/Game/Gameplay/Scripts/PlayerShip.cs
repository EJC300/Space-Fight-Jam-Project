using UnityEngine;
using UnityEngine.Rendering;
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
    private float enginePower;
    private float reactorPowerRate;
    private float weaponChargeRate;
    private float capacity;
    private Rigidbody rb;
    private bool canUseByPower;
    Quaternion upRotation;

    private void Start()
    {
        rb= GetComponent<Rigidbody>();
        IntiliazePower();
    }

   

    void PowerFromReactor(ref float from,ref float to)
    {
        float amount = 1;
        capacity -= amount;
      
        from -= amount;
        to += amount;
        to = Mathf.Clamp(to, 0, 100);
        from = Mathf.Clamp(from, 0, 100);
     
        if (from > to)
        {
            capacity += amount;
        }
        Debug.Log("from" + to);
        
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
        /*Power increases at a rate. Each time a power is added to a system capacity is lowered by 1
         * This takes power from another system as well
         * when system is used or effected such weapons firing and shields recharging to maxiumum strength the power is drained
         * if power is < 1 then all systems won't work
         */
        currentPower += 1;
        PlayerInput input = Singleton.instance.PlayerInput;
        
        if(input.PowerToEngines() )
        {
            var shieldPower = shieldUp = shieldDown = shieldLeft = shieldRight;
            PowerFromReactor(ref shieldPower,ref enginePower);
            PowerFromReactor(ref weaponPower,ref enginePower);
            shieldUp = shieldDown = shieldLeft = shieldRight = shieldPower;
        }
        if (input.PowerToShields())
        {
            var shieldPower = shieldUp = shieldDown = shieldLeft = shieldRight;
            
            PowerFromReactor(ref weaponPower,ref shieldPower);
            PowerFromReactor(ref enginePower, ref shieldPower);
            shieldUp = shieldDown = shieldLeft = shieldRight = shieldPower;
        }
        if (input.PowerToWeapons())
        {
            var shieldPower = shieldUp = shieldDown = shieldLeft = shieldRight;
            PowerFromReactor(ref shieldPower, ref weaponPower);
            PowerFromReactor(ref enginePower,ref weaponPower);
            shieldUp = shieldDown = shieldLeft = shieldRight = shieldPower;
        }

        if(input.ResetPower())
        { 
                capacity = 100;
            weaponPower = currentPower * 0.5f;

                shieldUp = shieldDown = shieldLeft = shieldRight = currentPower * 0.5f;

            enginePower = currentPower * 0.5f;

          
        }
       Debug.Log($"shieldDown" + shieldDown + "shieldUp" + shieldUp + "shieldRight" + shieldRight + "shieldLeft" + shieldLeft);
       Debug.Log("WeaponsPower" + weaponPower);
      Debug.Log("EnginesPower" + enginePower);
        Debug.Log("Power" + currentPower);

 
        currentPower = Mathf.Clamp(currentPower,0, capacity);
        
    }
    void IntiliazePower()
    {
        currentPower = 100;
        capacity = 100;
    }
    
    private void Update()
    {
        PowerManagement();
    }
    private void FixedUpdate()
    {
       
        ThrottleToThrust();
        PitchYawRoll();
       
    }
}
