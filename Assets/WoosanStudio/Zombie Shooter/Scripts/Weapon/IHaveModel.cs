using UnityEngine;

namespace WoosanStudio.ZombieShooter
{
    public interface IHaveModel
    {
        GameObject MakeModel();
        GameObject PrefabInstance { get; }
    }
}