//-----------------------------------------------------------------------
// <copyright file="MainWindow.cs" company="Lifeprojects.de">
//     Class: MainWindow
//     Copyright © Lifeprojects.de yyyy
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>dd.MM.yyyy</date>
//
// <summary>
// Klasse für 
// </summary>
//-----------------------------------------------------------------------

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace ReportingFixedDocument
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            this.InitializeComponent();
            WeakEventManager<Window, RoutedEventArgs>.AddHandler(this, "Loaded", this.OnLoaded);

            this.WindowTitel = "Minimal WPF Template";
            this.DataContext = this;
        }

        private string _WindowTitel;

        public string WindowTitel
        {
            get { return _WindowTitel; }
            set
            {
                if (this._WindowTitel != value)
                {
                    this._WindowTitel = value;
                    this.OnPropertyChanged();
                }
            }
        }


        private void OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        private void BtnOpenReportClick(object sender, RoutedEventArgs e)
        {
            string btn = ((Button)sender).Name;
            if (btn == "BtnA4QuerClick")
            {
                A4QuerFixedDokument fdoc = new A4QuerFixedDokument();
                fdoc.Owner = this.Owner;
                fdoc.ShowDialog();
            }
            else if (btn == "BtnA4HochClick")
            {
                A4HochFixedDokument fdoc = new A4HochFixedDokument();
                fdoc.Owner = this.Owner;
                fdoc.ShowDialog();
            }
        }

        #region INotifyPropertyChanged implementierung
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler == null)
            {
                return;
            }

            var e = new PropertyChangedEventArgs(propertyName);
            handler(this, e);
        }
        #endregion INotifyPropertyChanged implementierung
    }
}