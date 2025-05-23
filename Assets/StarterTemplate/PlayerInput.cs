using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float sensitivityY;
    [SerializeField] private float sensitivityX;
    float throttleInput = 0;
    float strafeInput = 0;
    float strafeUpInput = 0;
    //TODO support remapping.
    public bool OnExitToMenu()
    {       // This method is called when the player wants to exit to the main menu.
            // It returns true if the player pressed the exit button.
        return Keyboard.current.escapeKey.wasPressedThisFrame;
    }

    public bool OnLoadFirstLevel()
    {       // This method is called when the player wants to start the game.
            // It returns true if the player pressed the start button.
        return Keyboard.current.anyKey.wasPressedThisFrame;
    }

    //Method for rolling input

    public float RollInput()
    {
        float rollLeft = Keyboard.current.qKey.magnitude;
        float rollRight = Keyboard.current.eKey.magnitude;

        return rollLeft + -rollRight;
    }
    //I like pure relative mouse movement 
    //Method for pitch input
    public float MouseInputPitch()
    {
        Vector2 delta = Mouse.current.delta.ReadValue();
        return Mathf.Approximately(delta.y * sensitivityY, 0f) ? 0f : delta.y * sensitivityY;
       
    }

    // Method for yaw input (horizontal mouse movement)
    public float MouseInputYaw()
    {
        Vector2 delta = Mouse.current.delta.ReadValue();
        return Mathf.Approximately(delta.x * sensitivityX, 0f) ? 0f : delta.x * sensitivityX;
      
    }


    public float Throttle()
    {
        
       
     
        if(Keyboard.current.wKey.IsPressed())
        {
            throttleInput += 1;
        }
        else if (Keyboard.current.sKey.IsPressed())
        {
            throttleInput -= 1;
        }
        else
        {
            throttleInput = 0;
        }
       
            throttleInput = Mathf.Clamp(throttleInput, -1f, 1f);
        return throttleInput;
    }

    public float Strafe()
    {
        if (Keyboard.current.aKey.IsPressed())
        {
            strafeInput += 1;
        }
        else if (Keyboard.current.dKey.IsPressed())
        {
            strafeInput -= 1;
        }
        else
        {
            strafeInput = 0;
        }
            strafeInput = Mathf.Clamp(strafeInput, -1f, 1f);
        return strafeInput;
    }
    public float StrafeUP()
    {
        if (Keyboard.current.rKey.IsPressed())
        {
            strafeUpInput += 1;
        }
        else if (Keyboard.current.fKey.IsPressed())
        {
            strafeUpInput -= 1;
        }
        else
        {
            strafeUpInput = 0;
        }
            strafeUpInput = Mathf.Clamp(strafeUpInput, -1f, 1f);
        return strafeUpInput;
    }
    // Switch weapons
    public bool SwitchWeaponsInput()
    {
        return Keyboard.current.digit1Key.wasPressedThisFrame;
    }
    //Switch Missiles
    public bool SwitchMissilesInput()
    {
        return Keyboard.current.digit1Key.wasPressedThisFrame;
    }
    //Divert Power to componets



    //Power to Shields

    public bool PowerToShields()
    {
        return Keyboard.current.leftArrowKey.wasPressedThisFrame;
    }

    //Power to Weapons

    public bool PowerToWeapons()
    {
        return Keyboard.current.rightArrowKey.wasPressedThisFrame;
    }

    //Power to Engines

    public bool PowerToEngines()
    {
        return Keyboard.current.upArrowKey.wasPressedThisFrame;
    }

    //Reset Power

    public bool ResetPower()
    {
        return Keyboard.current.downArrowKey.wasPressedThisFrame;
    }

    //Radar Selection


    //Nearest Target
    public bool NearestTarget()
    {
        return Keyboard.current.fKey.wasPressedThisFrame;
    }

    //Cycle Targets

    public bool CycleTarget()
    {
        return Keyboard.current.tKey.wasPressedThisFrame;
    }

    //Interaction key like autopilot transition

    public bool Interaction()
    {
        return Keyboard.current.eKey.wasPressedThisFrame;
    }

    //Fire Selected Weapons
    public bool FireSelectedWeapons()
    {
        return Mouse.current.leftButton.wasPressedThisFrame;
    }

    //Fire Selected Missile
    public bool FireSelectedMissile()
    {
        return Mouse.current.rightButton.wasPressedThisFrame;
    }

    public bool TogglePause()
    {
        return Keyboard.current.escapeKey.wasPressedThisFrame;
    }

    //Change Camera

    public bool ToggleCamera()
    {
        return Keyboard.current.vKey.wasPressedThisFrame;
    
    }
}
