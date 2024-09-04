using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbstractState : IScreenState
{

    readonly List<ILink> m_Links = new List<ILink>();

    public virtual void Enter()
    {
    }

    public IEnumerator Execute()
    {
        yield return null;
    }

    public virtual void Exit()
    {
    }

    public virtual void AddLink()
    {
        throw new System.NotImplementedException();
    }

    public virtual void RemoveAllLinks()
    {
        throw new System.NotImplementedException();
    }

    public virtual bool ValidateLinks(out IScreenState nextState)
    {

        if (m_Links != null && m_Links.Count > 0)
        {
            foreach (ILink link in m_Links)
            {
                bool result = link.Validate(out nextState);
                if (result)
                {
                    return true;
                }
            }
        }

        // By default, return false without a valid IState
        nextState = null;
        return false;
    }
}
