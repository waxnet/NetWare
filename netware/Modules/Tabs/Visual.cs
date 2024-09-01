using NetWare.Attributes;
using NetWare.Modules.MenuTabs;
using NetWare.Modules.Tabs.VisualTab;
using NetWare.UI;

namespace NetWare.Modules.Tabs;

[Attributes.MenuTabAttribute("Visual", 1)]
public sealed class Visual : MenuTabs.MenuTab
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
