using Unity.VisualScripting;
using UnityEngine;

public class ShieldsAndHealth : MonoBehaviour, IDamageable
{
    private float rechargeRate;
    private float powerDraw;
    private float shieldUp, shieldDown, shieldLeft, shieldRight;
    private float health;
    //Call from PowerPlant Script
    public void RechargeShield(ref float currentPower)
    {
       float totalShield = shieldUp + shieldDown + shieldRight + shieldLeft;
        totalShield = Mathf.Clamp(totalShield, 0, 1);
        if(totalShield < 1f)
        {
            shieldUp    += rechargeRate;
            shieldDown  += rechargeRate;
            shieldLeft  += rechargeRate;
            shieldRight += rechargeRate;
            currentPower -= powerDraw;
        }
       
    }
    private void KillSelf()
    {
      if(health< 0f)
        {
            //Explode or do ship death
        }
    }
    public float ApplyDamage(int damage)
    {
        powerDraw += damage * 0.5f;
        powerDraw = Mathf.Clamp(powerDraw, 0, 10f);
        if(shieldUp < 0 || shieldDown < 0|| shieldRight < 0 || shieldLeft < 0)
        {
            //Damage
            health -= damage;
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
        
    }
}
