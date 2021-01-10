using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace ArquitectChallenge.Domain.Events
{
    [Table("EventData")]
    public class EventData : BaseObject
    {
        private string _valor;

        [Column("IsNumeric")]
        public bool IsNumeric { get; private set; }

        [Column("Tag")]
        public string Tag { get; set; }

        [Range(1, double.MaxValue)]
        [Required]
        [Column("TimeStamp")]
        public double TimeStamp { get; set; }

        [Column("Valor")]
        public string Valor
        {
            get
            {
                return _valor;
            }
            set
            {
                _valor = value;
                IsNumeric = Regex.IsMatch(_valor, "^[0-9]+$");
            }
        }

        /// <summary>
        /// Fill status as the following:
        /// "Cada evento poderá ter o estado processado ou erro, caso o campo valor chegue vazio, o status do evento será erro caso contrário processado."
        /// </summary>
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