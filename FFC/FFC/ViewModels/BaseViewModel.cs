using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FFC.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
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

        #region Commands

        #endregion

        #region Methods

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
