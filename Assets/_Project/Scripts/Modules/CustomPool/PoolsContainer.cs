using UnityEngine;

namespace CustomPool
{
    public sealed class PoolsContainer : MonoBehaviour, IPoolsContainer
    {
       public Transform Container => transform;
    }
}