using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class InputModel
    {
        [Required]
        public string Expression { get; set; }
    }

    public class MathResponseModel
    {
        public string Expression { get; set; }
        public double Result { get; set; }
        public string DescriptionMessage { get; set; }
    }
}
