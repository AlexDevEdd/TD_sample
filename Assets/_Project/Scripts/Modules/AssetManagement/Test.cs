using UnityEngine;

namespace AssetManagement
{
    public class Test
    {
        
    }
    

    public class AssetConverter : MonoBehaviour
    {
        // public void ConvertToAddressable(string filePath, string addressKey)
        // {
        //     AsyncOperationHandle<IAssetBundle> opHandle = Addressables.LoadAssetAsync<IAssetBundle>(filePath);
        //
        //     opHandle.Completed += (asyncOp) =>
        //     {
        //         if (asyncOp.Status == AsyncOperationStatus.Succeeded)
        //         {
        //             IAssetBundle assetBundle = opHandle.Result;
        //         
        //             // Создаем новый Addressable asset
        //             AsyncOperationHandle<AssetReference> refHandle = Addressables.CreateAssetAsync(assetBundle, addressKey);
        //         
        //             refHandle.Completed += (refOp) =>
        //             {
        //                 if (refOp.Status == AsyncOperationStatus.Succeeded)
        //                 {
        //                     Debug.Log($"Ассет создан с ключом: {addressKey}");
        //                 }
        //             };
        //         }
        //     };
        // }
    }
    
    

    internal class FileLoader : MonoBehaviour
    {
        // public void DownloadFile(string url, string filePath)
        // {
        //     using (UnityWebRequest www = UnityWebRequest.Get(url))
        //     {
        //         yield return www.SendWebRequest();
        //
        //         if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        //         {
        //             Debug.LogError("Ошибка загрузки: " + www.error);
        //         }
        //         else
        //         {
        //             byte[] bytes = www.downloadHandler.data;
        //             File.WriteAllBytes(filePath, bytes);
        //             Debug.Log("Файл успешно сохранен: " + filePath);
        //         }
        //     }
        // }
    }
}