using UnityEngine;

public class AIBehaviors : MonoBehaviour
{
 
    //Corkskrew
    public void CorkScrew(AIFlight AI,float seconds)
    {
        //AI Pull Up

        //AI Roll

        //AI Move Forward

        //Do this for set amount of seconds
    }
    //Dive
    public void Dive(AIFlight AI)
    {
        //Roll Over 

        //Pitch up

        //After seconds align self to previous then level out
    }

   
    //Roll Left or Roll Right
    public void Roll(Vector3 Roll,float seconds,AIFlight AI)
    {
        //Roll Left

        //Roll Right

        //Do for amount of seconds then level out to previous orientation
    }


    //Lead Target
    public void LeadTarget(AIFlight AI,Vector3 Target,Vector3 TargetVelocity)
    {
        //Lead Target with AI

        //Basically Look

        //Adjust Speed 
    }
    //Flee

    public void Flee(AIFlight AI,Vector3 Target, Vector3 TargetVelocity)
    {
        //Opposite of lead
        //flees the lead
    }



}
