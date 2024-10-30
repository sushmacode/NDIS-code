using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDIS.Entities
{
    public class paggingEntity
    {
        public int pgsize { get; set; }
        public int pgindex { get; set; }
        public string str { get; set; }
        public int sortby { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int Committee { get; set; }
    }
    public class Settings
    {
        public static string SetOrderStatus(int stat)
        {
            string OrderStatus = "Processing";
            if (stat != null)
            {
                switch (stat)
                {
                    case 0: OrderStatus = "Processing"; break;
                    case 1: OrderStatus = "Completed"; break;
                    case -1: OrderStatus = "Cancelled"; break;
                }
            }
            return OrderStatus;
        }

        public static string SetStatus(int stat)
        {
            string OrderStatus = "";
            if (stat != 3)
            {
                switch (stat)
                {
                    case 0: OrderStatus = "Inactive"; break;
                    case 1: OrderStatus = "Active"; break;
                    //case -1: OrderStatus = "Cancelled"; break;
                }
            }
            return OrderStatus;
        }

        public static string SetPriceFormat(string price)
        {
            if (price != null && price != "")
            {
                return "$" + string.Format("{0:0.00}", Convert.ToDouble(price));
            }
            else { return "$" + "0.00"; }
        }
        public static string SetPriceFormatNoCurrency(string price)
        {
            if (price != null && price != "")
            {
                return string.Format("{0:0.00}", Convert.ToDouble(price));
            }
            else { return "0.00"; }
        }

        public static string SetDateFormat(DateTime? dt)
        {
            if (dt != null)
            {
                return String.Format("{0:d/MMM/yyyy}", Convert.ToDateTime(dt));
            }
            else { return ""; }
        }
    }
}
