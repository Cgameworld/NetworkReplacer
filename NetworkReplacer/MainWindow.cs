using ColossalFramework.UI;
using ColossalFramework;
using UnityEngine;
using NetworkReplacer;

namespace NetworkReplacer
{
    public class NetReplacePanel : UIPanel
    {
        private UITitleBar m_title;

        private static NetReplacePanel _instance;
        private UIButton getIdsButton;
        private UIButton replaceRoad;

        public static NetReplacePanel instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = UIView.GetAView().AddUIComponent(typeof(NetReplacePanel)) as NetReplacePanel;
                }
                return _instance;
            }
        }

        public override void Start()
        {
            atlas = NetworkReplacer.UIUtils.GetAtlas("Ingame");
            backgroundSprite = "MenuPanel2";
            color = new Color32(255, 255, 255, 255);
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            clipChildren = true;
            width = 285;
            height = 380;
            relativePosition = new Vector3(0, 55);

            // Title Bar
            m_title = AddUIComponent<UITitleBar>();
            m_title.title = "Network Replacer";
            //m_title.isModal = true;

            UILabel label = AddUIComponent<UILabel>();
            label.text = "Loaded!";
            label.autoSize = false;
            label.width = 240;
            label.height = 240;
            //label.autoHeight = true;
            label.relativePosition = new Vector2(475, 597);
            label.wordWrap = true;
            label.textAlignment = UIHorizontalAlignment.Center;

            getIdsButton = UIUtils.CreateButton(this);
            getIdsButton.text = "getIDs";
            getIdsButton.relativePosition = new Vector2(20, 90);
            getIdsButton.width = 150;


            getIdsButton.eventClick += (c, p) =>
            {
                if (isVisible)
                {
                    Debug.Log("Button Pressed!");
                    Debug.Log(Tools.GetNetSegmentIds("Basic Road"));
                }
            };

            replaceRoad = UIUtils.CreateButton(this);
            replaceRoad.text = "ReplaceRoad";
            replaceRoad.relativePosition = new Vector2(20, 130);
            replaceRoad.width = 150;


            replaceRoad.eventClick += (c, p) =>
            {
                if (isVisible)
                {
                    Debug.Log("ReplaceRoads Pressed!");
                    Tools.UpgradeNetSegments("Basic Road");
                }
            };

        }
    }
}
