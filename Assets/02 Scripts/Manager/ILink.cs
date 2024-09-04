using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILink
{
    public abstract bool Validate(out IScreenState nextState);

    public void Enabled() {}

    public void Disable() {}
}
