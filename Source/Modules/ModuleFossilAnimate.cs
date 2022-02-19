using System;
using UnityEngine;

namespace FossilIndustries.Modules
{
    public class ModuleFossilAnimate : PartModule
    {
        public const string MODULENAME = "ModuleFossilAnimate";

        [KSPAxisField(axisMode = KSPAxisMode.Incremental, guiActive = true, guiActiveUnfocused = true, guiFormat = "0", guiName = "Animation Speed", guiUnits = "%", incrementalSpeed = 50f, isPersistant = true, maxValue = 100f, minValue = -100f, unfocusedRange = 25f)]
        [UI_FloatRange(affectSymCounterparts = UI_Scene.All, maxValue = 100f, minValue = 10f, scene = UI_Scene.All, stepIncrement = 0.1f)]
        public float AnimationSpeed = 100f;

        [KSPField]
        public string AnimationID;

        [KSPField]
        public float AnimationMaxSpeed = 1f;

        [KSPField]
        private string AnimOnText;

        [KSPField]
        private string AnimOffText;

        [KSPField(guiActive = true, guiActiveUnfocused = true, guiName = "Animate", isPersistant = true, unfocusedRange = 25f)]
        [UI_Toggle(affectSymCounterparts = UI_Scene.All, disabledText = "Open Arms", enabledText = "Close Arms", scene = UI_Scene.All)]
        public bool AnimateBool = false;

        private Animation anim;

        public bool PrevAnimateBool;
        public void Start()
        {
            anim = part.FindModelAnimator(AnimationID);
            if (anim == null)
            {
                Debug.LogError($"[{MODULENAME}] Could not find animation {AnimationID} on part {part.name}");
            }
            PrevAnimateBool = AnimateBool;


        }
        public void FixedUpdate()
        {
            if (AnimateBool == !PrevAnimateBool)
            {
                if (AnimateBool == true)
                {
                    anim[AnimationID].speed = AnimationMaxSpeed * (AnimationSpeed / 100);
                    anim[AnimationID].normalizedTime = 0.0f;
                    anim[AnimationID].enabled = true;
                    anim.Play(AnimationID);
                }
                else
                {
                    anim[AnimationID].speed = -AnimationMaxSpeed * (AnimationSpeed / 100);
                    anim[AnimationID].normalizedTime = anim[AnimationID].length - 1;
                    anim[AnimationID].enabled = true;
                    anim.Play(AnimationID);
                }
                
            }
            PrevAnimateBool = AnimateBool;
        }
    }
}