using Unity.VisualScripting;
using UnityEngine;
namespace Utilities
{ 
    public class ChaseCamera : MonoBehaviour
    {
        //Traditional Third Person Camera that follows behind a rigidbody target
        [SerializeField] private Vector3 currentVelocity;
        //Get reference to target rigid body
        [SerializeField] private Rigidbody target;
        //Damping 
        [SerializeField] private float damping;
        [SerializeField] private float lookDamping;
        //target up the direction of local up direction relative to world space
        private Vector3 targetUp = Vector3.zero;

        [SerializeField] private float currectionSpeed;
        [SerializeField] private float lookOffset;
        [SerializeField] private float lookAhead;
        [SerializeField] private float height;
        [SerializeField] private float maxAccleration;
        private Vector3 offset;
        [SerializeField] private Vector3 previousVelocity;
        public void Start()
        {
            targetUp = target.transform.up;
       

        }
    
        private void FixedUpdate()
        {


            Vector3 cross = Vector3.Cross(transform.forward, target.transform.right);
            offset =  (target.position + target.rotation * new Vector3(0, height, -lookOffset));
          
            targetUp = Vector3.Lerp(targetUp, cross, Time.deltaTime * currectionSpeed);
         
           
            if (LevelController.instance.shifted)
            {
                 previousVelocity = currentVelocity;
                
               
            }
            
             
               
           
            
            transform.position = (Vector3.SmoothDamp(transform.position, offset,ref currentVelocity, damping));
            Vector3 direction = (target.position + (target.transform.forward * lookAhead) - transform.position);
           
         
           Quaternion lookRotation = Quaternion.LookRotation(direction, targetUp);
           transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.fixedDeltaTime * lookDamping);
            currentVelocity = previousVelocity;



        }
            
        }

        

    
}

