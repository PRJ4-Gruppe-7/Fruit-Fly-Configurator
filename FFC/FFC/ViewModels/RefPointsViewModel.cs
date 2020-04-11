using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FFC.Models;

namespace FFC.ViewModels
{
    public class RefPointsViewModel : BaseViewModel
    {
        public List<ListPoints> Points { get; set; }

        public RefPointsViewModel()
        {
            Title = "Uploaded Reference Points";
            Points = new ListPoints().GetPoints();
        }

    }
}
