using System;
using System.Collections.Generic;

namespace Admin.Web.Models
{
    public class DtResult
    {
        public int draw { get; set; }
        public long recordsTotal { get; set; }
        public long recordsFiltered { get; set; }
        public object data { get; set; }
    }
    public class DtResult<T>
    {
        public int draw { get; set; }
        public long recordsTotal { get; set; }
        public long recordsFiltered { get; set; }
        public List<T> data { get; set; }
    }
    public abstract class DTRow
    {
        public virtual string DT_RowId
        {
            get { return null; }
        }
        public virtual string DT_RowClass
        {
            get { return null; }
        }
        public virtual object DT_RowData
        {
            get { return null; }
        }
    }

    public class DTParameters
    {
        public int PageIndex
        {
            get
            {
                if (Length == 0)
                    return 0;
                return Start / Length;
            }
        }
        public int Draw { get; set; }
        public DTColumn[] Columns { get; set; }
        public DTOrder[] Order { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public DTSearch Search { get; set; }
        public string SortOrder
        {
            get
            {
                return Columns != null && Order != null && Order.Length > 0
                    ? (Columns[Order[0].Column].Data + (Order[0].Dir == DTOrderDir.DESC ? " " + Order[0].Dir : string.Empty))
                    : null;
            }
        }

    }
    public class DTColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool Searchable { get; set; }
        public bool Orderable { get; set; }
        public DTSearch Search { get; set; }
    }
    public class DTOrder
    {
        public int Column { get; set; }
        public DTOrderDir Dir { get; set; }
    }
    public enum DTOrderDir
    {
        ASC,
        DESC
    }
    public class DTSearch
    {
        public string Value { get; set; }
        public bool Regex { get; set; }
    }

    public class DataList<T>
    {
        public long TotalCount { get; set; }
        public long FilteredCount { get; set; }
        public List<T> Items { get; set; }
    }
}
