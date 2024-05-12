
namespace movieAPI.Models
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
        [MaxLength(50,ErrorMessage ="the Max length is 50b ")]
        public string Name { get; set; }
    }
}
