using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.Moq.Entity
{
    public class BaseResult
    {
        public string Msg
        {
            get;
            set;
        }

        public bool IsSuccess
        {
            get;
            set;
        }

        public static DataResult<T> CreateDataResult<T>(T data, bool? success = null, string msg = "")
        {
            return new DataResult<T>
            {
                IsSuccess = (success ?? (data != null)),
                Msg = msg,
                Result = data
            };
        }

        public static ListResult<T> CreateListResult<T>(IEnumerable<T> datas, bool? success = null, string msg = "", int? total = null)
        {
            return new ListResult<T>
            {
                IsSuccess = (success ?? (datas != null)),
                Msg = msg,
                Result = datas,
                Total = (long)(total ?? ((datas != null) ? datas.Count<T>() : 0))
            };
        }
    }

    public abstract class BaseResult<T> : BaseResult
    {
        public T Result
        {
            get;
            set;
        }
    }

    public class DataResult<T> : BaseResult<T>
    {
    }

    public class ListResult<T> : BaseResult<IEnumerable<T>>
    {
        public long Total
        {
            get;
            set;
        }
    }
}
