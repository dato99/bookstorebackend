using BookStore.Models;
using Microsoft.AspNetCore.Identity.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace BookStore.Packages
{

    public interface IPKG_DS_BOOKS
    {
        public List<Book> get_books();
        public void add_book(Book book);
        public void delete_book(Book book);
        public void update_book(Book book);
 
    }
    public class PKG_DS_BOOKS : PKG_BASE, IPKG_DS_BOOKS
    {
        IConfiguration config;
        public PKG_DS_BOOKS(IConfiguration config) : base(config) { }

        public void add_book(Book book)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;

            conn.Open();

            OracleCommand cmd = conn.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_ds_books.add_book";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_name", OracleDbType.Varchar2).Value = book.Name;
            cmd.Parameters.Add("p_quantity", OracleDbType.Int32).Value = book.Quantity;
            cmd.Parameters.Add("p_author", OracleDbType.Varchar2).Value = book.Author;
            cmd.Parameters.Add("p_price", OracleDbType.Decimal).Value = book.Price;

            cmd.ExecuteNonQuery();

            conn.Close();

        }

        public List<Book> get_books()
        {
            List<Book> books = new List<Book>();
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_ds_books.get_books";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = ParameterDirection.Output;



            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Book book = new Book();
                book.Id = int.Parse(reader["id"].ToString());
                book.Name = reader["name"].ToString();
                book.Quantity = int.Parse(reader["quantity"].ToString());
                book.Author = reader["author"].ToString();
                book.Price = decimal.Parse(reader["price"].ToString());

                books.Add(book);
            }
            conn.Close();

            return books;
        }

        public void delete_book(Book book)
        {
            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = ConnStr;
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "olerning.PKG_ds_books.delete_book";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = book.Id;

            cmd.ExecuteNonQuery();
            conn.Close();

        }

        public void update_book(Book book)
        {
            using (OracleConnection conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (OracleCommand cmd = new OracleCommand("olerning.PKG_ds_books.update_book", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add all required parameters
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = book.Id;
                    cmd.Parameters.Add("p_name", OracleDbType.Varchar2).Value = book.Name;
                    cmd.Parameters.Add("p_quantity", OracleDbType.Int32).Value = book.Quantity;
                    cmd.Parameters.Add("p_author", OracleDbType.Varchar2).Value = book.Author;
                    cmd.Parameters.Add("p_price", OracleDbType.Decimal).Value = book.Price;
                    

                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
