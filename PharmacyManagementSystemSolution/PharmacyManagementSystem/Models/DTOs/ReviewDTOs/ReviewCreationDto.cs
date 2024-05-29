namespace PharmacyManagementSystem.Models.DTOs.ReviewDTOs
{
    public class ReviewCreationDto
    {
        public int ProductID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}