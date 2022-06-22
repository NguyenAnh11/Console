using System;

namespace Console.ApplicationCore.Interfaces
{
    public interface IAuditable
    {
        DateTime CreateAtUtc { get; set; }
        DateTime LastModifiedAtUtc { get; set; }
    }
}
