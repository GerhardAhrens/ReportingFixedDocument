//-----------------------------------------------------------------------
// <copyright file="ReportLine.cs" company="Lifeprojects.de">
//     Class: ReportLine
//     Copyright © Lifeprojects.de 2025
// </copyright>
//
// <author>Gerhard Ahrens - Lifeprojects.de</author>
// <email>developer@lifeprojects.de</email>
// <date>16.10.2025 14:46:37</date>
//
// <summary>
// Klasse für 
// </summary>
//-----------------------------------------------------------------------

namespace ReportingLibrary.Report
{
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    // Fix: "Line" ist ein versiegelter (sealed) Typ und kann nicht abgeleitet werden.
    // Lösung: Verwende Komposition statt Vererbung.
    public class ReportLine
    {
        private readonly Line _line;

        public ReportLine(Brush foreground,double top, double startLeft, double pageWidth, double height)
        {
            _line = new Line();
            _line.Stroke = foreground;
            _line.X1 = startLeft;
            _line.Y1 = top;
            _line.X2 = pageWidth - startLeft;
            _line.Y2 = top;
            _line.HorizontalAlignment = HorizontalAlignment.Left;
            _line.VerticalAlignment = VerticalAlignment.Center;
            _line.StrokeThickness = height;
        }

        public ReportLine(double top, double startLeft, double pageWidth, double height)
        {
            _line = new Line();
            _line.Stroke = Brushes.Black;
            _line.X1 = startLeft;
            _line.Y1 = top;
            _line.X2 = pageWidth - startLeft;
            _line.Y2 = top;
            _line.HorizontalAlignment = HorizontalAlignment.Left;
            _line.VerticalAlignment = VerticalAlignment.Center;
            _line.StrokeThickness = height;
        }

        /// <summary>
        /// Gibt die interne Line-Instanz zurück.
        /// </summary>
        public Line Line => _line;
    }
}
