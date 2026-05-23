// GENERATED AUTOMATICALLY FROM 'Assets/_Scripts/GGM/Player/Inputs/Inputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""GamePlay"",
            ""id"": ""2842cce0-97bc-46f7-b12e-cd72ae1696a4"",
            ""actions"": [
                {
                    ""name"": ""shoot"",
                    ""type"": ""Button"",
                    ""id"": ""1b6bd2e3-8909-42f3-a118-7a0fb95b0bb0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""changeGun1"",
                    ""type"": ""Button"",
                    ""id"": ""a1949eb9-d32f-4928-9726-24ce3cedfc69"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""changeGun2"",
                    ""type"": ""Button"",
                    ""id"": ""503481fc-463b-4aaf-b1f5-237b0b787c3b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""changeGun3"",
                    ""type"": ""Button"",
                    ""id"": ""d11f89a8-7ba2-4040-8d69-47288e1d0dcd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cd05d73b-f9d4-4b4e-ada7-1433847a67f3"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8314477e-dfdf-45a0-a2a9-c11f04ca3c76"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""changeGun1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bab7d4ca-bed8-497c-a340-b9aeef5ccae3"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""changeGun2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d9e7089-0130-4fab-91c9-502be3bfcfad"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""changeGun3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GamePlay
        m_GamePlay = asset.FindActionMap("GamePlay", throwIfNotFound: true);
        m_GamePlay_shoot = m_GamePlay.FindAction("shoot", throwIfNotFound: true);
        m_GamePlay_changeGun1 = m_GamePlay.FindAction("changeGun1", throwIfNotFound: true);
        m_GamePlay_changeGun2 = m_GamePlay.FindAction("changeGun2", throwIfNotFound: true);
        m_GamePlay_changeGun3 = m_GamePlay.FindAction("changeGun3", throwIfNotFound: true);
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

    // GamePlay
    private readonly InputActionMap m_GamePlay;
    private IGamePlayActions m_GamePlayActionsCallbackInterface;
    private readonly InputAction m_GamePlay_shoot;
    private readonly InputAction m_GamePlay_changeGun1;
    private readonly InputAction m_GamePlay_changeGun2;
    private readonly InputAction m_GamePlay_changeGun3;
    public struct GamePlayActions
    {
        private @Inputs m_Wrapper;
        public GamePlayActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @shoot => m_Wrapper.m_GamePlay_shoot;
        public InputAction @changeGun1 => m_Wrapper.m_GamePlay_changeGun1;
        public InputAction @changeGun2 => m_Wrapper.m_GamePlay_changeGun2;
        public InputAction @changeGun3 => m_Wrapper.m_GamePlay_changeGun3;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlayActions set) { return set.Get(); }
        public void SetCallbacks(IGamePlayActions instance)
        {
            if (m_Wrapper.m_GamePlayActionsCallbackInterface != null)
            {
                @shoot.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnShoot;
                @shoot.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnShoot;
                @shoot.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnShoot;
                @changeGun1.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChangeGun1;
                @changeGun1.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChangeGun1;
                @changeGun1.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChangeGun1;
                @changeGun2.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChangeGun2;
                @changeGun2.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChangeGun2;
                @changeGun2.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChangeGun2;
                @changeGun3.started -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChangeGun3;
                @changeGun3.performed -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChangeGun3;
                @changeGun3.canceled -= m_Wrapper.m_GamePlayActionsCallbackInterface.OnChangeGun3;
            }
            m_Wrapper.m_GamePlayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @shoot.started += instance.OnShoot;
                @shoot.performed += instance.OnShoot;
                @shoot.canceled += instance.OnShoot;
                @changeGun1.started += instance.OnChangeGun1;
                @changeGun1.performed += instance.OnChangeGun1;
                @changeGun1.canceled += instance.OnChangeGun1;
                @changeGun2.started += instance.OnChangeGun2;
                @changeGun2.performed += instance.OnChangeGun2;
                @changeGun2.canceled += instance.OnChangeGun2;
                @changeGun3.started += instance.OnChangeGun3;
                @changeGun3.performed += instance.OnChangeGun3;
                @changeGun3.canceled += instance.OnChangeGun3;
            }
        }
    }
    public GamePlayActions @GamePlay => new GamePlayActions(this);
    public interface IGamePlayActions
    {
        void OnShoot(InputAction.CallbackContext context);
        void OnChangeGun1(InputAction.CallbackContext context);
        void OnChangeGun2(InputAction.CallbackContext context);
        void OnChangeGun3(InputAction.CallbackContext context);
    }
}
