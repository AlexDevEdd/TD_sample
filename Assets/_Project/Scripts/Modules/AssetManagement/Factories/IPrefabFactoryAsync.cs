using Cysharp.Threading.Tasks;

namespace AssetManagement
{
    public interface IPrefabFactoryAsync
    {
        public UniTask<TComponent> Create<TComponent>(string assetKey);
    }
}