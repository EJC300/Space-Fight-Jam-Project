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
    float health = 100;
    private float currentPower;
    private float shieldLeftPower;
    private float shieldRightPower;
    private float shieldUpPower;
    private float shieldDownPower;

    private float shieldLeft;
    private float shieldRight;
    private float shieldUp;
    private float shieldDown;
    private float weaponPower;
    private float enginePower;
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
    
      
        from -= amount;
        to += amount;
        to = Mathf.Clamp(to, 0, 100);
        from = Mathf.Clamp(from, 0, 100);
     
       
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
    private void ReachargeShield()
    {
     
        if(shieldUpPower > 0)
        {
            shieldUp = MathHelpers.SmoothDamp(shieldUp,MaxShield,Time.deltaTime, shieldUpPower);
            currentPower -= (shieldUpPower / 100) *5;
        }
        
        if (shieldDownPower > 0)
        {
            shieldDown = MathHelpers.SmoothDamp(shieldDown, MaxShield, Time.deltaTime, shieldDownPower);
            currentPower -= (shieldDownPower / 100) * 5;
        }
      
        if (shieldLeftPower > 0)
        {
            shieldLeft = MathHelpers.SmoothDamp(shieldLeft, MaxShield, Time.deltaTime, shieldLeftPower);
            currentPower -= (shieldLeftPower / 100) * 5;
        }
       

        if (shieldRightPower > 0)
        {
            shieldRight = MathHelpers.SmoothDamp(shieldRight, MaxShield, Time.deltaTime, shieldRightPower);
            currentPower -= (shieldRightPower / 100) * 5;
        }
     
    
    }
    //For each direction determine how much shield damage is done to specific shield side if a shield side reaches zero damage can be taken on that side
    private void ApplyDamage()
    {
        if(shieldLeft < 0 || shieldRight < 0 || shieldUp < 0 || shieldDown < 0)
        {
            //Replace with compoennt model
            health -= 1;
        }
      
    }


    private void OnCollisionEnter(Collision collision)
    {
        Vector3 directionToCollision = collision.contacts[0].normal;
        directionToCollision.Normalize();

        float front = Vector3.Dot(directionToCollision, -transform.forward);
        float back = Vector3.Dot(directionToCollision, transform.forward);
        float left = Vector3.Dot(directionToCollision, transform.right);
        float right = Vector3.Dot(directionToCollision, -transform.right);
        
        
        if(front > 0.9f)
        {
            shieldUp -= 50;
        }
        if(back > 0.9f)
        {
            shieldDown -= 50;
        }

        if(left > 0.9f)
        {
            shieldLeft -= 50;
        }

        if(right < -0.9f)
        {
            shieldRight -= 50;
        }


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

 

    public void SelectFireWeapons()
    {
        PlayerInput input = Singleton.instance.PlayerInput;
        if(input.FireSelectedWeapons() && canUseByPower)
        {
            currentPower -= (weaponPower/100) * 5;
        }
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
            var shieldPower = shieldUpPower = shieldDownPower = shieldLeftPower = shieldRightPower;
            PowerFromReactor(ref shieldPower,ref enginePower);
            PowerFromReactor(ref weaponPower,ref enginePower);
            shieldUpPower = shieldDownPower = shieldLeftPower = shieldRightPower = shieldPower;
        }
        if (input.PowerToShields())
        {
            var shieldPower = shieldUpPower = shieldDownPower = shieldLeftPower = shieldRightPower;
            
            PowerFromReactor(ref weaponPower,ref shieldPower);
            PowerFromReactor(ref enginePower, ref shieldPower);
            shieldUpPower = shieldDownPower = shieldLeftPower = shieldRightPower = shieldPower;
        }
        if (input.PowerToWeapons())
        {
            var shieldPower = shieldUpPower = shieldDownPower = shieldLeftPower = shieldRightPower;
            PowerFromReactor(ref shieldPower, ref weaponPower);
            PowerFromReactor(ref enginePower,ref weaponPower);
            shieldUpPower = shieldDownPower = shieldLeftPower = shieldRightPower = shieldPower;
        }

        if(input.ResetPower())
        { 
                capacity = 100;
            weaponPower = currentPower * 0.5f;

                shieldUpPower = shieldDownPower = shieldLeftPower = shieldRightPower = currentPower * 0.5f;

            enginePower = currentPower * 0.5f;

          
        }
       Debug.Log($"shieldDown" + shieldDownPower + "shieldUp" + shieldUpPower + "shieldRight" + shieldRightPower + "shieldLeft" + shieldLeftPower);
       Debug.Log("WeaponsPower" + weaponPower);
      Debug.Log("EnginesPower" + enginePower);
        Debug.Log("CurrentPower" + currentPower);

        canUseByPower = currentPower > 0;
        currentPower = Mathf.Clamp(currentPower,0, capacity);
        float maxSpeedFromEnginePower=  100*(enginePower/currentPower);
        MaxSpeed = 2 * maxSpeedFromEnginePower;
    }
    void IntiliazePower()
    {
        currentPower = 100;
        capacity = 100;
    }
    
    private void Update()
    {
        PowerManagement();
        SelectFireWeapons();

    }
    private void FixedUpdate()
    {
       
        ThrottleToThrust();
        PitchYawRoll();
        ReachargeShield();
 
       
    }
}
