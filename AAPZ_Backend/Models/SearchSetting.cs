namespace AAPZ_Backend.Models
{
    public class SearchSetting
    {
        public int SearchSettingId { get; set; }
        public double Radius { get; set; }
        public double WantedCost { get; set; }

        public virtual Client Client { get; set; }
    }
}
