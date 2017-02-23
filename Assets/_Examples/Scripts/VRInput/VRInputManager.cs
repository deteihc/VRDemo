namespace Examples
{
    using System.Collections;
    using UnityEngine;
    using VRTK;

    public class VRInputManager : MonoBehaviour
    {
        #region SINGLETON

        private static VRInputManager instance = null;

        public static VRInputManager Instance
        {
            get
            {
                if (instance) return instance;
                else
                {
                    Debug.LogError("VRInputManager is uninitialized!");
                    return null;
                }
            }
        }

        #endregion SINGLETON

        #region Init

        private void Awake()
        {
            instance = this;
            Init();
        }

        private void Init()
        {
            InitController();
        }

        /// <summary>
        /// 初始化VR控制器
        /// </summary>
        private void InitController()
        {
            GameObject hand = VRTK_SDKManager.instance.actualBoundaries.transform.FindChild("Hand").gameObject;
            if (!VRInputDefined.isHMDConnected)
            {
                // 头显设置
                Transform tmpHead = VRTK_SDKManager.instance.actualHeadset.transform.parent;
                VRTK_SDKManager.instance.actualHeadset.transform.SetParent(VRTK_SDKManager.instance.actualBoundaries.transform, false);
                tmpHead.SetParent(VRTK_SDKManager.instance.actualHeadset.transform, false);
                tmpHead.GetChild(0).SetParent(VRTK_SDKManager.instance.actualHeadset.transform, false);
                tmpHead.gameObject.SetActive(false);
                VRTK_SDKManager.instance.actualHeadset.transform.localPosition = new Vector3(0, 1.7f, 0);

                // 控制器模型
                GameObject leftHand = Instantiate(hand, VRTK_SDKManager.instance.actualLeftController.transform, false) as GameObject;
                leftHand.name = "Hand";
                GameObject rightHand = Instantiate(hand, VRTK_SDKManager.instance.actualRightController.transform, false) as GameObject;
                rightHand.name = "Hand";
                VRTK_SDKManager.instance.actualLeftController.transform.localPosition = new Vector3(-0.2f, 1.2f, 0.5f);
                VRTK_SDKManager.instance.actualRightController.transform.localPosition = new Vector3(0.2f, 1.2f, 0.5f);

                // 添加模拟器操控脚本
                VRTK_SDKManager.instance.actualLeftController.AddComponent<SDK_ControllerSim>();
                VRTK_SDKManager.instance.actualRightController.AddComponent<SDK_ControllerSim>();
                VRTK_SDKManager.instance.actualBoundaries.AddComponent<Examples.SDK_InputSimulator>();
            }
            DestroyImmediate(hand);
        }

        #endregion Init
    }
}