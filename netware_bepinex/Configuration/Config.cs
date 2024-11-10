using NetWare.Attributes;
using NetWare.Configuration.Configs.Legit;
using NetWare.Configuration.Configs.Settings;
using NetWare.Configuration.Configs.Visual;
using NetWare.Extensions;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace NetWare.Configuration;

public sealed class Config
{
    public static string ConfigFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).CombineWith("NetWare", "configs");
    private const string ConfigExtension = ".nwc";

    // constructor
    static Config()
    {
        Active = new();
        Active.GetBindables();
    }

    // data
    public static Config Active { get; set; }
    public static List<string> ConfigNames = [];

    // config
    private static PropertyInfo[] _cachedBinadableTypes;
    public List<IBindable> Bindables = [];

    [ConfigProperty] public AimbotConfig Aimbot { get; set; } = new();
    [ConfigProperty] public SilentAimConfig SilentAim { get; set; } = new();

    [ConfigProperty] public NameTagsConfig NameTags { get; set; } = new();
    [ConfigProperty] public BoxesConfig Boxes { get; set; } = new();
    [ConfigProperty] public SkeletonConfig Skeleton { get; set; } = new();
    [ConfigProperty] public TracersConfig Tracers { get; set; } = new();
    [ConfigProperty] public FovChangerConfig FovChanger { get; set; } = new();
    [ConfigProperty] public CameraSettingsConfig CameraSettings { get; set; } = new();
     
    [ConfigProperty] public FpsCapperConfig FpsCapper { get; set; } = new();

    // main methods
    public static void Setup()
    {
        if (!Directory.Exists(ConfigFolder))
            Directory.CreateDirectory(ConfigFolder);
        UpdateInternal();
    }
    public static void Load(string configName)
    {
        // get and check config path
        if (string.IsNullOrEmpty(configName))
            return;
        var configPath = ConfigFolder
            .CombineWith(configName)
            .ChangeExtension(ConfigExtension);
        if (!File.Exists(configPath))
            return;
        
        // load config
        string data = File.ReadAllText(configPath);
        var deserialized = ConfigDeserializer.Deserialize<Config>(data);

        if (deserialized is not null)
        {
            Active = deserialized;
            Active.GetBindables();
        }
    }
    public static void Save(string configName)
    {
        // get and check config path
        if (string.IsNullOrEmpty(configName))
            return;
        var configPath = ConfigFolder
            .CombineWith(configName)
            .ChangeExtension(ConfigExtension);
        
        // save config
        File.WriteAllText(
            configPath,
            ConfigSerializer.Serialize(Active)
        );
        UpdateInternal();
    }
    public static void Delete(string configName)
    {
        // get and check config path
        if (string.IsNullOrEmpty(configName))
            return;
        var configPath = ConfigFolder
            .CombineWith(configName)
            .ChangeExtension(ConfigExtension);

        // delete config
        if (File.Exists(configPath))
            File.Delete(configPath);
        UpdateInternal();
    }

    // config selector updater
    public static IEnumerator Update()
    {
        while (true)
        {
            UpdateInternal();
            yield return new WaitForSeconds(1);
        }
    }
    private static void UpdateInternal()
    {
        var files = Directory
            .GetFiles(ConfigFolder)
            .AsQueryable()
            .Where(x => Path.GetExtension(x).Equals(ConfigExtension, StringComparison.OrdinalIgnoreCase))
            .Select(Path.GetFileNameWithoutExtension);

        ConfigNames.Clear();
        ConfigNames.AddRange(files);
    }

    // other
    private void GetBindables()
    {
        _cachedBinadableTypes ??= typeof(Config)
            .GetProperties()
            .Where(x => typeof(IBindable).IsAssignableFrom(x.PropertyType))
            .ToArray();

        foreach (var bindableType in _cachedBinadableTypes)
            Bindables.Add((IBindable)bindableType.GetValue(this));
    }
}