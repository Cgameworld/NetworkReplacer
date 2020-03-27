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
        private UIButton m_button;
        private UIButton initialRoadAdd;

        public UILabel fromRoadLabel;
        public string selectedRoadName;
        private UIButton removeFromFieldButton;
        private UIPanel fromFieldRow;
        private UIPanel toFieldRow;
        private UILabel toRoadLabel;
        private UIButton finalRoadAdd;
        private UIButton removeToFieldButton;

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
            atlas = UIUtils.GetAtlas("Ingame");
            backgroundSprite = "MenuPanel2";
            color = new Color32(255, 255, 255, 255);
            isVisible = false;
            canFocus = true;
            isInteractive = true;
            clipChildren = true;
            width = 470;
            height = 200;
            relativePosition = new Vector3(0, 55);

            // Title Bar
            m_title = AddUIComponent<UITitleBar>();
            m_title.title = "Network Replacer";
            //m_title.isModal = true;

            fromFieldRow = AddUIComponent<UIPanel>();
            fromFieldRow.relativePosition = new Vector2(0, 60);
            fromFieldRow.size = new Vector2(width, 0);

            fromRowUI();

            toFieldRow = AddUIComponent<UIPanel>();
            toFieldRow.relativePosition = new Vector2(0, 100);
            toFieldRow.size = new Vector2(width, 70);

            toRowUI();

            removeFromFieldButton = UIUtils.CreateButton(this);
            removeFromFieldButton.text = "Reset";
            removeFromFieldButton.relativePosition = new Vector2(210, 150);
            removeFromFieldButton.width = 150;


            removeFromFieldButton.eventClick += (c, p) =>
            {
                if (isVisible)
                {
                    initialRoadAdd.isVisible = true;
                    fromRoadLabel.isVisible = false;
                    finalRoadAdd.isVisible = true;
                    toRoadLabel.isVisible = false;
                }
            };

            getIdsButton = UIUtils.CreateButton(this);
            getIdsButton.text = "getIDs";
            getIdsButton.relativePosition = new Vector2(370, 150);
            getIdsButton.width = 70;


            getIdsButton.eventClick += (c, p) =>
            {
                if (isVisible)
                {
                    Debug.Log("Button Pressed!");
                    Debug.Log(Tools.GetNetSegmentIds("Basic Road"));
                }
            };

            replaceRoad = UIUtils.CreateButton(this);
            replaceRoad.text = "Replace Network";
            replaceRoad.relativePosition = new Vector2(20, 150);
            replaceRoad.width = 180;


            replaceRoad.eventClick += (c, p) =>
            {
                if (isVisible)
                {
                    Debug.Log(fromRoadLabel.text + " |||| " + toRoadLabel.text);
                    Tools.UpgradeNetSegments(fromRoadLabel.text,toRoadLabel.text);
                }
            };


        }

        private void fromRowUI()
        {
            UILabel fromLabel = fromFieldRow.AddUIComponent<UILabel>();
            //"select from road panel"
            fromLabel.autoSize = false;
            fromLabel.width = 60;
            fromLabel.height = 30;
            fromLabel.relativePosition = new Vector2(20, 0);
            fromLabel.text = "From:";

            fromRoadLabel = fromFieldRow.AddUIComponent<UILabel>();
            //"select from road panel"
            fromRoadLabel.autoSize = false;
            fromRoadLabel.width = 300;
            fromRoadLabel.height = 30;
            fromRoadLabel.textColor = new Color32(0, 255, 0, 255);
            fromRoadLabel.relativePosition = new Vector2(75, 0);
            fromRoadLabel.isVisible = false;

            initialRoadAdd = UIUtils.CreateButton(fromFieldRow);
            initialRoadAdd.text = "Add Selected Road";
            initialRoadAdd.relativePosition = new Vector2(80, -5);
            initialRoadAdd.width = 175;
            initialRoadAdd.height = 30;
            initialRoadAdd.name = "init_roadadd";
            initialRoadAdd.isVisible = true;
            initialRoadAdd.tooltip = "Adds actively selected network in the vanilla road panel/find it panel to the \"From:\" field ";


            initialRoadAdd.eventClick += (c, p) =>
            {
                if (isVisible)
                {
                    initialRoadAdd.isVisible = false;
                    fromRoadLabel.isVisible = true;
                    if (GameObject.FindObjectOfType<NetTool>().m_prefab == null)
                    {
                        fromRoadLabel.textColor = new Color32(255, 0, 0, 255);
                        fromRoadLabel.text = "Select from vanilla roads panel!";
                    }
                    else
                    {
                        fromRoadLabel.textColor = new Color32(0, 255, 0, 255);
                        fromRoadLabel.text = GameObject.FindObjectOfType<NetTool>().m_prefab.name;
                        fromRoadLabel.tooltip = GameObject.FindObjectOfType<NetTool>().m_prefab.name;
                    }
                }
            };

            removeFromFieldButton = UIUtils.CreateButton(fromFieldRow);
            removeFromFieldButton.text = "Clear";
            removeFromFieldButton.relativePosition = new Vector2(370, -5);
            removeFromFieldButton.height = 25;
            removeFromFieldButton.width = 80;
            removeFromFieldButton.isVisible = true;

            removeFromFieldButton.eventClick += (c, p) =>
            {
                initialRoadAdd.isVisible = true;
                fromRoadLabel.isVisible = false;
            };
        }
        private void toRowUI()
        {
            UILabel toLabel = toFieldRow.AddUIComponent<UILabel>();
            //"select from road panel"
            toLabel.autoSize = false;
            toLabel.width = 60;
            toLabel.height = 30;
            toLabel.relativePosition = new Vector2(42, 0);
            toLabel.text = "To:";

            toRoadLabel = toFieldRow.AddUIComponent<UILabel>();
            //"select from road panel"
            toRoadLabel.autoSize = false;
            toRoadLabel.width = 300;
            toRoadLabel.height = 30;
            toRoadLabel.textColor = new Color32(0, 255, 0, 255);
            toRoadLabel.relativePosition = new Vector2(75, 0);
            toRoadLabel.isVisible = false;

            finalRoadAdd = UIUtils.CreateButton(toFieldRow);
            finalRoadAdd.text = "Add Selected Road";
            finalRoadAdd.relativePosition = new Vector2(80, -5);
            finalRoadAdd.width = 175;
            finalRoadAdd.height = 30;
            finalRoadAdd.name = "init_roadto";
            finalRoadAdd.isVisible = true;
            finalRoadAdd.tooltip = "Adds actively selected network in the vanilla road panel/find it panel to the \"To:\" field ";


            finalRoadAdd.eventClick += (c, p) =>
            {
                if (isVisible)
                {
                    finalRoadAdd.isVisible = false;
                    toRoadLabel.isVisible = true;
                    if (GameObject.FindObjectOfType<NetTool>().m_prefab == null)
                    {
                        toRoadLabel.textColor = new Color32(255, 0, 0, 255);
                        toRoadLabel.text = "Select from vanilla roads panel!";
                    }
                    else
                    {
                        toRoadLabel.textColor = new Color32(0, 255, 0, 255);
                        toRoadLabel.text = GameObject.FindObjectOfType<NetTool>().m_prefab.name;
                        toRoadLabel.tooltip = GameObject.FindObjectOfType<NetTool>().m_prefab.name;
                    }
                }
            };

            removeToFieldButton = UIUtils.CreateButton(toFieldRow);
            removeToFieldButton.text = "Clear";
            removeToFieldButton.relativePosition = new Vector2(370, -5);
            removeToFieldButton.height = 25;
            removeToFieldButton.width = 80;

            removeToFieldButton.eventClick += (c, p) =>
            {
                finalRoadAdd.isVisible = true;
                toRoadLabel.isVisible = false;
            };
        }
    }

}
