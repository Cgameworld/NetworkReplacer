using ColossalFramework;
using ColossalFramework.Math;
using ColossalFramework.UI;
using ICities;
using NetworkReplacer.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkReplacer
{
    public class ModLoading : LoadingExtensionBase
    {
        UIButton button;

        public override void OnLevelLoaded(LoadMode mode)
        {
            NetReplacePanel.instance.Show();
            button = NetReplacerButton.CreateButton();
            Debug.Log("CGW Net replacer loaded!");
        }

        //maybe add apply only in tile..

        //select network one then type in new network in search field?
        //right click on upgrade road tool, turns red select new road..
        
        //upgrade to railway with one click?


        //note that too many presses might lead to bad results use with caution
        //might hit segment limit check with other mod,
        //back up saves before using this mod! as with any other mod

        //use cases

        //warning regen takes a while!!!!!! new vechicles need to spawn


        //replace all 3L highway with 2L outisde of map more realistic!
        //replace all rails with railway


        
    }
}