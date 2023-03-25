// Generated by https://quicktype.io
//
// To change quicktype's target language, run command:
//
//   "Set quicktype target language"

namespace nugsnet6.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Loadout
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Inspiration { get; set; } = string.Empty;
        public string[] Kit { get; set; } = Enumerable.Empty<string>().ToArray();
        public string[] PrimaryArm { get; set; } = Enumerable.Empty<string>().ToArray();
        public string[] Sidearm { get; set; } = Enumerable.Empty<string>().ToArray();
        public string[] UrlFromPart { get; set; } = Enumerable.Empty<string>().ToArray();
        // public DateTimeOffset CreatedTime { get; set; }
        // public AttachmentsFromPart[] AttachmentsFromPart { get; set; }
    }
}
