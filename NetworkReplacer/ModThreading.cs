using ColossalFramework.IO;
using ColossalFramework.UI;
using ICities;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace NetworkReplacer
{
    public class ModThreading : ThreadingExtensionBase
    {      
        bool processed = false;

        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            if (Input.GetKey(KeyCode.F5))
            {
                if (processed == false)
                {
                    
                    Debug.Log("F5 Pressed (CGW Netreplacer");
                    if (NetReplacePanel.instance.isVisible == false)
                    {
                        NetReplacePanel.instance.Show();
                    }
                    else
                    {
                        NetReplacePanel.instance.Hide();
                    } 
                    processed = true;
                    Debug.Log("is visible? " + NetReplacePanel.instance.isVisible);
                }
            }
            else
            {
                processed = false;
            }
        }

    }
}