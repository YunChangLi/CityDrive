using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IPlayerStartTest 
{
     IEnumerator StartGameLogic(Func<bool> Input);


    void EndLogic();
}
