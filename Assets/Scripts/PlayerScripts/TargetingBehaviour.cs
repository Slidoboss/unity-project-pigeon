using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class TargetingBehaviour : MonoBehaviour
{
    private Vector2 _mousePositionIn2dWorld;
    private Vector2 _playerPosition;
    private Transform _playerTransfrom;
    private Transform _leftArmSolverTransform;
    private GameObject _aimingArm;
    private GameObject _regularArm;
    private PlayerInputHandler inputHandler;
    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        _leftArmSolverTransform = GameObject.Find("GunArmSolver_Target").GetComponent<Transform>();
        _aimingArm = GameObject.Find("Gun arm");
        _regularArm = GameObject.Find("Left arm");
        _regularArm.SetActive(true);
        _aimingArm.SetActive(false);
        _playerTransfrom = GameObject.Find("P I Jenkins").GetComponent<Transform>();
        _playerPosition = _playerTransfrom.position;
    }
    // Update is called once per frame
    void Update()
    {
        Targeting();
    }
    public void Targeting()
    {
        if (inputHandler.Aim)
        {
            _aimingArm.SetActive(true);
            _regularArm.SetActive(false);
            _mousePositionIn2dWorld = inputHandler.Target;
            float offsetAmount = -0.8f;
            Vector2 offsetedMousePosition = new Vector2(_mousePositionIn2dWorld.x - offsetAmount, _mousePositionIn2dWorld.y + offsetAmount);
            _leftArmSolverTransform.position = offsetedMousePosition;
           // Vector2 direction = (_mousePositionIn2dWorld - _playerPosition).normalized;
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Debug.Log("Direction:" + direction.x);
        }
        else
        {
            _aimingArm.SetActive(false);
            _regularArm.SetActive(true);
        }
    }
}
