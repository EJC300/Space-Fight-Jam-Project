using UnityEngine;
namespace Utilities
{ 
    public class ChaseCamera : MonoBehaviour
    {
        //Traditional Third Person Camera that follows behind a rigidbody target

        //Get reference to target rigid body
        [SerializeField] private Rigidbody target;
        //Damping 
        [SerializeField] private float damping;
        [SerializeField] private float lookDamping;
        //target up the direction of local up direction relative to world space
        private Vector3 targetUp = Vector3.zero;

        [SerializeField] private float currectionSpeed;
        [SerializeField] private float lookOffset;
        [SerializeField] private float height;
        private Vector3 offset;
        public void Start()
        {
            targetUp = target.transform.up;
           
        }

        private void FixedUpdate()
        {
            offset = Vector3.up * height - Vector3.forward * ( lookOffset);
            Vector3 cross = Vector3.Cross(transform.forward, target.transform.right);

            targetUp = Vector3.Lerp(targetUp, cross, Time.deltaTime * currectionSpeed );
            offset = target.position + (target.rotation * offset);
            transform.position = MathHelpers.SmoothDamp(transform.position,offset,Time.deltaTime,damping);
            Vector3 direction = (target.position - transform.position + transform.forward * lookOffset).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction, targetUp);
            transform.rotation = MathHelpers.SmoothDamp(transform.rotation, lookRotation,Time.deltaTime,lookDamping);


        }


    }

}