using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeToUIManager : MonoBehaviour
{
    public UIManager uIManager;
    public void SetPlayerUIHealthProgressBar(float aRate) {
        uIManager.SetHealthProgressbar(aRate);
    }
}
