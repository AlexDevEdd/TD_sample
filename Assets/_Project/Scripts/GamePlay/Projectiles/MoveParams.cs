using UnityEngine;

namespace GamePlay
{
    public struct MoveParams
    {
        public Rigidbody Rigidbody { get; }
        public float Speed { get; }
        public Vector3 Velocity { get; private set; }
        public Transform Target { get; private set; }
        
        public MoveParams(Rigidbody rigidbody, float speed)
        {
            this = new MoveParams();
            Rigidbody = rigidbody;
            Speed = speed;
        }
        
        public void SetVelocity(Vector3 velocity)
        {
            Velocity = velocity;
        }
        
        public void SetTarget(Transform target)
        {
            Target = target;
        }
    }
}