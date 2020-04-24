using System;
using System.Collections.Generic;
using System.Linq;

namespace nswag_liquid_slim.ViewModels
{
    public class GridParameters
    {
        public string Filter { get; set; } = "";

        public string? SortBy { get; set; }

        public bool IncludeCount { get; set; } = false;

        public SortDirection SortDirection { get; set; } = SortDirection.Asc;

        public int Page { get; set; } = 0;

        public int PageSize { get; set; } = 10;

        internal string? SortExpression =>
            SortBy == null 
            ? null : $"{SortBy} {SortDirection}";

        internal int RecordIndex =>
            Page * PageSize;

        internal bool HasFilter =>
            !string.IsNullOrWhiteSpace(Filter) &&
            Filter.Length >= 3;
    }

    public enum SortDirection
    {
        Asc,
        Desc
    }

    public class GridData<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

        public int? Total { get; set; }
    }
}