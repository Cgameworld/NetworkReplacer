using ColossalFramework;
using ColossalFramework.Math;
using ColossalFramework.UI;
using ICities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkReplacer
{
    public class ModLoading : LoadingExtensionBase
    {
        public override void OnLevelLoaded(LoadMode mode)
        {
            NetReplacePanel.instance.Start();
            Debug.Log("CGW Net replacer loaded!");     
        }

        //select network one then type in new network in search field?
        //right click on upgrade road tool, turns red select new road.. 
    }
}