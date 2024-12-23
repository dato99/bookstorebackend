using BookStore.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace BookStore.Packages
{
    public interface IPKG_DS_BOOK_ORDER
    {
        public List<Order> get_ordes();
        public void add_user(User user);
        public void delete_order(Order order);
        public void complete_order(Order order);
    }

    public class PKG_DS_BOOK_ORDER : PKG_BASE
    {
        IConfiguration config;
        public PKG_DS_BOOK_ORDER(IConfiguration config) : base(config) { }

        public void add_user(User user)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;

            conn.Open();

            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_DS_BOOK_USER.add_user";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_first_name", OracleDbType.Varchar2).Value = user.Firstname;
            cmd.Parameters.Add("p_last_name", OracleDbType.Varchar2).Value = user.Lastname;

            cmd.ExecuteNonQuery();

            conn.Close();

        }

    }
}
