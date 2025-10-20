namespace ReportingLibrary.Report
{
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;

    public class DefaultDocumentPaginator : DocumentPaginator
    {
        private Size _PageSize;
        private Size _Margin;
        private DocumentPaginator _Paginator;

        public DefaultDocumentPaginator(DocumentPaginator paginator, Size pageSize, Size margin)
        {
            _PageSize = pageSize;
            _Margin = margin;
            _Paginator = paginator;
            _Paginator.PageSize = new Size(_PageSize.Width - margin.Width * 2,
                                            _PageSize.Height - margin.Height * 2);
        }

        public DefaultDocumentPaginator(DocumentPaginator paginator, Size pageSize)
        {
            Size margin = new Size(0, 0);
            _PageSize = pageSize;
            _Margin = margin;
            _Paginator = paginator;
            _Paginator.PageSize = new Size(_PageSize.Width - margin.Width * 2,
                                            _PageSize.Height - margin.Height * 2);
        }

        Rect Move(Rect rect)
        {
            if (rect.IsEmpty)
            {
                return rect;
            }
            else
            {
                return new Rect(rect.Left + _Margin.Width, rect.Top + _Margin.Height, rect.Width, rect.Height);
            }
        }

        public override DocumentPage GetPage(int pageNumber)
        {
            return _Paginator.GetPage(pageNumber + pageNumber);
        }

        public override bool IsPageCountValid
        {
            get
            {
                return _Paginator.IsPageCountValid;
            }
        }

        public override int PageCount
        {
            get
            {
                return _Paginator.PageCount;
            }
        }

        public override Size PageSize
        {
            get
            {
                return _Paginator.PageSize;
            }
            set
            {
                if (this == _Paginator)
                {
                    this._PageSize = value;
                }
                else
                {
                    _Paginator.PageSize = value;
                }
            }
        }

        public override IDocumentPaginatorSource Source
        {
            get
            {
                return _Paginator.Source;
            }
        }
    }
}
