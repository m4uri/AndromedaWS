using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleRESTServer.Models;
using MySql.Data;

namespace SimpleRESTServer
{
    public class PersonPersistence
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;

        public PersonPersistence()
        {
            string myConnectionString;
            myConnectionString = "server= localhost ;uid=root; pwd=Manager260198; database=employeedb;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
        }

        public Person getPerson(long ID)
        {
            Person p = new Person();
            MySql.Data.MySqlClient.MySqlDataReader mySQLReader = null;

            String sqlString = "SELECT * FROM tblpersonnel WHERE ID=" + ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);

            mySQLReader = cmd.ExecuteReader();
            if (mySQLReader.Read())
            {
                p.ID = mySQLReader.GetUInt32(0);
                p.FirstName = mySQLReader.GetString(1);
                p.LastName = mySQLReader.GetString(2);
                p.PayRate = mySQLReader.GetUInt32(3);
                p.StartDate = mySQLReader.GetDateTime(4);
                p.EndDate = mySQLReader.GetDateTime(5);
                return p;


            }

            else
            {
                return null;
            }



        }
        public long savePerson(Person personToSave)
        {
            string sqlString = "Insert into tblPersonnel(FirstName, LastName, PayRate, StartDate, EndDate) VALUES('" + personToSave.FirstName + "','" + personToSave.LastName + "'," + personToSave.PayRate + ",'" + personToSave.StartDate.ToString("yyyy-MM-dd HH:MM:ss") + "','" + personToSave.EndDate.ToString("yyyy-MM-dd HH:MM:ss") + "')";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
           cmd.ExecuteNonQuery();
            long id = cmd.LastInsertedId;
            return id;
        }

    }
}