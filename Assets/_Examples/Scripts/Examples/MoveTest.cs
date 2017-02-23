namespace Examples
{
    using System.Collections;
    using UnityEngine;

    public class MoveTest : MonoBehaviour
    {
        public bool moving { get; private set; }
        private Vector3 moveAxis = Vector3.zero;
        private float playerMoveMultiplier = 5f;
        private Transform head;

        private void Awake()
        {
            head = VRTK.VRTK_SDKManager.instance.actualHeadset.transform;
        }

        private void OnEnable()
        {
            GlobalEvent.register("OnStartMove", this, "OnStartMove");
            GlobalEvent.register("OnStopMove", this, "OnStopMove");
            GlobalEvent.register("OnChangeMove", this, "OnChangeMove");
        }

        private void OnDisable()
        {
            GlobalEvent.deregister(this);
        }

        public void OnStartMove(Vector2 axis)
        {
            //Debug.Log("OnStartMove");
            moving = true;
            moveAxis.x = axis.x;
            moveAxis.z = axis.y;
        }

        public void OnStopMove()
        {
            //Debug.Log("OnStopMove");
            moving = false;
            moveAxis = Vector3.zero;
        }

        public void OnChangeMove(Vector2 axis)
        {
            if (moving)
            {
                //Debug.Log("OnChangeMove");
                moveAxis.x = axis.x;
                moveAxis.z = axis.y;
            }
        }

        private void Update()
        {
            if (moving)
            {
                UpdatePosition();
            }
        }

        private void UpdatePosition()
        {
            Vector3 v = Vector3.zero;
            v = head.forward * moveAxis.z + head.right * moveAxis.x;
            v.y = 0;
            v.Normalize();

            transform.Translate(v * Time.deltaTime * playerMoveMultiplier, Space.World);
        }
    }
}