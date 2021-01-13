using UnityEngine;
using System.Collections.Generic;

public class bl_HudPath : MonoBehaviour
{

    public List<bl_Hud> AllHuds = new List<bl_Hud>();
    [SerializeField] private bool ShowFirstOnStart = true;

    private int CurrentHud = 0;

    private static bl_HudPath _instance;
    public static bl_HudPath instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<bl_HudPath>();
            }
            return _instance;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
       foreach(bl_Hud h in AllHuds) { h.Hide(); }
        if (ShowFirstOnStart)
        {
            AllHuds[0].Show();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ReachedHud(bl_HudInfo h)
    {
        bl_HudManager.instance.HideStateHud(h, true);
        if(h.ReachSound != null) { AudioSource.PlayClipAtPoint(h.ReachSound, transform.position); }
        CurrentHud++;
        if (CurrentHud <= AllHuds.Count - 1)
        {
            AllHuds[CurrentHud].Show();
        }
        else
        {
            Debug.Log("All huds in path reached!");
        }
    }
}