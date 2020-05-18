using System;
using System.Collections.Generic;
using System.Text;
using FFC.Services;

namespace FFC.Models
{
    public class Coordinates : ObservableObject
    {
        #region Fields

        private uint _xValue { get; set; }
        private uint _yValue { get; set; }

        #endregion

        #region Properties

        public uint XValue
        {
            get { return _xValue; }
            set
            {
                try
                {
                    //Validates entry to make sure it isn't negative or extends 
                    //maximum value of an integer
                    _xValue = value < UInt32.MaxValue ? value : _xValue;
                    NotifyPropertyChanged(nameof(XValue));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex}");
                }
            }
        }

        public uint YValue
        {
            get { return _yValue; }
            set
            {
                try
                {
                    //Validates entry to make sure it isn't negative or extends 
                    //maximum value of an integer
                    _yValue = value < UInt32.MaxValue ? value : _yValue;
                    NotifyPropertyChanged(nameof(YValue));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex}");
                }
            }
        }

        #endregion

    }
}
