using System.Collections;
using UnityEngine;

public class Blaster : MonoBehaviour
{
    private PowerPlant powerPlant { get {  return GetComponent<PowerPlant>(); } }

    [SerializeField] private float DrainRate;

    [SerializeField] private float FireRate;

    [SerializeField] private GameObject bullet;

    [SerializeField] private Vector3 offset;
    IEnumerator FireGun()
    {

        yield return new WaitForSeconds(FireRate);
        //MuzzlFlash | Sound
        powerPlant.DrainPower(DrainRate);
        Instantiate(bullet,offset,transform.parent.rotation);
    }

}
