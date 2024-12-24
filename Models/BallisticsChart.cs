namespace nugsnet6.Models
{
    class BallisticsChart
    {
        string type { get; set; } = "bar";
        bool responsive { get; set; } = true;
        Data data { get; set; }
        Options options { get; set; }
    }

    class Options
    {
        Scales scales { get; set; }
    }

    class Scales
    {
        YAx[] yAxes { get; set; }
    }

    class YAx
    {
        Ticks ticks { get; set; }
    }

    class Ticks
    {
        bool beginAtZero { get; set; } = true;
    }

    class Data
    {
        string[] labels { get; set; } =
            new[] { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" };
        Dataset[] datasets { get; set; }
    }

    class Dataset
    {
        string label { get; set; }
        int[] data { get; set; }

        string[] backgroundColor { get; set; } =
            new string[]
            {
                "rgba(255, 99, 132, 0.2)",
                "rgba(54, 162, 235, 0.2)",
                "rgba(255, 206, 86, 0.2)",
                "rgba(75, 192, 192, 0.2)",
                "rgba(153, 102, 255, 0.2)",
                "rgba(255, 159, 64, 0.2)",
            };

        string[] borderColor { get; set; } =
            new string[]
            {
                "rgba(255, 99, 132, 1)",
                "rgba(54, 162, 235, 1)",
                "rgba(255, 206, 86, 1)",
                "rgba(75, 192, 192, 1)",
                "rgba(153, 102, 255, 1)",
                "rgba(255, 159, 64, 1)",
            };

        int borderWidth { get; set; }
    }
}
