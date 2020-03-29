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
        private UIButton clearAllButton;
        private UIButton swapFromToButton;

        string NullRoadPhrase = "Select from vanilla roads panel!";

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
            relativePosition = new Vector3(25, 700);

            // Title Bar
            m_title = AddUIComponent<UITitleBar>();
            m_title.title = "Bulk Network Replacer";
            //m_title.isModal = true;

            fromFieldRow = AddUIComponent<UIPanel>();
            fromFieldRow.relativePosition = new Vector2(0, 60);
            fromFieldRow.size = new Vector2(width, 0);

            fromRowUI();

            toFieldRow = AddUIComponent<UIPanel>();
            toFieldRow.relativePosition = new Vector2(0, 100);
            toFieldRow.size = new Vector2(width, 70);

            toRowUI();

            replaceRoad = UIUtils.CreateButton(this);
            replaceRoad.text = "Replace Network";
            replaceRoad.relativePosition = new Vector2(20, 150);
            replaceRoad.width = 200;


            replaceRoad.eventClick += (c, p) =>
            {
                if (isVisible)
                {
                    Debug.Log("from: " + fromRoadLabel.text + "\nto:" + toRoadLabel.text);
                    if (fromRoadLabel.text == NullRoadPhrase || toRoadLabel.text == NullRoadPhrase)
                    {
                        Tools.ShowErrorWindow("Input Error", "Bulk Network Replacer:\n\n" + "The from and/or to field have no network(s) selected");
                    }
                    else if (fromRoadLabel.text == toRoadLabel.text)
                    {
                            Debug.Log("dups");
                            Tools.ShowErrorWindow("Duplicate Networks", "Bulk Network Replacer:\n\n" + "The from and to fields have the same network selected");
                    }
                    else if (fromRoadLabel.text != NullRoadPhrase && toRoadLabel.text != NullRoadPhrase)
                    {
                            Tools.UpgradeNetSegments(fromRoadLabel.text, toRoadLabel.text);
                    }

                    else
                    {
                        Tools.ShowErrorWindow("Unknown Error", "Bulk Network Replacer:\n\n" + "Report the steps before clicking taken on the workshop page of this mod");
                    }
                }
            };

            swapFromToButton = UIUtils.CreateButton(this);
            swapFromToButton.text = "Swap";
            swapFromToButton.relativePosition = new Vector2(230, 150);
            swapFromToButton.width = 90;


            swapFromToButton.eventClick += (c, p) =>
            {
                if (isVisible)
                {
                    var fromtext = fromRoadLabel.text;
                    var totext = toRoadLabel.text;
                    fromRoadLabel.text = totext;
                    toRoadLabel.text = fromtext;
                    
                }
            };


            clearAllButton = UIUtils.CreateButton(this);
            clearAllButton.text = "Clear All";
            clearAllButton.relativePosition = new Vector2(330, 150);
            clearAllButton.width = 110;

            clearAllButton.eventClick += (c, p) =>
            {
                if (isVisible)
                {
                    initialRoadAdd.isVisible = true;
                    fromRoadLabel.isVisible = false;
                    finalRoadAdd.isVisible = true;
                    toRoadLabel.isVisible = false;

                    removeFromFieldButton.isVisible = false;
                    removeToFieldButton.isVisible = false;

                    fromRoadLabel.text = NullRoadPhrase;
                    toRoadLabel.text = NullRoadPhrase;
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
            fromRoadLabel.text = NullRoadPhrase;

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
                    removeFromFieldButton.isVisible = true;
                    if (GameObject.FindObjectOfType<NetTool>().m_prefab == null)
                    {
                        fromRoadLabel.textColor = new Color32(255, 0, 0, 255);
                        fromRoadLabel.text = NullRoadPhrase;
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
            removeFromFieldButton.isVisible = false;

            removeFromFieldButton.eventClick += (c, p) =>
            {
                initialRoadAdd.isVisible = true;
                fromRoadLabel.isVisible = false;
                fromRoadLabel.text = NullRoadPhrase;
                removeFromFieldButton.isVisible = false;
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
            toRoadLabel.text = NullRoadPhrase;
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
                    removeToFieldButton.isVisible = true;
                    if (GameObject.FindObjectOfType<NetTool>().m_prefab == null)
                    {
                        toRoadLabel.textColor = new Color32(255, 0, 0, 255);
                        toRoadLabel.text = NullRoadPhrase;
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
            removeToFieldButton.isVisible = false;

            removeToFieldButton.eventClick += (c, p) =>
            {
                finalRoadAdd.isVisible = true;
                toRoadLabel.isVisible = false;
                toRoadLabel.text = NullRoadPhrase;
                removeToFieldButton.isVisible = false;
            };
        }
    }

}
