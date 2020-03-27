﻿using ColossalFramework.UI;
using System;
using UnityEngine;

// from Pedestrian Bridge Builder, Crossings and Roundabout Builder

namespace NetworkReplacer.UI
{
    public class NetReplacerButton : UIButton
    {

        private const float BUTTON_HORIZONTAL_POSITION = 130;
        const int SIZE = 31;
        const string NetReplacerButtonBg = "NetReplacerButtonBg";
        const string NetReplacerButtonBgPressed = "NetReplacerButtonBgPressed";
        const string NetReplacerButtonBgHovered = "NetReplacerButtonBgHovered";
        const string NetReplacerIcon = "NetReplacerIcon";
        const string NetReplacerIconPressed = "NetReplacerIconPressed";

        bool clickClose = false;

        public override void Start()
        {
            name = "NetReplaceButton";
            playAudioEvents = true;
            tooltip = "Network Replacer";

            var roadsOptionPanel = UIUtils.Instance.FindComponent<UIComponent>("RoadsOptionPanel", null, UIUtils.FindOptions.NameContains);
            var builtinTabstrip = UIUtils.Instance.FindComponent<UITabstrip>("ToolMode", roadsOptionPanel, UIUtils.FindOptions.None);
            UIButton uibutton = (UIButton)builtinTabstrip.tabs[0];

            string[] spriteNames = new string[]
            {
                NetReplacerButtonBg,
                NetReplacerButtonBgPressed,
                NetReplacerButtonBgHovered,
                NetReplacerIcon,
                NetReplacerIconPressed
            };
            var atlas = ResourceLoader.GetAtlas("NetReplacerUI");

            if (atlas == UIView.GetAView().defaultAtlas)
            {
                atlas = ResourceLoader.CreateTextureAtlas("sprites.png", "NetReplacerUI", uibutton.atlas.material, SIZE, SIZE, spriteNames);
            }
            this.atlas = atlas;

            normalBgSprite = focusedBgSprite = disabledBgSprite = NetReplacerButtonBg;
            hoveredBgSprite = NetReplacerButtonBgHovered;
            pressedBgSprite = NetReplacerButtonBgPressed;

            normalFgSprite = disabledFgSprite = hoveredFgSprite = pressedFgSprite = NetReplacerIcon;
            focusedFgSprite = NetReplacerIcon;

            relativePosition = new Vector3(BUTTON_HORIZONTAL_POSITION, 38f); //RABOUT:23,38     Crossings:94,38
            size = new Vector2(SIZE, SIZE);
        }

        public static UIButton CreateButton()
        {
            Debug.Log("create button NR called");
            var roadsOptionPanel = UIUtils.Instance.FindComponent<UIComponent>("RoadsOptionPanel", null, UIUtils.FindOptions.NameContains);
            return roadsOptionPanel?.AddUIComponent<NetReplacerButton>();
        }

        protected override void OnClick(UIMouseEventParameter p)
        {
            base.OnClick(p);
            

            if (clickClose == false)
            {
                NetReplacePanel.instance.Show();
                clickClose = true;
            }
            else if (clickClose == true)
            {
                NetReplacePanel.instance.Hide();
                clickClose = false;
            }

        }
    }


    public class UIUtils
    {
        // Token: 0x17000006 RID: 6
        // (get) Token: 0x06000038 RID: 56 RVA: 0x00004CF0 File Offset: 0x00002EF0
        public static UIUtils Instance
        {
            get
            {
                bool flag = UIUtils.instance == null;
                if (flag)
                {
                    UIUtils.instance = new UIUtils();
                }
                return UIUtils.instance;
            }
        }

        // Token: 0x06000039 RID: 57 RVA: 0x00004D20 File Offset: 0x00002F20
        private void FindUIRoot()
        {
            this.uiRoot = null;
            foreach (UIView uiview in UnityEngine.Object.FindObjectsOfType<UIView>())
            {
                bool flag = uiview.transform.parent == null && uiview.name == "UIView";
                if (flag)
                {
                    this.uiRoot = uiview;
                    break;
                }
            }
        }

        // Token: 0x0600003A RID: 58 RVA: 0x00004D84 File Offset: 0x00002F84
        public string GetTransformPath(Transform transform)
        {
            string text = transform.name;
            Transform parent = transform.parent;
            while (parent != null)
            {
                text = parent.name + "/" + text;
                parent = parent.parent;
            }
            return text;
        }

        // Token: 0x0600003B RID: 59 RVA: 0x00004DD0 File Offset: 0x00002FD0
        public T FindComponent<T>(string name, UIComponent parent = null, UIUtils.FindOptions options = UIUtils.FindOptions.None) where T : UIComponent
        {
            bool flag = this.uiRoot == null;
            if (flag)
            {
                this.FindUIRoot();
                bool flag2 = this.uiRoot == null;
                if (flag2)
                {
                    return default(T);
                }
            }
            foreach (T t in UnityEngine.Object.FindObjectsOfType<T>())
            {
                bool flag3 = (options & UIUtils.FindOptions.NameContains) > UIUtils.FindOptions.None;
                bool flag4;
                if (flag3)
                {
                    flag4 = t.name.Contains(name);
                }
                else
                {
                    flag4 = (t.name == name);
                }
                bool flag5 = !flag4;
                if (!flag5)
                {
                    bool flag6 = parent != null;
                    Transform transform;
                    if (flag6)
                    {
                        transform = parent.transform;
                    }
                    else
                    {
                        transform = this.uiRoot.transform;
                    }
                    Transform parent2 = t.transform.parent;
                    while (parent2 != null && parent2 != transform)
                    {
                        parent2 = parent2.parent;
                    }
                    bool flag7 = parent2 == null;
                    if (!flag7)
                    {
                        return t;
                    }
                }
            }
            return default(T);
        }

        // Token: 0x04000024 RID: 36
        private static UIUtils instance = null;

        // Token: 0x04000025 RID: 37
        private UIView uiRoot = null;

        // Token: 0x02000010 RID: 16
        [Flags]
        public enum FindOptions
        {
            // Token: 0x04000034 RID: 52
            None = 0,
            // Token: 0x04000035 RID: 53
            NameContains = 1
        }
    }
}
