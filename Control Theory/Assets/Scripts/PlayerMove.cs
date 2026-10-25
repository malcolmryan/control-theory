/**
 * 
 * Author: Malcolm Ryan
 * Version: 1.0
 * For Unity Version: 6000.0.53f1
 */

using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMove : MonoBehaviour
{

#region Parameters
    [SerializeField, Unit(Units.DegreesPerSecond)] private float angularVelocity = Mathf.PI / 3;
#endregion 

#region Connected Objects
    [SerializeField, Required, ChildGameObjectsOnly] private WheelJoint2D wheel;
#endregion

#region Components
#endregion

#region State
    private Actions actions;
    private float moveInput = 0;
#endregion

#region Properties
#endregion

#region Events
#endregion

#region Init & Destroy
    void Awake()
    {
        actions = new Actions();
    }

    void OnEnable()
    {
        actions.Player.Enable();
    }

    void OnDisable()
    {
        actions.Player.Disable();
    }
#endregion 

#region Update
    void Update()
    {
        moveInput = actions.Player.Move.ReadValue<Vector2>().x;
    }
#endregion

#region FixedUpdate
    void FixedUpdate()
    {
        JointMotor2D motor = wheel.motor;
        motor.motorSpeed = moveInput * angularVelocity;
        wheel.motor = motor;
    }
#endregion

#region Gizmos
    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            // Don't run in the editor
            return;
        }
    }
#endregion
}
