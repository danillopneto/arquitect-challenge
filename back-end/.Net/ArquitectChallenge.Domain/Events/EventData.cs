using ArquitectChallenge.Domain.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArquitectChallenge.Domain.Events
{
    [Table("EventData")]
    public class EventData : BaseObject
    {
        [Column("IsNumeric")]
        public bool IsNumeric { get; private set; }

        [Column("Tag")]
        public string Tag { get; set; }

        [Range(1, long.MaxValue)]
        [Required]
        [Column("Timestamp")]
        public long Timestamp { get; set; }

        [Column("Valor")]
        public string Valor { get; set; }

        #region " DOMAIN RULES "

        public void PrepareToSave()
        {
            FillIsNumeric();
            FillStatus();
        }

        private void FillIsNumeric()
        {
            IsNumeric = UtilExtensions.IsNumeric(Valor);
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