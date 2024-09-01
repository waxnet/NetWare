using UnityEngine;

namespace NetWare.Configuration;

public interface IBindable
{
    bool Enabled { get; set; }
    KeyCode? KeyBind { get; set; } // null = currently assigning
}
