/*******
*
*     Title:
*     Description:
*     Date:
*     Version:
*     Modify Recoder:
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace BaseMVCFrame.UI
{
	public class CameraController : MonoBehaviour
	{
        public float moveSpeed; // 设置相机移动速度  
        /// <summary>
        /// 相机的最大缩放像素
        /// </summary>
        public int _CameraMaxScale;
        /// <summary>
        /// 相机的最小缩放像素
        /// </summary>
        public int _CameraMinScale;
        /// <summary>
        /// 相机
        /// </summary>
        private Camera _mainCamera;
        /// <summary>
        /// 鼠标左键按下开始的位置
        /// </summary>
        private Vector2 _MouseLeftStartPos;
        /// <summary>
        /// 是否显示选择区域
        /// </summary>
        private bool _IsShowChoseArea;

        private LineRenderer _MouseLeftChoseLine;

        private void Awake()
        {
            _mainCamera = GetComponent<Camera>();
            _IsShowChoseArea = false;
            _MouseLeftChoseLine = this.transform.GetChild<LineRenderer>("MouseLeftChoseLine");
        }

        void Update()
        {
            //// 当按住鼠标右键的时候  
            if (Input.GetMouseButton(1))
            {
                // 获取鼠标的x和y的值，乘以速度和Time.deltaTime是因为这个可以是运动起来更平滑  
                float h = Input.GetAxis("Mouse X")* moveSpeed * Time.deltaTime;
                float v = Input.GetAxis("Mouse Y")* moveSpeed * Time.deltaTime;
                // 设置当前摄像机移动，y轴并不改变  
                // 需要摄像机按照世界坐标移动，而不是按照它自身的坐标移动，所以加上Spance.World
                this.transform.Translate(-h, -v, 0, Space.World);
            }


            CamreaScaleCheck();

            ShowMouseLeftChoseArea();
        }

        /// <summary>
        /// 鼠标滑轮缩放检测
        /// </summary>
        private void CamreaScaleCheck()
        {
            float wheel = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 100;
            if (wheel != 0)
            {
                PixelPerfectCamera perfectCamera = _mainCamera.GetComponent<PixelPerfectCamera>();
                int ScaleValue = 20;
                if (wheel < 0)
                {
                    ScaleValue = -ScaleValue;
                }
                int value = perfectCamera.assetsPPU + ScaleValue;
                if (value >= _CameraMaxScale)
                {
                    perfectCamera.assetsPPU = _CameraMaxScale;
                }
                else if (value <= _CameraMinScale)
                {
                    perfectCamera.assetsPPU = _CameraMinScale;
                }
                else
                {
                    perfectCamera.assetsPPU += ScaleValue;
                }
                //Debug.Log("滑轮的值:" + wheel);
            }
        }

        private void ShowMouseLeftChoseArea()
        {
            if (MouseListener._Instance.IsDrawMouseChoseArea)
            {
                Vector2 mouseStartPos = MouseListener._Instance.MouseStartDragPos;
                Vector2 mouseEndPos = MouseListener._Instance.MouseEndDragPos;
                //更新胡画线组件的线条
                Vector3[] LinePos = new Vector3[4];
                LinePos[0] = new Vector3(mouseStartPos.x, mouseStartPos.y, -2);
                LinePos[1] = new Vector3(mouseEndPos.x, mouseStartPos.y, -2);
                LinePos[2] = new Vector3(mouseEndPos.x, mouseEndPos.y, -2);
                LinePos[3] = new Vector3(mouseStartPos.x, mouseEndPos.y, -2);
                _MouseLeftChoseLine.positionCount = 5;
                _MouseLeftChoseLine.SetPosition(0, LinePos[0]);
                _MouseLeftChoseLine.SetPosition(1, LinePos[1]);
                _MouseLeftChoseLine.SetPosition(2, LinePos[2]);
                _MouseLeftChoseLine.SetPosition(3, LinePos[3]);
                _MouseLeftChoseLine.SetPosition(4, LinePos[0]);
            }
            else
            {
                if (_MouseLeftChoseLine.positionCount != 0)
                {
                    _MouseLeftChoseLine.positionCount = 0;
                }
            }
        }

    }
}
