using UnityEngine;
namespace Utilities
{
    public class MathHelpers
    {
        /*
         * Math helpers such as smooth damp linear remapping and other common mathematical methods
         * 
         */

        //Smooth Damp 

       

        public static float SmoothDamp(float start,float end,float dt,float damp)
        {
            return Mathf.Lerp(start, end, Mathf.Abs(1 - Mathf.Lerp(0, end, dt) * Time.deltaTime));
        }
        public static Vector3 SmoothDamp(Vector3 start, Vector3 end, float dt, float damp)
        {
            float x = SmoothDamp(start.x, end.x, dt, damp);
            float y = SmoothDamp(start.y, end.y, dt, damp);
            float z = SmoothDamp(start.z, end.z, dt, damp);
            return new Vector3(x, y, z);
        }

        public static Quaternion SmoothDamp(Quaternion start, Quaternion end, float dt, float damp)
        {
            Vector3 startEuler = SmoothDamp(start.eulerAngles, end.eulerAngles, dt, damp);

            return Quaternion.Slerp(start, Quaternion.Euler(startEuler.x, startEuler.y, startEuler.z), damp * dt);
        }
        //Linear Remapping 
        public static float Remap(float value ,float startMin,float startMax,float endMin,float endMax)
        {
            return value +(startMax - startMax) * (value - endMin)/(startMin - endMin);
        }

        public static Vector3 Remap(Vector3 value,Vector3 startMin,Vector3 startMax,Vector3 endMin,Vector3 endMax)
        {
            float x = Remap(value.x,startMin.x, startMax.x, endMin.x, endMax.x);
            float y = Remap(value.y, startMin.y, startMax.y, endMin.y, endMax.y);
            float z = Remap(value.z,startMin.z, startMax.z, endMin.z, endMax.z);
            return new Vector3(x,y,z);
        }
        public static Quaternion Remap(Quaternion value, Quaternion startMin, Quaternion startMax, Quaternion endMin, Quaternion endMax)
        {
            Vector3 valueRotation = value.eulerAngles;
            Vector3 startMinRotation = startMin.eulerAngles;
            Vector3 startMaxRotation = startMax.eulerAngles;
            Vector3 endMinRotation = endMin.eulerAngles;
            Vector3 endMaxRotation = endMax.eulerAngles;

            float x = Remap(valueRotation.x,startMin.x,startMaxRotation.x,endMinRotation.x,endMaxRotation.x);
            float y = Remap(valueRotation.y, startMin.y, startMaxRotation.y, endMinRotation.y, endMaxRotation.y); 
            float z = Remap(valueRotation.z, startMin.z, startMaxRotation.z, endMinRotation.z, endMaxRotation.z); 

            return Quaternion.Euler(x,y,z);
        }
        //Dot product check based on first and next orientation
        public static bool IsBehindOrientation(Vector3 value, Vector3 target,float threshhold)
        {
            Vector3 direction = (target - value).normalized;

            float dot = Vector3.Dot(value,direction);
            return dot > threshhold;
        }
        public static bool IsInFrontOrientation(Vector3 value, Vector3 target, float threshhold)
        {
            Vector3 direction = (target - value).normalized;

            float dot = Vector3.Dot(value, direction);
            return dot < threshhold;
        }
        //Closest Distance 
        public static float GetClosestDistance(Vector3 start, Vector3 end)
        {
           float maxDistance = Mathf.Infinity;
           float distance = (end - start).sqrMagnitude;
            if (distance < maxDistance)
            {

                maxDistance = distance;

            }
            return Mathf.Min(distance,maxDistance);
        }
        //Target Lead
        public static Vector3 TargetLead(Transform targetTransform,Transform currentTransform,Vector3 velocity,Vector3 targetVelocity)
        {
            Vector3 direction = (( currentTransform.rotation*currentTransform.position) -  ( targetTransform.rotation* targetTransform.position));
            float speedDiff = (velocity - targetVelocity).magnitude;
            Vector3 lead = targetTransform.position + direction * speedDiff;

            return lead;

        }

        
    }
}
