using JetBrains.Annotations;

namespace GamePlay
{
    [UsedImplicitly]
    public sealed class TowerProvider
    {
        public IEntity Value { get; }

        public TowerProvider(IEntity value)
        {
            Value = value;
        }
    }
}