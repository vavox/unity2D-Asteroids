using System.Collections.Generic;
using UnityEngine.Events;

public static class EventManager
{
    #region Fields
    static List<Asteroid> addPointsInvokers = new List<Asteroid>();
    static List<UnityAction<int>> addPointsListeners = new List<UnityAction<int>>();

    static List<ShipController> drawHealthPointsInvokers = new List<ShipController>();
    static List<UnityAction<int>> drawHealthPointsListeners = new List<UnityAction<int>>();

    static List<SpawnManager> setHudWaveNumberInvokers = new List<SpawnManager>();
    static List<UnityAction<int>> setWaveNumberListeners = new List<UnityAction<int>>();
    #endregion

    #region Add Points
    public static void AddAddPointsInvoker(Asteroid invoker)
    {
        addPointsInvokers.Add(invoker);
        foreach(UnityAction<int> listener in addPointsListeners)
        {
            invoker.AddAddPointsListener(listener);
        }
    }

    public static void AddAddPointsListener(UnityAction<int> listener)
    {
        addPointsListeners.Add(listener);
        foreach(Asteroid invoker in addPointsInvokers)
        {
            invoker.AddAddPointsListener(listener);
        }
    }

    public static void RemoveAddPointsInvoker(Asteroid invoker)
    {
        addPointsInvokers.Remove(invoker);
    }
    #endregion

    #region Draw Health Points
    public static void AddDrawHealthPointsInvoker(ShipController invoker)
    {
        drawHealthPointsInvokers.Add(invoker);
        foreach(UnityAction<int> listener in drawHealthPointsListeners)
        {
            invoker.AddDrawHealthPointsListener(listener);
        }
    }

    public static void AddDrawHealthPointsListener(UnityAction<int> listener)
    {
        drawHealthPointsListeners.Add(listener);
        foreach(ShipController invoker in drawHealthPointsInvokers)
        {
            invoker.AddDrawHealthPointsListener(listener);
        }
    }

    public static void RemoveDrawHealthPointsInvoker(ShipController invoker)
    {
        drawHealthPointsInvokers.Remove(invoker);
    }
    #endregion

    #region Set Hud Wave Number
    public static void AddSetHudWaveNumberInvoker(SpawnManager invoker)
    {
        setHudWaveNumberInvokers.Add(invoker);
        foreach(UnityAction<int> listener in setWaveNumberListeners)
        {
            invoker.AddSetHudWaveNumberListener(listener);
        }
    }

    public static void AddSetHudWaveNumberListener(UnityAction<int> listener)
    {
        setWaveNumberListeners.Add(listener);
        foreach(SpawnManager invoker in setHudWaveNumberInvokers)
        {
            invoker.AddSetHudWaveNumberListener(listener);
        }
    }

    public static void RemoveSetHudWaveNumberInvoker(SpawnManager invoker)
    {
        setHudWaveNumberInvokers.Remove(invoker);
    }
    #endregion
}
