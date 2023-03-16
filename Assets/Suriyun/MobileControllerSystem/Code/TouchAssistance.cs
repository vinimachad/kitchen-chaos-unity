﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Suriyun.MCS
{

    public class TouchAssistance : MonoBehaviour
    {
        public BtnSetting btnMain;
        public BtnSetting btnSub1;

        protected virtual void Start()
        {
            btnMain.Init();
            btnSub1.Init();
        }



        [Serializable]
        public class BtnSetting
        {
            public RectTransform btn;
            protected Vector3 refScale;
            public float focusedMultiplier = 1.2f;
            public float unfocusedMultiplier = 0.8f;
            protected Vector3 toScale;
            public float scaleSpeed = 1.6f;

            public void Init()
            {
                refScale = btn.localScale;
                toScale = refScale;
            }

            public void Update()
            {
                btn.localScale = Vector3.Lerp(btn.localScale, toScale, scaleSpeed * Time.deltaTime);
            }

            public void ToFocusedScale()
            {
                toScale = refScale * focusedMultiplier;
            }

            public void ToUnfocusedScale()
            {
                toScale = refScale * unfocusedMultiplier;
            }

        }
    }

}