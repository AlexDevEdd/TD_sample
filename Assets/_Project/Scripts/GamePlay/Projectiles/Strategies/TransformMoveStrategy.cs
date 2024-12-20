using UnityEngine;
using Utils;

namespace GamePlay
{
    public sealed class TransformMoveStrategy : MoveStrategy
    {
        private Vector3 _lastDirection;
        
        public override void Move(in MoveParams moveParams)
        {
            if(moveParams.Target == null)
                return;

            if (!moveParams.Target.IsActive())
                MoveToTarget(moveParams, _lastDirection);
            else
            {
                _lastDirection = (moveParams.Target.position - moveParams.Rigidbody.position).normalized;
                MoveToTarget(moveParams, _lastDirection);
            }
           
        }

        private static void MoveToTarget(in MoveParams moveParams,Vector3 direction)
        {
            moveParams.Rigidbody.transform.Translate(direction * (moveParams.Speed * Time.deltaTime));
        }
    }
    
    
    
}