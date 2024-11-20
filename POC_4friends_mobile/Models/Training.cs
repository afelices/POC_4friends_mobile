
using SQLite;

namespace POC_4friends_mobile.Models
{
    public class Training
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int HR { get; set; }
        public string Datetime { get; set; } = default!;
    }
}
