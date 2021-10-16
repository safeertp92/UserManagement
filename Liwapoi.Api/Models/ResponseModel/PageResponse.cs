using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liwapoi.Api.Models.ResponseModel
{
    public class PageResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public T Data { get; set; }

        public PageResponse()
        {

        }
        public PageResponse(T data, int pageNumber, int pageSize, int totalPages, int totalRecords)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }
    }
}
