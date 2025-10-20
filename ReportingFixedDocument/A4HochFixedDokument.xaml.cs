namespace ReportingFixedDocument
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Printing;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Xps;
    using System.Windows.Xps.Packaging;

    using Microsoft.Win32;

    using ReportingLibrary.Extension;
    using ReportingLibrary.Report;

    using static System.Net.WebRequestMethods;

    /// <summary>
    /// Interaktionslogik für A4HochFixedDokument.xaml
    /// </summary>
    public partial class A4HochFixedDokument : Window
    {
        private FixedDocument _fixedDocument;
        private Size paperSize = ReportPaperSize.A4Portrait();

        public A4HochFixedDokument()
        {
            this.InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            const int PAGEROW = 10;
            List<MyItem> demoData = DemoDaten.Create();

            FixedDocument document = new FixedDocument();
            DocumentPaginator paginator = ((IDocumentPaginatorSource)document).DocumentPaginator;
            paginator = new DefaultDocumentPaginator(paginator, paperSize);

            int page = (int)Math.Round((demoData.Count / (double)PAGEROW),MidpointRounding.AwayFromZero);
            for (int i = 0; i < page; i++)
            {
                Range range = new Range(i* PAGEROW, (i+1) * PAGEROW);
                List<MyItem> printSource = demoData.Take(range).ToList();
                if (printSource != null && printSource.Count > 0)
                {
                    FixedPage page1 = CreatePage(document, printSource);

                    PageContent page1Content = new PageContent();
                    ((IAddChild)page1Content).AddChild(page1);
                    document.Pages.Add(page1Content);
                }
            }

            this._fixedDocument = document;
            this.rootDoc.Document = document;
        }

        private static FixedPage CreatePage(FixedDocument document, List<MyItem> source)
        {
            const double TOP = 73;
            const double LEFT = 40;

            string packUri = "pack://application:,,,/ReportingFixedDocument;component/Resources/Picture/ApplicationIcon.png";
            ImageSource imageSource = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;

            FixedPage resultPage = new FixedPage();
            resultPage.Width = document.DocumentPaginator.PageSize.Width;
            resultPage.Height = document.DocumentPaginator.PageSize.Height;

            resultPage.ReportLabel("Artikelnummer: 99999", Brushes.Blue, 14, FontWeights.Bold, new Thickness(45, 45, 0, 0));
            resultPage.ReportLabel("Überschrift", new Thickness(300, 45, 0, 0));
            resultPage.Image(imageSource, 50,new Thickness(resultPage.Width-100,TOP-50, 0,0));
            resultPage.Line(TOP, LEFT, resultPage.Width-50, 2);
            resultPage.ReportLabel("Titel kurz 1", Brushes.Black, 12, FontWeights.Bold, new Thickness(45, 80, 0, 0));
            resultPage.ReportLabel("Titel kurz 2", Brushes.Black, 12, FontWeights.Bold, new Thickness(45, 100, 0, 0));

            ListView lv = new ListView();
            lv.BorderBrush = Brushes.Black;
            lv.BorderThickness = new Thickness(0);

            GridView gv = new GridView();
            GridViewColumn gvc1 = new GridViewColumn();
            gvc1.Width = 50;
            gvc1.DisplayMemberBinding = new Binding("Id");
            gvc1.Header = "Id";
            gv.Columns.Add(gvc1);

            GridViewColumn gvc2 = new GridViewColumn();
            gvc2.Width = 200;
            gvc2.DisplayMemberBinding = new Binding("Vorname");
            gvc2.Header = "Vorname";
            gv.Columns.Add(gvc2);

            GridViewColumn gvc3 = new GridViewColumn();
            gvc3.Width = 200;
            gvc3.Header = "Nachname";
            gvc3.CellTemplate = GetTextBlockDataTemplate("Nachname");
            gv.Columns.Add(gvc3);

            GridViewColumn gvc4 = new GridViewColumn();
            gvc4.Width = 50;
            gvc4.Header = "Aktiv";
            gvc4.CellTemplate = GetCheckBoxDataTemplate("Aktiv");
            gv.Columns.Add(gvc4);

            lv.View = gv;
            lv.Margin = new Thickness(45, 130, 0, 0);
            lv.ItemsSource = source;

            resultPage.Children.Add(lv);

            resultPage.AddFooterLeft(DateTime.Now.ToString(CultureInfo.CurrentCulture));
            resultPage.AddFooterCenter("Report: FixedPage");
            resultPage.AddFooterRight($"Seite : {document.DocumentPaginator.PageCount + 1}");

            return resultPage;
        }

        public void OnPrint(object sender, RoutedEventArgs e)
        {
            var printDialog = new PrintDialog();
            printDialog.UserPageRangeEnabled = true;
            printDialog.CurrentPageEnabled = true;
            printDialog.PrintTicket.PageOrientation = PageOrientation.Portrait;

            if (printDialog.ShowDialog() == true)
            {
                DocumentPaginator paginator = _fixedDocument.DocumentPaginator;

                if (printDialog.PageRangeSelection == PageRangeSelection.UserPages)
                {
                    paginator = new PageRangeDocumentPaginator(_fixedDocument.DocumentPaginator, printDialog.PageRange);
                }

                printDialog.PrintDocument(paginator, "FixedDocument");
            }
        }

        private void CommandBindingCanOnOpen(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }

        public void CommandBindingOnOpen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Report Dokument (*.xps)|*.xps|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Report Dokument";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                XpsDocument document = new XpsDocument(openFileDialog.FileName, FileAccess.Read);
                FixedDocumentSequence sequence = document.GetFixedDocumentSequence();
                this.rootDoc.Document = sequence.References[0].GetDocument(false).DocumentPaginator.Source;
            }
        }

        private void CommandBindingCanExecuteOnSave(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }

        public void CommandBindingOnSave(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Report Dokument (*.xps)|*.xps|All files (*.*)|*.*";
            saveFileDialog.Title = "Report Dokument";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.FileName = $"FixedDocument_{DateTime.Today.ToString("yyyyMMdd",CultureInfo.CurrentCulture)}";
            if (saveFileDialog.ShowDialog() == true)
            {
                DocumentPaginator paginator = _fixedDocument.DocumentPaginator;
                XpsDocument xpsDocument = new XpsDocument(saveFileDialog.FileName, FileAccess.Write);
                XpsDocumentWriter documentWriter = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
                documentWriter.Write(paginator);
                xpsDocument.Close();
            }
        }

        private static DataTemplate GetTextBlockDataTemplate(string colName)
        {
            FrameworkElementFactory textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            textBlockFactory.SetBinding(TextBlock.TextProperty, new Binding(colName));
            textBlockFactory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Left);
            textBlockFactory.SetValue(TextBlock.WidthProperty, 190.0);
            textBlockFactory.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
            textBlockFactory.SetValue(TextBlock.BackgroundProperty, Brushes.LightGray);
            DataTemplate template = new DataTemplate();
            template.VisualTree = textBlockFactory;

            return template;
        }

        private static DataTemplate GetCheckBoxDataTemplate(string colName)
        {
            FrameworkElementFactory textBlockFactory = new FrameworkElementFactory(typeof(CheckBox));
            textBlockFactory.SetBinding(CheckBox.IsCheckedProperty, new Binding(colName));
            textBlockFactory.SetValue(CheckBox.IsHitTestVisibleProperty, false);
            textBlockFactory.SetValue(CheckBox.WidthProperty, 45.0);
            textBlockFactory.SetValue(CheckBox.BackgroundProperty, Brushes.LightGray);
            DataTemplate template = new DataTemplate();
            template.VisualTree = textBlockFactory;

            return template;
        }
    }
}