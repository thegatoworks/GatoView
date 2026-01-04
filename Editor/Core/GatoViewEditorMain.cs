#if UNITY_EDITOR

    using System;
    using GatoView.Core;
    using UnityEngine;
    using UnityEditor;
    using UnityEngine.UIElements;
    namespace GatoView.Editor
    {


        

        public class GatoViewInstallerWindow : EditorWindow
        {
            private bool _sizeLocked;

            void CreateGUI()
            {
                // Remove implicit EditorWindow padding
                rootVisualElement.style.paddingLeft = 0;
                rootVisualElement.style.paddingRight = 0;
                rootVisualElement.style.paddingTop = 0;
                rootVisualElement.style.paddingBottom = 0;
                rootVisualElement.style.flexGrow = 0;
                
                var uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                                                                          "Assets/Main/Assets/UI/UXML/EditorInstallPrompt.uxml");

                var ui = uxml.CloneTree();

                // IMPORTANT: prevent stretch
                ui.style.flexGrow = 0;

                rootVisualElement.Add(ui);

                // Listen on the UI, not the window root
                ui.RegisterCallback<GeometryChangedEvent>(OnGeometryChanged);

                var installBtn = ui.Q<Button>("SetupButton");
                var installerInfo = ui.Q<Label>("AssetInfoLabel");

                installerInfo.text = string.Format(
                                                   installerInfo.text,
                                                   GatoViewConstants.AssetVersion,
                                                   GatoViewConstants.InstallerVersion
                                                  );

                installBtn.clicked += OnSetupClick;
            }

            void OnGeometryChanged(GeometryChangedEvent evt)
            {
                if (_sizeLocked)
                    return;

                var element = (VisualElement)evt.target;

                // IMPORTANT: use layout, not newRect
                Vector2 size = element.layout.size;

                // Guard against zero-size passes
                if (size.x <= 0 || size.y <= 0)
                    return;

                _sizeLocked = true;

                minSize = size;
                maxSize = size;

                // Do NOT recenter using Screen.currentResolution in the Editor
                position = new Rect(position.position, size);

                // Stop listening once locked
                element.UnregisterCallback<GeometryChangedEvent>(OnGeometryChanged);
            }

            void OnSetupClick()
            {
                var go = new GameObject("GatoViewService");
                go.AddComponent<GatoViewMain>();
                //DontDestroyOnLoad(go);

                EditorPrefs.SetBool("GatoViewInstallStatus", true);
                Close();
            }
        }//aa

        
        [InitializeOnLoad]
        public class GatoViewEditorMain
        {
            static GatoViewEditorMain()
            {
                EditorApplication.delayCall += ShowInstallWindow;
            }

            static void ShowInstallWindow()
            {
                var window = EditorWindow.CreateWindow<GatoViewInstallerWindow>();
                window.titleContent = new GUIContent("GatoView Setup");

                window.Show();
            }
        }
    }
#endif
