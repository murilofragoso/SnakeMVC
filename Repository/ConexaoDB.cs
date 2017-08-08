using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Repository
{
    class ConexaoDB
    {
        private string _connectionString => ConfigurationManager.ConnectionStrings["ConexaoBanco"].ToString();
        private SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }


        public void Connect()
        {
            Connection = new SqlConnection(_connectionString);

            if (Connection.State == ConnectionState.Broken)
            {
                Connection.Close();
                Connection.Open();
            }
            if (Connection.State != ConnectionState.Open)
            {
                Connection.Open();
            }
        }

        public void ExecutarProcedure(string nomeProcedure)
        {
            Command = new SqlCommand(nomeProcedure, Connection)
            {
                CommandType = CommandType.StoredProcedure
            };
        }

        public void AddParametro(string nomeParametro, object valor)
        {
            Command.Parameters.Add(new SqlParameter(nomeParametro, valor ?? DBNull.Value));
        }

        public void Executar()
        {
            Command.ExecuteNonQuery();
        }

        public SqlDataReader ExecutarReader()
        {
            return Command.ExecuteReader();
        }

        public void Disconect()
        {
            Connection.Close();
        }
    }
}
