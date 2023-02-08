using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Controller
{
    public interface IRaycastable
    {
        CursorType GetCursorType();
        bool HandleRaycast(PlayerController callingController);
    }
}

