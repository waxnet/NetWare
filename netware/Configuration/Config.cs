using NetWare.Attributes;
using NetWare.Configuration.Subtypes.Combat;
using NetWare.Configuration.Subtypes.Settings;
using NetWare.Configuration.Subtypes.Visual;
using NetWare.Constants;
using NetWare.Extensions;
using NetWare.Utils;
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
    static Config()
    {
        Active = new();
        Active.GetBindables();
    }

    public static Config Active { get; set; }
    public static List<string> ConfigNames = [];

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
     
    [ConfigProperty] public WatermarkConfig Watermark { get; set; } = new();
    [ConfigProperty]  public FpsCapperConfig FpsCapper { get; set; } = new();

    public static void Setup()
    {
        if (!Directory.Exists(ConfigConstants.ConfigFolder))
            Directory.CreateDirectory(ConfigConstants.ConfigFolder);

        UpdateConfigsInternal();
    }

    public static void Load(string configName)
    {
        var configPath = GetConfigPath(configName);

        if (!File.Exists(configPath))
            return;
        
        var data = File.ReadAllText(configPath);
        var deserialized = ConfigDeserializer.Deserialize<Config>(data);

        if (deserialized is not null)
        {
            Active = deserialized;
            Active.GetBindables();
        }
    }

    public static void Save(string configName)
    {
        var configPath = GetConfigPath(configName);
        var serialized = ConfigSerializer.Serialize(Active);
        File.WriteAllText(configPath, serialized);

        UpdateConfigsInternal();
    }

    public static void Delete(string configName)
    {
        var configPath = GetConfigPath(configName);

        if (File.Exists(configPath))
            File.Delete(configPath);

        UpdateConfigsInternal();
    }

    public static void OpenConfigFolder()
    {
        SystemUtils.OpenFolder(ConfigConstants.ConfigFolder);
    }

    public static IEnumerator UpdateConfigs()
    {
        while (true)
        {
            UpdateConfigsInternal();
            yield return new WaitForSeconds(1);
        }
    }

    private static void UpdateConfigsInternal()
    {
        var files = Directory
            .GetFiles(ConfigConstants.ConfigFolder)
            .AsQueryable()
            .Where(x => Path.GetExtension(x).Equals(ConfigConstants.ConfigExtension, StringComparison.OrdinalIgnoreCase))
            .Select(Path.GetFileNameWithoutExtension);

        ConfigNames.Clear();
        ConfigNames.AddRange(files);
    }

    private static string GetConfigPath(string configName)
    {
        return ConfigConstants.ConfigFolder.CombineWith(configName).ChangeExtension(ConfigConstants.ConfigExtension);
    }

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