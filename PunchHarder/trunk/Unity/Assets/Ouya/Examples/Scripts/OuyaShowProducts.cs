using System;
using System.Collections.Generic;
using UnityEngine;

public class OuyaShowProducts : MonoBehaviour,
    OuyaSDK.IPauseListener, OuyaSDK.IResumeListener,
    OuyaSDK.IGetProductsListener, OuyaSDK.IPurchaseListener, OuyaSDK.IGetReceiptsListener,
    OuyaSDK.IMenuAppearingListener
{
    void Awake()
    {
        OuyaSDK.registerMenuAppearingListener(this);
        OuyaSDK.registerPauseListener(this);
        OuyaSDK.registerResumeListener(this);
        OuyaSDK.registerGetProductsListener(this);
        OuyaSDK.registerPurchaseListener(this);
        OuyaSDK.registerGetReceiptsListener(this);
    }
    void OnDestroy()
    {
        OuyaSDK.unregisterMenuAppearingListener(this);
        OuyaSDK.unregisterPauseListener(this);
        OuyaSDK.unregisterResumeListener(this);
        OuyaSDK.unregisterGetProductsListener(this);
        OuyaSDK.unregisterPurchaseListener(this);
        OuyaSDK.unregisterGetReceiptsListener(this);
    }

    public void OuyaMenuAppearing()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().ToString());
    }

    public void OuyaOnPause()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().ToString());
    }

    public void OuyaOnResume()
    {
        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().ToString());
    }

    public void OuyaGetProductsOnSuccess(List<OuyaSDK.Product> products)
    {
        m_products.Clear();
        foreach (OuyaSDK.Product product in products)
        {
            m_products.Add(product);
        }
    }

    public void OuyaGetProductsOnFailure(int errorCode, string errorMessage)
    {
        Debug.LogError(string.Format("OuyaGetProductsOnFailure: error={0} errorMessage={1}", errorCode, errorMessage));
    }

    public void OuyaPurchaseOnSuccess(OuyaSDK.Product product)
    {
        
    }

    public void OuyaPurchaseOnFailure(int errorCode, string errorMessage)
    {
        Debug.LogError(string.Format("OuyaPurchaseOnFailure: error={0} errorMessage={1}", errorCode, errorMessage));
    }

    public void OuyaGetReceiptsOnSuccess(List<OuyaSDK.Receipt> receipts)
    {
        m_receipts.Clear();
        foreach (OuyaSDK.Receipt receipt in receipts)
        {
            m_receipts.Add(receipt);
        }
    }

    public void OuyaGetReceiptsOnFailure(int errorCode, string errorMessage)
    {
        Debug.LogError(string.Format("OuyaGetReceiptsOnFailure: error={0} errorMessage={1}", errorCode, errorMessage));
    }

    #region Data containers

    private List<OuyaSDK.Product> m_products = new List<OuyaSDK.Product>();

    private List<OuyaSDK.Receipt> m_receipts = new List<OuyaSDK.Receipt>();

    #endregion

    #region Presentation

    private void OnGUI()
    {
        try
        {
            GUILayout.Label(string.Empty);
            GUILayout.Label(string.Empty);
            GUILayout.Label(string.Empty);
            GUILayout.Label(string.Empty);

            GUILayout.BeginHorizontal();
            GUILayout.Space(400);
            if (GUILayout.Button("Debug Initialize SDK", GUILayout.Height(40)))
            {
                OuyaSDK.initialize(OuyaGameObject.Singleton.DEVELOPER_ID,
                    OuyaGameObject.Singleton.UseLegacyInput);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(400);

            GUILayout.Label("GetProductList:");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(400);
            if (GUILayout.Button("Clear Get Product List", GUILayout.Height(40)))
            {
                OuyaSDK.OuyaJava.JavaClearGetProductList();
            }
            GUILayout.EndHorizontal();

            GUILayout.Label(string.Empty);

            GUILayout.BeginHorizontal();
            GUILayout.Space(400);
            if (GUILayout.Button("Debug Get Product List", GUILayout.Height(40)))
            {
                OuyaSDK.OuyaJava.JavaDebugGetProductList();
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(400);
            foreach (string productId in OuyaGameObject.Singleton.Purchasables)
            {
                if (GUILayout.Button(string.Format("Add: {0}", productId), GUILayout.Height(40)))
                {
                    OuyaSDK.Purchasable purchasable = new OuyaSDK.Purchasable(productId);
                    OuyaSDK.OuyaJava.JavaAddGetProduct(purchasable);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Label(string.Empty);

            GUILayout.BeginHorizontal();
            GUILayout.Space(400);
            GUILayout.Label("Products:");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(400);
            if (GUILayout.Button("Get Products", GUILayout.Height(40)))
            {
                List<OuyaSDK.Purchasable> productIdentifierList =
                    new List<OuyaSDK.Purchasable>();

                foreach (string productId in OuyaGameObject.Singleton.Purchasables)
                {
                    productIdentifierList.Add(new OuyaSDK.Purchasable(productId));
                }

                OuyaSDK.requestProductList(productIdentifierList);
            }
            GUILayout.EndHorizontal();

            foreach (OuyaSDK.Product product in m_products)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(400);

                GUILayout.Label(string.Format("Name={0}", product.getName()));
                GUILayout.Label(string.Format("Price={0}", product.getPriceInCents()));
                GUILayout.Label(string.Format("Identifier={0}", product.getIdentifier()));

                if (GUILayout.Button("Purchase"))
                {
                    Debug.Log(string.Format("Purchase Identifier: {0}", product.getIdentifier()));
                    OuyaSDK.requestPurchase(product.getIdentifier());
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.Label(string.Empty);

            GUILayout.BeginHorizontal();
            GUILayout.Space(400);
            GUILayout.Label("Receipts:");
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Space(400);
            if (GUILayout.Button("Get Receipts", GUILayout.Height(40)))
            {
                OuyaSDK.requestReceiptList();
            }
            GUILayout.EndHorizontal();
        }
        catch (System.Exception)
        {
        }
    }

    #endregion
}