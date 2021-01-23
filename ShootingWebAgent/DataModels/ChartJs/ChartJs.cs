namespace ShootingWebAgent.DataModels
{
    public class ChartJs
    {
        public string type { get; set; }
        public int duration { get; set; }
        public string easing { get; set; }
        public bool responsive { get; set; }
        public Title title { get; set; }
        public bool lazy { get; set; }
        public Data data { get; set; }
        public Options options { get; set; }

        public class Title
        {
            public bool display { get; set; }
            public string text { get; set; }
        }
        
        public class Data
        {
            public string[] labels { get; set; }
            public Dataset[] datasets { get; set; }
        }

        public class Dataset
        {
            public string label { get; set; }
            public int[] data { get; set; }
            public string[] backgroundColor { get; set; }
            public string[] borderColor { get; set; }
            public int borderWidth { get; set; }
            public string yAxisID { get; set; }
            public string xAxisID { get; set; }
        }

        public class Options
        {
            public Scales scales { get; set; }
        }

        public class Scales
        {
            public yAxes[] yAxes { get; set; }
            public xAxes[] xAxes { get; set; }
        }

        public class Yax
        {
            public Ticks ticks { get; set; }
        }

        public class Ticks
        {
            public bool beginAtZero { get; set; }
        }
        
        public class xAxes
        {
            public string id { get; set; }
            public bool display { get; set; }
            public string type { get; set; }
            public Ticks ticks { get; set; }
        }
        
        public class yAxes
        {
            public string id { get; set; }
            public bool display { get; set; }
            public string type { get; set; }
            public Ticks ticks { get; set; }
        }
    }
}