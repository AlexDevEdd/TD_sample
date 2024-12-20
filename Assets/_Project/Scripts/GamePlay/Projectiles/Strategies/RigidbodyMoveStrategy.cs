using UnityEngine;

namespace GamePlay
{
    public sealed class RigidbodyMoveStrategy : MoveStrategy
    {
        public override void Move(in MoveParams moveParams)
        {
            moveParams.Rigidbody.velocity =  moveParams.Velocity;
        }
    }
}