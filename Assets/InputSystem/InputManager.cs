using UnityEngine;
#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

public class InputManager : MonoBehaviour
{
	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 look;
	public bool jump;
	public bool sprint;
	public bool interact;
	public bool crouch;
	public bool ads;
	public bool actionTrigger;
	public bool pause;
	public bool start;

	[Header("Movement Settings")]
	public bool analogMovement;
	public bool toggleCrouch;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked; 
	public bool cursorInputForLook;

	public static InputManager instance;

    void Awake()
    {
        instance = this;
    }

#if ENABLE_INPUT_SYSTEM 
    public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnLook(InputValue value)
	{
		if(cursorInputForLook)
		{
			LookInput(value.Get<Vector2>());
		}
	}

	public void OnJump(InputValue value)
	{
		JumpInput(value.isPressed);
	}

	public void OnSprint(InputValue value)
	{
		SprintInput(value.isPressed);
	}

	public void OnInteract(InputValue value)
    {
		InteractInput(value.isPressed);
    }

	public void OnCrouch(InputValue value)
    {
		if (!toggleCrouch)
		{
			CrouchInput(value.isPressed);
		}
		else if (toggleCrouch)
		{
			ToggleCrouch(value.isPressed);
		}
    }

	public void OnADS(InputValue value)
    {
		ADSInput(value.isPressed);
    }

	public void OnActionTrigger(InputValue value)
    {
		ActionTriggerInput(value.isPressed);
    }

	public void OnPause()
	{
		PauseInput();
	}

	public void OnStart()
	{
		StartInput();
	}


#endif


	public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	} 

	public void LookInput(Vector2 newLookDirection)
	{
		look = newLookDirection;
	}

	public void JumpInput(bool newJumpState)
	{
		jump = newJumpState;
	}

	public void SprintInput(bool newSprintState)
	{ 
		sprint = newSprintState;
	}

	public void InteractInput(bool newInteractState)
    {
		interact = newInteractState;
    }

	public void CrouchInput(bool newCrouchState)
	{
		crouch = newCrouchState;
	}

	public void ToggleCrouch(bool newCrouchState)
    {
		crouch = !crouch;
    }

	public void ADSInput(bool newADSState)
    {
		ads = newADSState;
    }

	public void ActionTriggerInput(bool newADSState)
	{
		actionTrigger = newADSState;
	}

	public void PauseInput()
	{
		pause = !pause;
	}

	public void StartInput()
	{
		start = !start;
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	public void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None; 
	}
}
	