using Unity.VisualScripting;
using UnityEngine;

public class ShieldsAndHealth : MonoBehaviour, IDamageable
{
    private PowerPlant PowerPlant { get {  return GetComponent<PowerPlant>(); } }
    
    [SerializeField] float rechargeRate;
    [SerializeField] float powerDraw;
    [SerializeField] float shieldUp, shieldDown, shieldLeft, shieldRight;
    [SerializeField] float health;

    //Call from PowerPlant Script
    public void RechargeShield(ref float currentPower)
    {
     
       
        if(shieldDown < 100 || shieldLeft <100 || shieldUp < 100 || shieldRight < 100)
        {
        
            shieldUp    += rechargeRate;
            shieldDown  += rechargeRate;
            shieldLeft  += rechargeRate;
            shieldRight += rechargeRate;
            currentPower -= powerDraw;
        }
        shieldUp = Mathf.Clamp(shieldUp,0,100);
        shieldDown = Mathf.Clamp(shieldDown, 0, 100);
        shieldRight = Mathf.Clamp(shieldRight, 0, 100);
        shieldLeft = Mathf.Clamp(shieldLeft, 0, 100);
    }
    private void KillSelf()
    {
      if(health< 0f)
        {
            //Explode or do ship death
            Destroy(gameObject);
        }
    }
    public float ApplyDamage(int damage)
    {
        powerDraw += damage * 0.5f;

        if(shieldUp < 0 || shieldDown < 0|| shieldRight < 0 || shieldLeft < 0)
        {
            //Damage
            health -= damage;
        }
        if (powerDraw > 0f)
        {
            PowerPlant.DrainPower(powerDraw);
        }
            return damage;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 directionToCollision = collision.contacts[0].normal;
        directionToCollision.Normalize();

        float front = Vector3.Dot(directionToCollision, -transform.forward);
        float back = Vector3.Dot(directionToCollision, transform.forward);
        float left = Vector3.Dot(directionToCollision, transform.right);
        float right = Vector3.Dot(directionToCollision, -transform.right);
        if (collision.gameObject.TryGetComponent(out Damager damage))
        {

            if (front > 0.9f)
            {
                shieldUp -= ApplyDamage(Mathf.FloorToInt(damage.GetDamage));
            }
            if (back > 0.9f)
            {
                shieldDown -= ApplyDamage(Mathf.FloorToInt(damage.GetDamage));
            }

            if (left > 0.9f)
            {
                shieldLeft -= ApplyDamage(Mathf.FloorToInt(damage.GetDamage));
            }

            if (right > 0.9f)
            {
                shieldRight -= ApplyDamage(Mathf.FloorToInt(damage.GetDamage));
            }


        }
    }
    void OnCollisionExit(Collision collision)
    {
        powerDraw = 0;
    }
    void Update()
    {
        powerDraw = Mathf.Clamp(powerDraw, 0, 10f);
        RechargeShield(ref PowerPlant.currentPower);
    }
}
