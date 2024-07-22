using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using Vector2 = UnityEngine.Vector2;

public class PlayerInputHandler : MonoBehaviour
{

    #region VARIABLE DECLARATIONS
    private Vector2 move;
    private Vector2 target;
    private bool jump;
    private bool hop;
    private bool aim;
    #endregion

    #region PROPERTIES
    public Vector2 Move
    {
        get { return move; }
    }
    public bool Jump
    {
        get { return jump; }
    }
    public bool Hop
    {
        get { return hop; }
    }
    public bool Aim
    {
        get { return aim; }
    }
    public Vector2 Target
    {
        get { return target; }
    }
    #endregion

    #region INPUTSYSTEM DECLARATIONS
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _aimAction;
    private InputAction _targetAction;
    // Start is called before the first frame update
    #endregion

    private PlayerBehaviour player;
    private new Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        player = GetComponent<PlayerBehaviour>();
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _targetAction = _playerInput.actions["Target"];
        _jumpAction = _playerInput.actions["Jump"];
        _aimAction = _playerInput.actions["Aim"];
    }
    private void OnEnable()
    {
        //Pairs the mouse to the user, to allow them use the PlayerInput component with the Event System and the mouse.
        InputUser.PerformPairingWithDevice(Mouse.current,_playerInput.user);
        _jumpAction.performed += JumpInput;
        _jumpAction.canceled += JumpInput;
        _jumpAction.performed += player.JumpSystem;
        _jumpAction.canceled += player.JumpSystem;
    }

    private void OnDisable()
    {
        _jumpAction.performed -= JumpInput;
        _jumpAction.canceled -= JumpInput;
        _jumpAction.performed += player.JumpSystem;
        _jumpAction.canceled += player.JumpSystem;
    }
    // Update is called once per frame
    void Update()
    {
        OnTarget();
        OnMove();
        OnAim();
    }

    private void OnMove()
    {
        move = _moveAction.ReadValue<Vector2>();
    }
    private void OnAim()
    {
        if (_aimAction.IsPressed())
        {
            aim = true;
        }
        else
        {
            aim = false;
        }
    }
    private void OnJump(bool jumpState, bool hopState)
    {
        jump = jumpState;
        hop = hopState;
    }
    private void OnTarget()
    {
        Vector2 mousePosOnScreen = _targetAction.ReadValue<Vector2>();
        target = camera.ScreenToWorldPoint(mousePosOnScreen);
    }
    private void JumpInput(InputAction.CallbackContext context)
    {
        OnJump(context.performed, context.canceled);
    }
}
