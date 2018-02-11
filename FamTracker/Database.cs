using System.Data;

public abstract class Database
{
    public string connectionString;

    #region Abstract Functions

    public abstract IDbConnection CreateConnection();
    public abstract IDbCommand CreateCommand();
    public abstract IDbConnection CreateOpenConnection();
    public abstract IDbCommand CreateCommand(string CommandText, IDbConnection connection);
    public abstract IDbCommand CreateStoredProcCommand(string procName, IDbConnection connection);
    public abstract IDataParameter CreateParameter(string parameterName, object parameterValue);

    #endregion
}
