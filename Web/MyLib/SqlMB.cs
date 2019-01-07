namespace Web.MyLib
{
    public class SqlMB
    {
        private int PagingIndex = 0;
        private int PagingCount = 0;

        private string PageList()
        {
            string sql = ""
                + " declare @bi int "
                + " declare @ei int "
                + " set @bi = (" + PagingIndex + " - 1) * " + PagingCount + " + 1 "
                + " set @ei = " + PagingIndex + " * " + PagingCount + " "

                + " declare @count int "
                + " select @count = count(1) "
                + " from table1 "
                    + " left join table2 on "
                + " where 1=1 "
                    + " and "

                + " select @count c, * "
                + " from ( "
                    + " select "
                        + " ROW_NUMBER() over (order by (select 1)) i "
                        + ", "
                    + " from table1 "
                        + " left join table2 on "
                    + " where 1=1 "
                        + " and "
                + " ) t "
                + " where @bi <= i and i <= @ei ";

            return sql;
        }
    }
}