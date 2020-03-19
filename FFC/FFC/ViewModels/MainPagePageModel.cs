using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using FFC.Models;
using FFC.Utilities;
using Prism.Commands;
using Prism.Mvvm;

namespace FFC.ViewModels
{
    public class MainPagePageModel : BindableBase
    {
        public MainPagePageModel()
        { }

        private int _x;
        private int _y;
        private int _rssi;
        
        public int XValue
        {
            get => _x;
            set => SetProperty(ref _x, value);
        }

        public int YValue
        {
            get => _y;
            set => SetProperty(ref _y, value);
        }

        public void AddReferencePointToDatabase(int x, int y, int rssi)
        {
            using (var db = new ReferenceContext())
            {
                db.Add(new Reference { xPoint = x, yPoint = y, RSSI = rssi });
            }
        }
        public void Button_OnClicked(object sender, EventArgs e)
        {
            AddReferencePointToDatabase(_x, _y, _rssi);

        }
    }
}
