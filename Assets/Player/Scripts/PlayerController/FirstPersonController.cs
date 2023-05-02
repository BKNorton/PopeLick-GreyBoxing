using System.Collections;
using UnityEngine;
using Cinemachine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
public class FirstPersonController : MonoBehaviour
{
	[Header("Player")]
	[Tooltip("Move speed of the character in m/s")]
	[SerializeField] private float MoveSpeed;
	[Tooltip("Sprint speed of the character in m/s")]
	[SerializeField] private float SprintSpeed;
	[Tooltip("Crouch speed of the character in m/s")]
	[SerializeField] private float CrouchSpeed;
	[Tooltip("Rotation speed of the character")]
	[SerializeField] private float RotationSpeed;
	[Tooltip("Acceleration and deceleration")]
	[SerializeField] private float SpeedChangeRate;

	[Space(10)]
	[Tooltip("The height the player can jump")]
	[SerializeField] private float JumpHeight;
	[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
	[SerializeField] private float Gravity;

	[Space(10)]
	[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
	[SerializeField] private float JumpTimeout = 0.1f;
	[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
	[SerializeField] private float FallTimeout = 0.15f;

	[Header("Player Grounded")]
	[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
	[SerializeField] private bool Grounded = true;
	[Tooltip("Useful for rough ground")]
	[SerializeField] private float GroundedOffset = -0.14f;
	[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
	[SerializeField] private float GroundedRadius = 0.5f;
	[Tooltip("What layers the character uses as ground")]
	[SerializeField] private LayerMask GroundLayers;

	[Header("Player Zoom")]
	[Tooltip("Time it takes player to ADS")]
	[SerializeField] private float TimeToZoom;
	[SerializeField] private float ZoomFOV;
	[SerializeField] private float DefaultFOV;
	private Coroutine zoomRoutine;

	[Header("Player QuickTurn")]
	[Tooltip("Time it takes player to QuickTurn")]
	[SerializeField] private float TimeToTurn;
	[SerializeField] private float TurnRotation;
	[SerializeField] private float QuickTurnCoolDown;
	private Coroutine quickTurnRoutine;


	[Header("Cinemachine")]
	[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
	[SerializeField] private GameObject CinemachineCameraTarget;
	[Tooltip("Cinemachine")]
	[SerializeField] private CinemachineVirtualCamera Cinemachine;
	[Tooltip("How far in degrees can you move the camera up")]
	[SerializeField] private float TopClamp = 90.0f;
	[Tooltip("How far in degrees can you move the camera down")]
	[SerializeField] private float BottomClamp = -90.0f;

	[Header("Animatioins")]
	[SerializeField] private Animator animator;

	// cinemachine
	private float _cinemachineTargetPitch;

	// player
	private PlayerUI _playerUI;
	private float _speed;
	private float _rotationVelocity;
	private float _verticalVelocity;
	private float _terminalVelocity = 53.0f;
	private float targetSpeed;
	private float currentHorizontalSpeed;
	private float speedOffset = 0.1f;
	private float inputMagnitude;
	private float lastInputMagnitude;
	private Vector3 inputDirection;
	private Vector3 lastInputDirection;
	private float currentRotation;
	private float targetRotation;
	public float lastQuickTurn = 0f;
	public float quickTurnTime;
	private bool canQuickTurn;

	// timeout deltatime
	private float _jumpTimeoutDelta;
	private float _fallTimeoutDelta;

	
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	private PlayerInput _playerInput;
#endif
    private CharacterController _controller;
    private InputManager _input;
    private GameObject _mainCamera;
    private const float _threshold = 0.01f;
    
    private bool IsCurrentDeviceMouse
    {
    	get
    	{
    		#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    		return _playerInput.currentControlScheme == "KeyboardMouse";
    		#else
    		return false;
    		#endif
    	}
    }
    
    private void Awake()
    {
    	// get a reference to our main camera
    	if (_mainCamera == null)
    	{
    		_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    	}
		
    }
    
    private void Start()
    {
		_controller = GetComponent<CharacterController>();
		_input = InputManager.instance;
		_playerUI = GetComponent<PlayerUI>();
		DefaultFOV = Cinemachine.m_Lens.FieldOfView;
		
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		_playerInput = GetComponent<PlayerInput>();

#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

		// reset our timeouts on start
		_jumpTimeoutDelta = JumpTimeout;
		_fallTimeoutDelta = FallTimeout;
	}

	private void Update()
	{
		GroundedCheck();
		JumpAndGravity();
		Crouch();
		Move();
		QuickTurn(); 
		MoveAnimation();
	}

	private void LateUpdate()
	{
		ADS();
		CameraRotation();
	}

	private void GroundedCheck()
	{
		// set sphere position, with offset
		Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
		Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
	}

	private void CameraRotation()
	{
		// if there is an input
		if (_input.look.sqrMagnitude >= _threshold)
		{
			//Don't multiply mouse input by Time.deltaTime
			float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
			if (_input.pause || _input.start) deltaTimeMultiplier = 0;
			
			_cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
			_rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

			// clamp our pitch rotation
			_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

			// Update Cinemachine camera target pitch
			CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

			// rotate the player left and right
			transform.Rotate(Vector3.up * _rotationVelocity);
		}

	}

	private void Move()
	{
		// set target speed based on move speed, sprint speed and if sprint is pressed, crouch speed and if crouch is pressed
		if (_input.sprint)
		{
			targetSpeed = SprintSpeed;
		}
		else if (_input.crouch)
		{
			targetSpeed = CrouchSpeed;
		}
		else
		{
			targetSpeed = MoveSpeed;
		}
		if (!Grounded)
		{
			targetSpeed = currentHorizontalSpeed;
		}

		// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon
		// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is no input, set the target speed to 0
		if (_input.move == Vector2.zero && Grounded) targetSpeed = 0.0f;

		// a reference to the players current horizontal velocity
		currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

		// Check players magnitude (if player is not grounded use last magnitude recorded)
		if (Grounded)
		{
			inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;
			lastInputMagnitude = inputMagnitude;
		}
		else inputMagnitude = lastInputMagnitude;
		

		// accelerate or decelerate to target speed
		if(currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
		{
			// creates curved result rather than a linear one giving a more organic speed change
			// note T in Lerp is clamped, so we don't need to clamp our speed
			_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

			// round speed to 3 decimal places
			_speed = Mathf.Round(_speed * 1000f) / 1000f;
		}


		// Check players direction (if player is not grounded use last direction recorded) and normalise input direction
		if (Grounded)
		{
			inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			// move
			inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
			lastInputDirection = inputDirection;
		}
		else inputDirection = lastInputDirection;

		

		// move the player
		_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
	}

	private void JumpAndGravity()
	{
		if (Grounded)
		{
			// reset the fall timeout timer
			_fallTimeoutDelta = FallTimeout;

			// stop our velocity dropping infinitely when grounded
			if (_verticalVelocity < 0.0f)
			{
				_verticalVelocity = -2f;
			}

			// Jump
			if (_input.jump && _jumpTimeoutDelta <= 0.0f)
			{
				// the square root of H * -2 * G = how much velocity needed to reach desired height
				_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
			}

			// jump timeout
			if (_jumpTimeoutDelta >= 0.0f)
			{
				_jumpTimeoutDelta -= Time.deltaTime;
			}
		}
		else
		{
			// reset the jump timeout timer
			_jumpTimeoutDelta = JumpTimeout;

			// fall timeout
			if (_fallTimeoutDelta >= 0.0f)
			{
				_fallTimeoutDelta -= Time.deltaTime;
			}

			// if we are not grounded, do not jump
			_input.jump = false;
		}

		// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
		if (_verticalVelocity < _terminalVelocity)
		{
			_verticalVelocity += Gravity * Time.deltaTime;
		}
	}

	public void Crouch()
	{
		animator.SetBool("IsCrouching", _input.crouch);
	}

	public void ADS()
    {
		if (_input.ads)
        {
			//_playerUI.CameraUIEnable();
			//if (zoomRoutine != null)
			//{
			//StopCoroutine(zoomRoutine);
			//zoomRoutine = null;

			//}
			_playerUI.CameraUIEnable();
			zoomRoutine = StartCoroutine(ToggleZoom(true));
		}
		else
		{
			if (zoomRoutine != null)
			{
				StopCoroutine(zoomRoutine);
				zoomRoutine = null;
			}
			zoomRoutine = StartCoroutine(ToggleZoom(false));
		}
	}

	public void QuickTurn()
    {
		if (!Grounded)
		{
			canQuickTurn = false;
			return;
		}
		if ((_input.move.y < 0) && _input.sprint)
        {
			// Find current character controller rotation before the Quickturn only
			// Use current rotation to find target rotation
			if (quickTurnRoutine == null)
			{
				currentRotation = _controller.transform.eulerAngles.y;
				targetRotation = currentRotation + TurnRotation;
			}

			// Check for cooldown
			quickTurnTime = Time.time;
			if (quickTurnTime - lastQuickTurn > QuickTurnCoolDown)
			{
				canQuickTurn = true;
				quickTurnRoutine = StartCoroutine(ToggleQuickTurn(currentRotation, targetRotation));
			}
			lastQuickTurn = Time.time;
        }
		else canQuickTurn = false;
    }

	private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
	{
		if (lfAngle < -360f) lfAngle += 360f;
		if (lfAngle > 360f) lfAngle -= 360f;
		return Mathf.Clamp(lfAngle, lfMin, lfMax);
	}

	private void OnDrawGizmosSelected()
	{
		Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
		Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

		if (Grounded) Gizmos.color = transparentGreen;
		else Gizmos.color = transparentRed;

		// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
		Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
	}

	public void MoveAnimation()
	{
		animator.SetFloat("MoveSpeed", _input.move.magnitude);
		animator.SetBool("OnGround", Grounded);
		animator.SetBool("IsSprinting", _input.sprint);
		animator.SetBool("QuickTurn", canQuickTurn);
	}

	private IEnumerator ToggleZoom(bool isEnter)
    {
		float targetFOV = isEnter ? ZoomFOV : DefaultFOV;
		float startingFOV = Cinemachine.m_Lens.FieldOfView;
		float timeElapsed = 0;

		while (timeElapsed < TimeToZoom)
		{ 
			//hide camera display so it doesnt apear on picture
			if (_input.actionTrigger)
			{
				_playerUI.CameraUIDisable();
				_playerUI.HideCrosshair();
			}
			Cinemachine.m_Lens.FieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed / TimeToZoom);
			timeElapsed += Time.deltaTime;
			yield return null;
        }

		Cinemachine.m_Lens.FieldOfView = targetFOV;
		zoomRoutine = null;
    }

	private IEnumerator ToggleQuickTurn(float currentRotation, float targetRotation)
    {
		float timeElapsed = 0;
	
		while (timeElapsed < TimeToTurn)
        {
			_controller.transform.localRotation = Quaternion.Euler(0, Mathf.Lerp(currentRotation, targetRotation, timeElapsed / TimeToTurn), 0);
			timeElapsed += Time.deltaTime;
			yield return null;
        }
		_controller.transform.localRotation = Quaternion.Euler(0, targetRotation, 0);
		quickTurnRoutine = null;
    }
}
