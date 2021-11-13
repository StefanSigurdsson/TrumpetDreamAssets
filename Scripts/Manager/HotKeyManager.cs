using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotKeyManager : MonoBehaviour
{
    KeyCode[] keyNameArray = { KeyCode.S };
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            CheckKeys();
        }
    }

    private KeyCode WhichKeyIsDown()
    {
        foreach (KeyCode key in keyNameArray)
        {
            if (Input.GetKeyDown(key))
            {
                return key;
            }
        }
        return KeyCode.None;
    }

    private void CheckKeys()
    {
        switch (WhichKeyIsDown())
        {
            case KeyCode.S:
                ShopManager.Instance.ToggleActivateShop();
                break;
            default:
                break;

        }
    }
}
