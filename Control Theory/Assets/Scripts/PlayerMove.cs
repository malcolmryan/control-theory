/**
 * 
 * Author: Malcolm Ryan
 * Version: 1.0
 * For Unity Version: 6000.0.53f1
 */

using UnityEngine;
using Sirenix.OdinInspector;
using WordsOnPlay.Utils;

public class PlayerMove : MonoBehaviour
{

#region Parameters
    [SerializeField, Unit(Units.NewtonMeter)] private float maxTorque = 1;
#endregion 

#region Connected Objects
    [SerializeField, Required, ChildGameObjectsOnly] private Rigidbody2D wheel;
    [SerializeField, Required, ChildGameObjectsOnly] private Rigidbody2D body;
#endregion

#region Components
#endregion

#region State
    private HingeJoint2D wheelJoint;
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

        wheelJoint = wheel.GetComponent<HingeJoint2D>(throwIfNull : true);        
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
        wheel.AddTorque(maxTorque * moveInput);
        body.AddTorque(-maxTorque * moveInput);
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
