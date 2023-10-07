//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Resources/InpuyActions/CameraControl.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Monster
{
    public partial class @CameraControl : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @CameraControl()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""CameraControl"",
    ""maps"": [
        {
            ""name"": ""CameraMovement"",
            ""id"": ""14b7ea89-e483-44af-b11a-2b63284a9edb"",
            ""actions"": [
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""4fff9458-9ba0-4c08-8c50-598f5102c1cf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""87243a52-a045-4acd-a33d-fcde6a5f6ef6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveUp"",
                    ""type"": ""Button"",
                    ""id"": ""641af164-173d-49a2-9a95-20a657574b4e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveDown"",
                    ""type"": ""Button"",
                    ""id"": ""a6a37461-0dee-460f-bc8c-58add731ea93"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ZoomIn"",
                    ""type"": ""Button"",
                    ""id"": ""20fcabed-6469-494c-ba16-19ac31f127d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ZoomOut"",
                    ""type"": ""Button"",
                    ""id"": ""83d2ff76-be78-486e-b2b6-96a7c8b931eb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d0f135af-df82-472a-9320-c362dc45d7a5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1230735-cd7c-4fb1-b050-df7fa7603510"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4bb394f7-eb94-4ffd-a9d4-1c43aed6b260"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""286d7397-0f7c-4f1b-8471-fff8f1a63d50"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11572269-33db-4c9f-9467-fb1d893533be"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomIn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10704fe5-1086-4579-82a9-7cdf0998cd67"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomOut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // CameraMovement
            m_CameraMovement = asset.FindActionMap("CameraMovement", throwIfNotFound: true);
            m_CameraMovement_MoveLeft = m_CameraMovement.FindAction("MoveLeft", throwIfNotFound: true);
            m_CameraMovement_MoveRight = m_CameraMovement.FindAction("MoveRight", throwIfNotFound: true);
            m_CameraMovement_MoveUp = m_CameraMovement.FindAction("MoveUp", throwIfNotFound: true);
            m_CameraMovement_MoveDown = m_CameraMovement.FindAction("MoveDown", throwIfNotFound: true);
            m_CameraMovement_ZoomIn = m_CameraMovement.FindAction("ZoomIn", throwIfNotFound: true);
            m_CameraMovement_ZoomOut = m_CameraMovement.FindAction("ZoomOut", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }
        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // CameraMovement
        private readonly InputActionMap m_CameraMovement;
        private ICameraMovementActions m_CameraMovementActionsCallbackInterface;
        private readonly InputAction m_CameraMovement_MoveLeft;
        private readonly InputAction m_CameraMovement_MoveRight;
        private readonly InputAction m_CameraMovement_MoveUp;
        private readonly InputAction m_CameraMovement_MoveDown;
        private readonly InputAction m_CameraMovement_ZoomIn;
        private readonly InputAction m_CameraMovement_ZoomOut;
        public struct CameraMovementActions
        {
            private @CameraControl m_Wrapper;
            public CameraMovementActions(@CameraControl wrapper) { m_Wrapper = wrapper; }
            public InputAction @MoveLeft => m_Wrapper.m_CameraMovement_MoveLeft;
            public InputAction @MoveRight => m_Wrapper.m_CameraMovement_MoveRight;
            public InputAction @MoveUp => m_Wrapper.m_CameraMovement_MoveUp;
            public InputAction @MoveDown => m_Wrapper.m_CameraMovement_MoveDown;
            public InputAction @ZoomIn => m_Wrapper.m_CameraMovement_ZoomIn;
            public InputAction @ZoomOut => m_Wrapper.m_CameraMovement_ZoomOut;
            public InputActionMap Get() { return m_Wrapper.m_CameraMovement; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CameraMovementActions set) { return set.Get(); }
            public void SetCallbacks(ICameraMovementActions instance)
            {
                if (m_Wrapper.m_CameraMovementActionsCallbackInterface != null)
                {
                    @MoveLeft.started -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMoveLeft;
                    @MoveLeft.performed -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMoveLeft;
                    @MoveLeft.canceled -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMoveLeft;
                    @MoveRight.started -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMoveRight;
                    @MoveRight.performed -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMoveRight;
                    @MoveRight.canceled -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMoveRight;
                    @MoveUp.started -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMoveUp;
                    @MoveUp.performed -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMoveUp;
                    @MoveUp.canceled -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMoveUp;
                    @MoveDown.started -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMoveDown;
                    @MoveDown.performed -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMoveDown;
                    @MoveDown.canceled -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnMoveDown;
                    @ZoomIn.started -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnZoomIn;
                    @ZoomIn.performed -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnZoomIn;
                    @ZoomIn.canceled -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnZoomIn;
                    @ZoomOut.started -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnZoomOut;
                    @ZoomOut.performed -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnZoomOut;
                    @ZoomOut.canceled -= m_Wrapper.m_CameraMovementActionsCallbackInterface.OnZoomOut;
                }
                m_Wrapper.m_CameraMovementActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MoveLeft.started += instance.OnMoveLeft;
                    @MoveLeft.performed += instance.OnMoveLeft;
                    @MoveLeft.canceled += instance.OnMoveLeft;
                    @MoveRight.started += instance.OnMoveRight;
                    @MoveRight.performed += instance.OnMoveRight;
                    @MoveRight.canceled += instance.OnMoveRight;
                    @MoveUp.started += instance.OnMoveUp;
                    @MoveUp.performed += instance.OnMoveUp;
                    @MoveUp.canceled += instance.OnMoveUp;
                    @MoveDown.started += instance.OnMoveDown;
                    @MoveDown.performed += instance.OnMoveDown;
                    @MoveDown.canceled += instance.OnMoveDown;
                    @ZoomIn.started += instance.OnZoomIn;
                    @ZoomIn.performed += instance.OnZoomIn;
                    @ZoomIn.canceled += instance.OnZoomIn;
                    @ZoomOut.started += instance.OnZoomOut;
                    @ZoomOut.performed += instance.OnZoomOut;
                    @ZoomOut.canceled += instance.OnZoomOut;
                }
            }
        }
        public CameraMovementActions @CameraMovement => new CameraMovementActions(this);
        public interface ICameraMovementActions
        {
            void OnMoveLeft(InputAction.CallbackContext context);
            void OnMoveRight(InputAction.CallbackContext context);
            void OnMoveUp(InputAction.CallbackContext context);
            void OnMoveDown(InputAction.CallbackContext context);
            void OnZoomIn(InputAction.CallbackContext context);
            void OnZoomOut(InputAction.CallbackContext context);
        }
    }
}
