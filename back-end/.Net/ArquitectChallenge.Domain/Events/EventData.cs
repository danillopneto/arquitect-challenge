using ArquitectChallenge.Domain.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
                FillIsNumeric();
                FillStatus();
            }
        }

        #region " DOMAIN RULES "

        private void FillIsNumeric()
        {
            IsNumeric = UtilExtensions.IsNumeric(_valor);
        }

        /// <summary>
        /// Fill status as the following:
        /// "Cada evento poderá ter o estado processado ou erro, caso o campo valor chegue vazio, o status do evento será erro caso contrário processado."
        /// </summary>
        private void FillStatus()
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

        #endregion " DOMAIN RULES "
    }
}