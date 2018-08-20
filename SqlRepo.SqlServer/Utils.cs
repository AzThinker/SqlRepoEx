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
    }
}
