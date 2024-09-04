using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScreenState
{
    public abstract void Enter();

    public abstract IEnumerator Execute();

    public abstract void AddLink();

    public abstract void Exit();

    public abstract void RemoveAllLinks();
}
