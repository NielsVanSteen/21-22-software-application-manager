namespace SoftwareApplicationManager.BL.Domain
{
    public struct AppType
    {
        public static readonly string[] Description = {
            "Free",
            "Paid",
            "Both Free and Paid",
        };

        public enum Type : byte
        {
            Free = 0,
            Paid = 1,
            Both = 2
        }
    }
}