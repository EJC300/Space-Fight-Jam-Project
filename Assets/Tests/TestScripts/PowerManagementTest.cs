using UnityEngine;
using UnityEngine.UIElements;
using Utilities;

public class PowerManagementTest : MonoBehaviour
{
    private float powerPool;
    private float shieldPower;
    private float fireWeapon;
    private float powerDrain;
    private void Update()
    {
        powerDrain = shieldPower;
        powerPool =  100 *(MathHelpers.SmoothDamp(powerPool, 100, Time.deltaTime, 5f)-powerDrain);
        shieldPower = MathHelpers.SmoothDamp(shieldPower, powerPool, Time.deltaTime, 10f);
        fireWeapon = MathHelpers.SmoothDamp(fireWeapon,powerPool,Time.deltaTime,10);

        if(Input.GetMouseButton(0))
        {
            powerDrain += fireWeapon * 20;
        }
        Debug.Log($"{powerPool} power pool");
        Debug.Log($"{powerDrain} powerDrain");
    }




}
