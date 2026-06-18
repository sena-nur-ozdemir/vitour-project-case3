namespace Case3Vitour.Settings
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CategoryCollectionName { get; set; }
        string TourCollectionName { get; set; }
        string TourPlanCollectionName { get; set; }
        string GalleryCollectionName { get; set; }
        string ReviewCollectionName { get; set; }
        string ReservationCollectionName { get; set; }
    }
}