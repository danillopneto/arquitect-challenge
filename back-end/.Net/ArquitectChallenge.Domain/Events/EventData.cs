using System.ComponentModel.DataAnnotations;

namespace ArquitectChallenge.Domain.Events
{
    public class EventData : BaseObject
    {
        [Range(1, double.MaxValue)]
        [Required]
        public double TimeStamp { get; set; }

        [RegularExpression(".*\\..*")]
        [Required]
        public string Tag { get; set; }

        public string Valor { get; set; }

        public void FillStatus()
        {
            if (string.IsNullOrWhiteSpace(Valor))
            {
                Status = EnumStatus.Error;
            }
            else
            {
                Status = EnumStatus.Done;
            }
        }
    }
}