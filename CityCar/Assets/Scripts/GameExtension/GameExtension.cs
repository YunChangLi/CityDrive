using System.Collections;
using System.Collections.Generic;
using System.Linq;
using I2.Loc;
using UnityEngine;

public static class GameExtension  {

    public static string GetCurrentCultureValue(params string[] key)
    {
        return string.Join("", key.Select(p =>
        {
            var translate = LocalizationManager.GetTranslation(p);
            if (p != null && string.IsNullOrEmpty(translate))
            {
                return p;
            }
            return translate;
        }));
    }
}
