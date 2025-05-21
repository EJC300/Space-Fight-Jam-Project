using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float damage;

    public float GetDamage { get { return damage; } }
}
