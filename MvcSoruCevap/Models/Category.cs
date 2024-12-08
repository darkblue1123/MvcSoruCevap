namespace MvcSoruCevap.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // Navigation Properties
        public virtual ICollection<Question> Questions { get; set; }
    }

}
