using System;
using System.Collections.Generic;
using System.Text;

namespace FFC.Models
{
    public class ListPoints
    {
        public int PointID { get; set; }
        public string CoordinateSet { get; set; }


        public List<ListPoints> GetPoints()
        {
            List<ListPoints> points = new List<ListPoints>()
            {
                new ListPoints() {PointID = 2, CoordinateSet="2,2"},
                new ListPoints() {PointID = 2, CoordinateSet="2,2"},
                new ListPoints() {PointID = 2, CoordinateSet="2,2"},
                new ListPoints() {PointID = 2, CoordinateSet="2,2"},
                new ListPoints() {PointID = 2, CoordinateSet="2,2"},
                new ListPoints() {PointID = 2, CoordinateSet="2,2"},
                new ListPoints() {PointID = 2, CoordinateSet="2,2"},
                new ListPoints() {PointID = 2, CoordinateSet="2,2"},
                new ListPoints() {PointID = 2, CoordinateSet="2,2"},
                new ListPoints() {PointID = 2, CoordinateSet="2,2"},
                new ListPoints() {PointID = 2, CoordinateSet="2,2"},
                new ListPoints() {PointID = 2, CoordinateSet="2,2"}
            };

            return points;
        }
    }
}
