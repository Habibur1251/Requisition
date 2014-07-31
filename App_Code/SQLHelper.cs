using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Summary description for SQLHelper
/// </summary>
public class SQLHelper
{
    SqlConnection _connection = null;
    SqlCommand _cmd = null;
    SqlTransaction transaction = null;

    public static string ConnectionString
    {
        get
        {
            return WebConfigurationManager.ConnectionStrings["dtcon"].ConnectionString;
        }
    }

    public static string ConnectionStringWeb
    {
        get
        {
            return WebConfigurationManager.ConnectionStrings["dtweb"].ConnectionString;
        }
    }

    public SQLHelper()
    {
        string connStr = ConnectionString; // ConfigurationManager.ConnectionStrings["dtcon"].ConnectionString;
        _connection = new SqlConnection(connStr);
        _connection.Open();
    }

    public void CloseConnection()
    {
        if (_connection != null)
        {
            _connection.Close();
            _connection = null;
        }
    }

    public int ExecuteQuery(string psql) //insert,update, delete
    {
        int affectedrows = 0;
        _cmd = new SqlCommand();
        _cmd.Connection = _connection;
        _cmd.CommandType = CommandType.Text;
        _cmd.CommandText = psql;
        //IDbTransaction trans = _connection.BeginTransaction(); 
        SqlTransaction trans = _connection.BeginTransaction();
        try
        {
            affectedrows = _cmd.ExecuteNonQuery();
            trans.Commit();
            return affectedrows;
        }

        catch (Exception)
        {
            trans.Rollback();
            return 0;
        }
    }

    public SqlDataReader FetchQuery(string psql)//select
    {
        try
        {
            _cmd = new SqlCommand();
            _cmd.Connection = _connection;
            _cmd.CommandType = CommandType.Text;
            _cmd.CommandText = psql;
            SqlDataReader sreader = _cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return sreader;
        }
        catch (Exception err)
        {
            return null;
        }
    }

    public SqlCommand CreateCommand(string sqlText, bool procedure)
    {
        _cmd = _connection.CreateCommand();
        _cmd.CommandText = sqlText;
        if (procedure)
            _cmd.CommandType = CommandType.StoredProcedure;
        return _cmd;
    }
}