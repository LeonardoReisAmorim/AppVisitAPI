namespace Application.DTOs.PlaceDTO
{
    public class ReadPlaceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FileVRId { get; set; }
        public int CityId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string City { get; set; }
        public string FileName { get; set; }
        public string UsageInstructionsVR { get; set; }
        public int TypePlaceId { get; set; }
    }
}
