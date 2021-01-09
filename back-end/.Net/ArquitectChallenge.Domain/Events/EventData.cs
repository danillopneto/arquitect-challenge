using System.ComponentModel.DataAnnotations;

namespace ArquitectChallenge.Domain.Events
{
    public class EventData : BaseObject
    {
        [RegularExpression(".*\\..*\\..*")]
        [Required]
        public string Tag { get; set; }

        [Range(1, double.MaxValue)]
        [Required]
        public double TimeStamp { get; set; }

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