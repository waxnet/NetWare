using NetWare.Attributes;
using NetWare.Modules.MenuTabs;
using NetWare.Modules.Tabs.VisualTab;
using NetWare.UI;

namespace NetWare.Modules.Tabs;

[MenuTab("Visual", 2)]
public sealed class Visual : MenuTab
{
    public Visual() : base()
    {
    }

    public override void Tab()
    {
        Menu.Begin();

        NameTagsSection.Draw();
        SkeletonSection.Draw();
        CameraSettingsSection.Draw();

        Menu.Separate();

        TracersSection.Draw();
        BoxesSection.Draw();
        FovChangerSection.Draw();

        Menu.End();
    }
}
