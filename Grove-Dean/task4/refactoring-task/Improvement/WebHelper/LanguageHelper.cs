using System;

public static class LanguageHelper
{
    /// <summary>
    /// Returns the current language string for a specified xml language file entry.
    /// </summary>
    /// <param name="xmlPath">The path to the string in the xml file.</param>
    /// <returns></returns>
    public static string GetLanguageString(string xmlPath)
    {
        return EPiServer.Core.LanguageManager.Instance.Translate(xmlPath, GetCurrentLanguage());
    }

    /// <summary>
    /// Returns the current language as a two letter code (no or en for instance).
    /// </summary>
    /// <returns></returns>
    public static string GetCurrentLanguage()
    {
        return EPiServer.Globalization.ContentLanguage.PreferredCulture.Name;
    }
}