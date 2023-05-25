using System;

namespace SoftwareApplicationManager.UI.MVC.Models
{
    public static class FormatExtensions
    {
        public static string FormatNumber(this int num)
        {
            if (num >= 1000000000) {
                return (num / 1000000000D).ToString("0.#B");
            }
            if (num >= 100000000) {
                return (num / 1000000D).ToString("0.#M");
            }
            if (num >= 1000000) {
                return (num / 1000000D).ToString("0.##M");
            }
            if (num >= 100000) {
                return (num / 1000D).ToString("0.#k");
            }
            if (num >= 10000) {
                return (num / 1000D).ToString("0.##k");
            }

            return num.ToString("#,0");
        }

        public static string FormatDate(this DateTime date)
        {
            return date.ToString("dd MMMM yyyy");
        } // FormatDate.
    }
}