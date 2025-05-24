using UnityEngine;

public class Bullet : Damager
{
    [SerializeField] private float speed;

    [SerializeField] private float deathTime;

    [SerializeField] private Rigidbody rb;

    private void FixedUpdate()
    {
        LevelController.instance.WrapAround(transform);
        rb.linearVelocity = speed * transform.forward;
        rb.maxLinearVelocity = speed;
        Destroy(this.gameObject,deathTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
