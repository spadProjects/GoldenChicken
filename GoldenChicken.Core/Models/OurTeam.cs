using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoldenChicken.Core.Models
{
    public class OurTeam : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Display(Name = "نقش")]
        public string Role { get; set; }
        [Display(Name = "تصویر")]
        public string Image { get; set; }
        [Display(Name = "لینک فیسبوک")]
        public string Facebook { get; set; }
        [Display(Name = "لینک تویتر")]
        public string Twitter { get; set; }
        [Display(Name = "لینک گوگل پلاس")]
        public string Google { get; set; }
        [Display(Name = "لینک لینکدین")]
        public string Linkedin { get; set; }
        public string InsertUser { get; set; }
        public DateTime? InsertDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
