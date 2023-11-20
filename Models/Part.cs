// Generated by https://quicktype.io
//
// To change quicktype's target language, run command:
//
//   "Set quicktype target language"

using CodeMechanic.Extensions;
using NSpecifications;

namespace nugsnet6.Models;

public class Part
{
    public string Id { get; set; } = string.Empty;
    public DateTime createdTime { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Kind { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string ProductCode { get; set; } = string.Empty;

    public double Cost { get; set; }
    public double WeightInOz { get; set; }
    public Attachment[] Attachments { get; set; }
    public string[] Calibers { set; get; }
    public string[] Builds { set; get; }
    public Uri Url { get; set; }
    public Uri Demo { get; set; }
    public long ComboCost { get; set; }
    public FakeUser CreatedBy { get; set; }
    public DateTimeOffset Created { get; set; }
    public object Combo { get; set; }
    public FakeUser LastModifiedBy { get; set; }
    public DateTimeOffset LastModified { get; set; }

    public static Maybe<Part> NotFound = new Maybe<Part>(new Part()
    {
        Id = string.Empty, Name = "Not Available", Cost = -1, ComboCost = -1
    });

    public static ISpecification<Part> IsValid => new Spec<Part>(part => part.Cost > 0
                                                                         && NotFound.IfSome(_ =>
                                                                             part.Equals(NotFound.Value)));
}

// public class Thumbnails
// {

//     public Full Small { get; set; }


//     public Full Large { get; set; }


//     public Full Full { get; set; }
// }

// public  class Full
// {

//     public Uri Url { get; set; }


//     public long Width { get; set; }


//     public long Height { get; set; }
// }