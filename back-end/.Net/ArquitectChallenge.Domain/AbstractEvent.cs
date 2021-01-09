using System;

namespace ArquitectChallenge.Domain
{
    public abstract class AbstractEvent
    {
        public Guid Id { get; set; }

        public EnumStatus Status { get; set; }
    }
}