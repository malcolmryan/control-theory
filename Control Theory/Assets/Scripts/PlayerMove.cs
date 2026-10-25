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
#region Structs
    [System.Serializable]
    public struct Gain
    {
        public float wheelAngularVelocity;
        public float poleAngle;
        public float poleAngularVelocity;
    }
#endregion 


#region Parameters
    [SerializeField, Unit(Units.MetersPerSecond)] private float speed = 10;
    [SerializeField, Unit(Units.NewtonMeter)] private float maxTorque = 10;
    [SerializeField] private Gain gain;
#endregion 

#region Connected Objects
    [SerializeField, Required, ChildGameObjectsOnly] private Rigidbody2D wheel;
    [SerializeField, Required, ChildGameObjectsOnly] private Rigidbody2D body;
#endregion

#region Components
#endregion

#region State
    private HingeJoint2D wheelJoint;
    private CircleCollider2D wheelCollider;
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
        wheelCollider = wheel.GetComponent<CircleCollider2D>(throwIfNull : true);
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
        float targetWheelAngularVelocity = wheelCollider.radius * speed * -moveInput;
        Vector2 targetPoleDirection = Vector2.up;
        float targetPoleAngularVelocity = 0;

        float dWheelAngularVelocity = targetWheelAngularVelocity - wheel.angularVelocity * Mathf.Deg2Rad;

        float dPoleDirection = PerpDot(targetPoleDirection, body.transform.up);
        float dPoleAngularVelocity = targetPoleAngularVelocity - body.angularVelocity * Mathf.Deg2Rad;
        Debug.Log($"dPoleAngularVelocity = {targetPoleAngularVelocity} - {body.angularVelocity * Mathf.Deg2Rad} = {dPoleAngularVelocity}");

        float torque = gain.wheelAngularVelocity * dWheelAngularVelocity +
            gain.poleAngle * dPoleDirection +
            gain.poleAngularVelocity * dPoleAngularVelocity;

        wheel.AddTorque(torque);
        body.AddTorque(-torque);
    }
#endregion

#region Private methods

    private float PerpDot(Vector2 a, Vector2 b)
    {
        return a.y * b.x - a.x * b.y;   // perp(a) . b = |a||b| sin \theta

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
