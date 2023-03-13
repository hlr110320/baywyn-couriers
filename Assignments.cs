﻿using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace BayWynCouriersWinForm
{
    class Assignments
    {
        // Initiliasing the assignments page combo box strings
        private static string CbAUnassigned = "Get Unassigned Deliveries";
        private static string CbAUndelivered = "Get Undelivered Assignments";

        // Method to get the assignments combo box values and return as an array
        public static string[] fillCbA()
        {
            string[] _fillCbA = { CbAUnassigned, CbAUndelivered };
            return _fillCbA;
        }

        /// <summary>
        /// Gets all jobs which are unassigned. The couriers and logistics coordinator can then accept assignments.
        /// </summary>
        /// <returns>The dataset of unassigned deliveries</returns>
        public DataSet GetAssignments()
        {
            DataSet dsA = new DataSet();

            // Getting and opening the connection string
            string bwcCon = ConfigurationManager.ConnectionStrings["bwcCon"].ConnectionString;
            OleDbConnection con = new OleDbConnection(bwcCon);
            con.Open();

            // Setting the sql command and adapter
            OleDbCommand cmA = new OleDbCommand();
            cmA.Connection = con;
            cmA.CommandType = CommandType.Text;
            cmA.CommandText = "Select Deliveries.DeliveryID, Deliveries.Date, Deliveries.Time, Deliveries.CourierID, Deliveries.ClientID from Deliveries Inner Join Clients On Deliveries.ClientID = Clients.ClientID WHERE CourierID Is NULL";
            //Where CourierID Is Null";
            OleDbDataAdapter dA = new OleDbDataAdapter(cmA);

            dA.Fill(dsA);
            con.Close();

            return dsA;
        }

        //public DataSet AcceptAssignments()
        //{
        //    DataSet ds = new DataSet();

        //    // Getting and opening the connection string
        //    string bwcCon = ConfigurationManager.ConnectionStrings["BayWynCouriersWinForm.Properties.Settings.bwcCon"].ConnectionString;
        //    OleDbConnection con = new OleDbConnection(bwcCon);
        //    con.Open();

        //    // Setting the sql command and adapter
        //    OleDbCommand cmA = new OleDbCommand();
        //    cmA.Connection = con;
        //    cmA.CommandType = CommandType.Text;
        //    cmA.CommandText = "Select * from Deliveries where CourierID Is NULL";
        //    OleDbDataAdapter dA = new OleDbDataAdapter(cmA);

        //    dA.Fill(ds);
        //    con.Close();

        //    return ds;

        //}


        /// <summary>
        /// Gets all jobs which are undelivered. The couriers and logistics coordinator can then update the delivery date when the delivery has occurred.
        /// </summary>
        /// <returns>The dataset of undelivered jobs</returns>
        public DataSet GetUndelivered()
        {
            DataSet dsU = new DataSet();

            // Getting and opening the connection string
            string bwcCon = ConfigurationManager.ConnectionStrings["bwcCon"].ConnectionString;
            OleDbConnection con = new OleDbConnection(bwcCon);
            con.Open();

            // Setting the sql command and adapter
            OleDbCommand cmU = new OleDbCommand();
            cmU.Connection = con;
            cmU.CommandType = CommandType.Text;
            cmU.CommandText = "Select From Deliveries, Clients, Slots, Couriers WHERE Deliveries.Delivered Is NULL";
            OleDbDataAdapter daU = new OleDbDataAdapter(cmU);

            daU.Fill(dsU);
            con.Close();

            return dsU;
        }

    }
}