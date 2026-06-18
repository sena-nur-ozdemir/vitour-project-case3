namespace Case3Vitour.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CategoryCollectionName { get; set; } = null!;
        public string TourCollectionName { get; set; } = null!;
        public string TourPlanCollectionName { get; set; } = null!;
        public string GalleryCollectionName { get; set; } = null!;
        public string ReviewCollectionName { get; set; } = null!;
        public string ReservationCollectionName { get; set; } = null!;
    }
}