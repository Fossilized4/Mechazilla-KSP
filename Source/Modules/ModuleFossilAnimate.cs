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
        public string AnimOnText;

        [KSPField]
        public string AnimOffText;

        [KSPField]
        public string AnimToggleText;

        [KSPField(guiActive = true, guiActiveUnfocused = true, guiName = "Animate", isPersistant = true, unfocusedRange = 75f)]
        [UI_Toggle(affectSymCounterparts = UI_Scene.All, disabledText = "AnimOffText", enabledText = "AnimOnText", scene = UI_Scene.All)]
        public bool AnimateBool = true;

        [KSPAction(guiName = "AnimToggleText")]
        private void ToggleAction(KSPActionParam param)
        {
            AnimateBool = !AnimateBool;
        }

        [KSPAction(guiName = "AnimOffText")]
        private void EnableAction(KSPActionParam param)
        {
            AnimateBool = true;
        }

        [KSPAction(guiName = "AnimOnText")]
        private void DisableAction(KSPActionParam param)
        {
            AnimateBool = false;
        }


        private Animation anim;

        public bool PrevAnimateBool;
        public void Start()
        {
            anim = part.FindModelAnimator(AnimationID);
            if (anim == null)
            {
                Debug.LogError($"[{MODULENAME}] Could not find animation {AnimationID} on part {part.name}");
            }

            if (AnimOnText == null)
            {
                Debug.LogError($"[{MODULENAME}] AnimOnText not set on part {part.name}");
            }
            else
            {
                Actions["DisableAction"].guiName = AnimOnText;
            }

            if (AnimOffText == null)
            {
                Debug.LogError($"[{MODULENAME}] AnimOffText not set on part {part.name}");
            }
            else
            {
                Actions["EnableAction"].guiName = AnimOffText;
            }

            if (AnimToggleText == null)
            {
                Debug.LogError($"[{MODULENAME}] AnimToggleText not set on part {part.name}");
            }
            else
            {
                Actions["ToggleAction"].guiName = AnimToggleText;
            }

            PrevAnimateBool = AnimateBool;
            if (Fields.TryGetFieldUIControl("AnimateBool", out UI_Toggle AnimateFieldVar))
            {
                if (AnimOnText != null)
                {
                    AnimateFieldVar.enabledText = AnimOnText;
                }
                if (AnimOffText != null)
                {
                    AnimateFieldVar.disabledText = AnimOffText;
                }
               
            }
            
            
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
                    anim[AnimationID].normalizedTime = 1.0f;
                    anim[AnimationID].enabled = true;
                    anim.Play(AnimationID);
                }
                
            }
            PrevAnimateBool = AnimateBool;
        }
    }
}