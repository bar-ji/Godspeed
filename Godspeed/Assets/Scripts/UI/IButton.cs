using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public interface IButton
    {
        void OnCursorEnter();
        void OnCursorExit();
        void OnClick();
    }
}

