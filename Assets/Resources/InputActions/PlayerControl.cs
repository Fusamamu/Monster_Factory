//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Resources/InputActions/PlayerControl.inputactions
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
    public partial class @PlayerControl : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControl()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControl"",
    ""maps"": [
        {
            ""name"": ""PlayerSelection"",
            ""id"": ""103197b4-8e22-446b-baa3-512904ed5e5f"",
            ""actions"": [
                {
                    ""name"": ""ChangePlayerControl"",
                    ""type"": ""Button"",
                    ""id"": ""c1f64771-ff37-4753-90af-f8eecb9f6cbc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangePlayerIndexUp"",
                    ""type"": ""Button"",
                    ""id"": ""5f1f187a-a4ac-48fa-a658-e66a9e09a717"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangePlayerIndexDown"",
                    ""type"": ""Button"",
                    ""id"": ""d8b157b5-b083-4f55-9566-2443eb0730fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""54d51385-887e-49c7-9df6-6894424cd7bd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangePlayerControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""506afc00-fe4d-4fc1-aeda-69d40a73a813"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangePlayerIndexUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df66d207-3af2-407c-99d2-453d7e93fccf"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangePlayerIndexDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // PlayerSelection
            m_PlayerSelection = asset.FindActionMap("PlayerSelection", throwIfNotFound: true);
            m_PlayerSelection_ChangePlayerControl = m_PlayerSelection.FindAction("ChangePlayerControl", throwIfNotFound: true);
            m_PlayerSelection_ChangePlayerIndexUp = m_PlayerSelection.FindAction("ChangePlayerIndexUp", throwIfNotFound: true);
            m_PlayerSelection_ChangePlayerIndexDown = m_PlayerSelection.FindAction("ChangePlayerIndexDown", throwIfNotFound: true);
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

        // PlayerSelection
        private readonly InputActionMap m_PlayerSelection;
        private IPlayerSelectionActions m_PlayerSelectionActionsCallbackInterface;
        private readonly InputAction m_PlayerSelection_ChangePlayerControl;
        private readonly InputAction m_PlayerSelection_ChangePlayerIndexUp;
        private readonly InputAction m_PlayerSelection_ChangePlayerIndexDown;
        public struct PlayerSelectionActions
        {
            private @PlayerControl m_Wrapper;
            public PlayerSelectionActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
            public InputAction @ChangePlayerControl => m_Wrapper.m_PlayerSelection_ChangePlayerControl;
            public InputAction @ChangePlayerIndexUp => m_Wrapper.m_PlayerSelection_ChangePlayerIndexUp;
            public InputAction @ChangePlayerIndexDown => m_Wrapper.m_PlayerSelection_ChangePlayerIndexDown;
            public InputActionMap Get() { return m_Wrapper.m_PlayerSelection; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerSelectionActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerSelectionActions instance)
            {
                if (m_Wrapper.m_PlayerSelectionActionsCallbackInterface != null)
                {
                    @ChangePlayerControl.started -= m_Wrapper.m_PlayerSelectionActionsCallbackInterface.OnChangePlayerControl;
                    @ChangePlayerControl.performed -= m_Wrapper.m_PlayerSelectionActionsCallbackInterface.OnChangePlayerControl;
                    @ChangePlayerControl.canceled -= m_Wrapper.m_PlayerSelectionActionsCallbackInterface.OnChangePlayerControl;
                    @ChangePlayerIndexUp.started -= m_Wrapper.m_PlayerSelectionActionsCallbackInterface.OnChangePlayerIndexUp;
                    @ChangePlayerIndexUp.performed -= m_Wrapper.m_PlayerSelectionActionsCallbackInterface.OnChangePlayerIndexUp;
                    @ChangePlayerIndexUp.canceled -= m_Wrapper.m_PlayerSelectionActionsCallbackInterface.OnChangePlayerIndexUp;
                    @ChangePlayerIndexDown.started -= m_Wrapper.m_PlayerSelectionActionsCallbackInterface.OnChangePlayerIndexDown;
                    @ChangePlayerIndexDown.performed -= m_Wrapper.m_PlayerSelectionActionsCallbackInterface.OnChangePlayerIndexDown;
                    @ChangePlayerIndexDown.canceled -= m_Wrapper.m_PlayerSelectionActionsCallbackInterface.OnChangePlayerIndexDown;
                }
                m_Wrapper.m_PlayerSelectionActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ChangePlayerControl.started += instance.OnChangePlayerControl;
                    @ChangePlayerControl.performed += instance.OnChangePlayerControl;
                    @ChangePlayerControl.canceled += instance.OnChangePlayerControl;
                    @ChangePlayerIndexUp.started += instance.OnChangePlayerIndexUp;
                    @ChangePlayerIndexUp.performed += instance.OnChangePlayerIndexUp;
                    @ChangePlayerIndexUp.canceled += instance.OnChangePlayerIndexUp;
                    @ChangePlayerIndexDown.started += instance.OnChangePlayerIndexDown;
                    @ChangePlayerIndexDown.performed += instance.OnChangePlayerIndexDown;
                    @ChangePlayerIndexDown.canceled += instance.OnChangePlayerIndexDown;
                }
            }
        }
        public PlayerSelectionActions @PlayerSelection => new PlayerSelectionActions(this);
        public interface IPlayerSelectionActions
        {
            void OnChangePlayerControl(InputAction.CallbackContext context);
            void OnChangePlayerIndexUp(InputAction.CallbackContext context);
            void OnChangePlayerIndexDown(InputAction.CallbackContext context);
        }
    }
}
