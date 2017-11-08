namespace PTIT2012
{
    public static class ConvertSQL
    {
        public static string DaytoSQL(string ngaysql)
        {
            string thang = ngaysql.Substring(0, 2);
            string ngay = ngaysql.Substring(3, 2);
            string nam = ngaysql.Substring(6, 4);
            return ngay + "/" + thang + "/" + nam;
        }

        public static string DaytoSQLInsert(string ngaysql)
        {
            string ngay = ngaysql.Substring(0, 2);
            string thang = ngaysql.Substring(3, 2);
            string nam = ngaysql.Substring(6, 4);
            return nam + "/" + thang + "/" + ngay;
        }
    }
}