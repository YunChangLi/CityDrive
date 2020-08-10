using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineData 
{

    public List<Data> Linelist;

    public class Data
    {
        public string LineName { get; set; }

        public string Route { get; set; }

        public string LineText { get; set; }

        public string Difficult { get; set; }
    }
}
