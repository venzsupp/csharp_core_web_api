using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace csharp_core_web_api.Abstracts;
public interface IDbConnector
{
    public string ConnectionString { get; set; }

    // public Task<SqlConnection> connectDB(string ConnectionString);
    public SqlConnection connectDB(string ConnectionString);
}