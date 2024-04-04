using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeyHUD : MonoBehaviour
{
    [SerializeField] TMP_Text keyText;

    int keys = 0;

    public int Keys {
        get {
            return keys;
        }

        set {
            keys = value;
            UpdateHud();
        }
    }

    private void UpdateHud (){
        keyText.text = keys.ToString();
    }

}
