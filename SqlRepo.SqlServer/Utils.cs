using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using SqlRepoEx.SqlServer;

namespace SqlRepoEx.SqlServer
{
    public static class Utils
    {
        public static string Alias(TableAlias alias)
        {
            if (alias == TableAlias.a9)
            {
                return string.Empty;
            }
            else
            {
                return alias.ToString();
            }

        }

        public static SqlParameterCollection GetParameterCollection(IDataReader dataReader)
        {
            var pcmdproperty = dataReader.GetType().GetRuntimeProperties()
                .Where(p => p.PropertyType.Name == "SqlCommand").FirstOrDefault();
            if (pcmdproperty == null)
            {
                return null;
            }

            var pcmvalue = pcmdproperty.GetValue(dataReader);

            var paramesproperty = pcmvalue.GetType().GetRuntimeProperties()
                  .Where(dk => dk.PropertyType.Name == "SqlParameterCollection").FirstOrDefault();
            if (paramesproperty == null)
            {
                return null;
            }

            var paramcols = paramesproperty.GetValue(pcmvalue);
            if (paramcols == null)
            {
                return null;
            }

            if (paramcols is SqlParameterCollection)
            {
                return paramcols as SqlParameterCollection;
            }
            return null;
        }

        public static IDataReader GetParameterCollection(this IDataReader dataReader, ParameterDefinition[] parameters)
        {
            if (parameters.Where(p => p.Direction > ParameterDirection.Input).Count() == 0)
            {
                return dataReader;
            }

            var cols = GetParameterCollection(dataReader);

            foreach (SqlParameter p in cols)
            {
                var par = parameters.Where(m => m.Name == p.ParameterName).FirstOrDefault();
                if (par != null)
                {
                    par.Value = p.Value;
                }
            }
            return dataReader;
        }

    }
}
