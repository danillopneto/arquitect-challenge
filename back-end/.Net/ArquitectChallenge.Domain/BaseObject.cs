using System;

namespace ArquitectChallenge.Domain
{
    public abstract class BaseObject
    {
        public Guid Id { get; set; }

        public EnumStatus Status { get; set; }
    }
}