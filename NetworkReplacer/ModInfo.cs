using ColossalFramework.UI;
using ICities;
using System;
using UnityEngine;

namespace NetworkReplacer
{
    public class ModInfo : IUserMod
    {
        public string Name
        {
            get { return "Network Replacer"; }
        }

        public string Description
        {
            get { return "Mod Description"; }
        }
    }
}