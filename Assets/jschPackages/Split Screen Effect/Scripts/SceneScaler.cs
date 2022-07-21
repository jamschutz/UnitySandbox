using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace jsch.Packages.CameraEffects
{
    public class SceneScaler : MonoBehaviour
    {
        public RectTransform leftHandSide;
        public RectTransform rightHandSide;
        [Range(0, 0.5f)]
        public float sceneViewWidths = 1.0f;
        public float sceneViewHeightPadding = 20;


        void Update()
        {
            SetSceneViewTransforms();
        }

        
        void SetSceneViewTransforms()
        {
            float width = Screen.width * (1.0f - sceneViewWidths) * 0.5f;

            float left  = (Screen.width * sceneViewWidths) * 0.25f;
            float right = Screen.width - (Screen.width * sceneViewWidths) - left - (left * 0.5f);

            // set the Left value in memory rectTransform
            float leftVal = Screen.width * (1.0f - sceneViewWidths);
            leftHandSide.offsetMin = new Vector2(leftVal, leftHandSide.offsetMin.y);

            // set the Right value in dream rectTransform
            float rightVal = Screen.width * (1.0f - sceneViewWidths);
            rightHandSide.offsetMax = new Vector2(-rightVal, rightHandSide.offsetMax.y);

            SetLeft(leftHandSide, left);
            SetLeft(rightHandSide, right);
            SetRight(leftHandSide, right);
            SetRight(rightHandSide, left);

            // set top of both
            rightHandSide.offsetMax  = new Vector2(rightHandSide.offsetMax.x,  -sceneViewHeightPadding);
            leftHandSide.offsetMax = new Vector2(leftHandSide.offsetMax.x, -sceneViewHeightPadding);
            // set bottom of both
            rightHandSide.offsetMin  = new Vector2(rightHandSide.offsetMin.x, sceneViewHeightPadding);
            leftHandSide.offsetMin = new Vector2(leftHandSide.offsetMin.x, sceneViewHeightPadding);
        }


        void SetLeft(RectTransform rect, float left)
        {
            rect.offsetMin = new Vector2(left, rect.offsetMin.y);
        }

        void SetRight(RectTransform rect, float right)
        {
            rect.offsetMax = new Vector2(-right, rect.offsetMax.y);
        }

        void SetTop(RectTransform rect, float top)
        {
            rect.offsetMax  = new Vector2(rect.offsetMax.x,  -top);
        }

        void SetBottom(RectTransform rect, float bottom)
        {
            rect.offsetMin  = new Vector2(rect.offsetMin.x, bottom);
        }
    }

}
