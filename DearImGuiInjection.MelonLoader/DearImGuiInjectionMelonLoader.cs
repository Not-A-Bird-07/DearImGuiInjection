using System.Collections.Generic;
using MelonLoader;
using System.IO;
using DearImGuiInjection.MelonLoader;
using MelonLoader.Utils;
using UnityEngine;
using UnityEngine.Rendering;

[assembly: MelonInfo(typeof(DearImGuiInjectionMelonLoader), DearImGuiInjectionMetadata.Name, DearImGuiInjectionMetadata.Version, DearImGuiInjectionMetadata.Author)]
namespace DearImGuiInjection.MelonLoader;

internal class DearImGuiInjectionMelonLoader : MelonMod, ILoader
{ 
    public LoaderKind Kind => LoaderKind.MelonMono;

    public string ConfigPath => MelonEnvironment.UserDataDirectory;
    public string AssemblyPath => Path.GetDirectoryName(MelonAssembly.Location);

    public override void OnInitializeMelon()
    {
        GameObject gameObject = new ();
        Object.DontDestroyOnLoad(gameObject);
        gameObject.hideFlags = HideFlags.HideAndDontSave;
        GraphicsDeviceType graphicsDeviceType = SystemInfo.graphicsDeviceType;
        if (!DearImGuiInjectionCore.Init(this, (int)graphicsDeviceType, graphicsDeviceType.ToString()))
            return;
        gameObject.AddComponent<UnityMainThreadDispatcher>();
    }

    public override void OnDeinitializeMelon() => DearImGuiInjectionCore.Dispose();

    public void CreateConfig<T>(ref IConfigEntry<T> configEntry, string category, string key, T defaultValue, string description) {
        MelonPreferences_Category confCategory = MelonPreferences.CreateCategory(category);
        confCategory.SetFilePath(Path.Combine(ConfigPath, "DearImGuiInjection.cfg"));
        configEntry = new ConfigEntryMelonLoader<T>(confCategory.CreateEntry(key, defaultValue));
    }

    public void SaveConfig() {
        MelonPreferences.Save();
    }
    
    public void Debug(object data) => MelonLogger.Msg(data);
    public void Error(object data) => MelonLogger.Error(data);
    public void Fatal(object data) => MelonLogger.Error(data);
    public new void Info(object data) => MelonLogger.Msg(data);
    public void Message(object data) => MelonLogger.Msg(data);
    public void Warning(object data) => MelonLogger.Warning(data);
}