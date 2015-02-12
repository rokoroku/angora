using UnityEngine;
using System.Collections;

public interface IDialog
{
    void OnPositiveClick();
    void OnNegativeClick();
    void OnCancel();    
}
