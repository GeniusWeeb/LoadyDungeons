using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// Used for the Hat selection logic
public class PlayerConfigurator : MonoBehaviour
{
    [SerializeField]
    private Transform m_HatAnchor;

    private ResourceRequest m_HatLoadingRequest;
    private AsyncOperationHandle<GameObject> m_hatLoadOperationnHandle;
    

    [SerializeField] private AssetReferenceGameObject m_address_ref;
    

    void Start()
    {           
        SetHat(string.Format("Hat{0:00}", GameManager.s_ActiveHat));
    }

    public void SetHat(string hatKey)
    {

        if (!m_address_ref.RuntimeKeyIsValid()) return;

        m_hatLoadOperationnHandle = m_address_ref.LoadAssetAsync<GameObject>();
        m_hatLoadOperationnHandle.Completed += OnHatLoadComplete;
    }

    private void OnHatLoaded(AsyncOperation asyncOperation)
    {
        Instantiate(m_HatLoadingRequest.asset as GameObject, m_HatAnchor, false);
    }

    private void OnDisable()
    {
       
        m_hatLoadOperationnHandle.Completed -= OnHatLoadComplete;

        
    }
    
    private void OnHatLoadComplete(AsyncOperationHandle<GameObject> obj)
    {

        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(obj.Result, m_HatAnchor);
        }
    }

 
}
