using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Jsons
{
    public readonly static string languageFolder = "Languages/";
    public static string language = "English/";
    static readonly string resPath = "Assets/Resources/";
    public readonly static string events = File.ReadAllText(resPath + "events.json");
    public readonly static string hormoneNames = File.ReadAllText(resPath + languageFolder + language + "hormones.json");
    public readonly static string eventTexts = File.ReadAllText(resPath + languageFolder + language + "events.json");
}

public static class Lists
{
    public static IList<HormoneData> events = new List<HormoneData>(JsonHelper.FromJson<HormoneData>(Jsons.events));
    public static IList<string> hormoneNames = new List<string>(JsonHelper.FromJson<string>(Jsons.hormoneNames));
    public static IList<EventText> eventTexts = new List<EventText>(JsonHelper.FromJson<EventText>(Jsons.eventTexts));
}

