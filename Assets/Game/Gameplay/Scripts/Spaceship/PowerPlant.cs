using UnityEngine;
using Utilities;

public class PowerPlant : MonoBehaviour
{
    [SerializeField] private float maxPower;

    [SerializeField] private float rechargeRate;

    [HideInInspector] public float currentPower;

    private void GeneratePower() => currentPower = MathHelpers.SmoothDamp(currentPower, maxPower, Time.deltaTime, rechargeRate);

    public void DrainPower(float amount)
    {
        currentPower -= amount * Time.deltaTime;
        currentPower = Mathf.Clamp(currentPower, 0, maxPower);
    }
    private void Update()=> GeneratePower();
   
}




