using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using FFC.Services;

namespace FFC.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        #region Properties

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }

        #endregion
    }
}
