// public class DataAccessException : [System.Serializable]
using System;
namespace csharp_core_web_api.Abstracts.Exceptions;
public class DataAccessException : Exception
{
    public DataAccessException() { }
    public DataAccessException(string message) : base(message) { }
    public DataAccessException(string message, System.Exception inner) : base(message, inner) { }
    protected DataAccessException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}