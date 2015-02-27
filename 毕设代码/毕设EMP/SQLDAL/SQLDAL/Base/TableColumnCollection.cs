using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Specialized;

namespace MCSFramework.SQLDAL
{
    public class SqlParameterDictionary : Dictionary<string, SqlParameter>
    {
        public SqlParameterDictionary()
            : base()
        {
        }

        public SqlParameterDictionary(int capacity)
            : base(capacity)
        {
        }
    }
}
