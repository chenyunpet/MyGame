using System;
using System.Collections.Generic;
using UnityEngine;
public class GOAPActionGoTo : GOAPAction
{
    public GOAPActionGoTo(Agent owner) : base(E_GOAPAction.gotoPos, owner)
    {

    }
    public override void InitAction()
    {
        throw new NotImplementedException();
    }

}


