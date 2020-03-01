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
       
        private bool _processed = false;
        bool enabled = false;

        public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        {
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKey(KeyCode.F5))
            {
                Debug.Log("F5 Pressed (CGW Netreplacer");
                NetReplacePanel.instance.Show();
            }
            else
            {
                // not both keys pressed: Reset processed state
                _processed = false;
            }
        }

    }
}