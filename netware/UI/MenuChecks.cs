using UnityEngine;
using NetWare.Configuration;
using NetWare.Attributes;

namespace NetWare.UI;

[NetWareComponent]
public sealed class MenuChecks : MonoBehaviour
{
    public void Update()
    {
        if (!Input.anyKey)
            return;

        foreach (var bindable in Config.Active.Bindables)
        {
            if (bindable.KeyBind is not null && Input.GetKeyDown(bindable.KeyBind.Value))
                bindable.Enabled = !bindable.Enabled;
        }
    }
}
