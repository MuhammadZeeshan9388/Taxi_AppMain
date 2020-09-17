using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taxi_AppMain.Classes
{
    class ColChooser
    {
        string query = "";
        public DataTable columne { get; set; }
        public string Colname { get; set; }
        public bool IsVisable { get; set; }
        public double colWidth { get; set; }
        public double ColOrder { get; set; }

        

        private DataTable dataTable = null;
        private BindingSource bindingSource = null;
        DataTable dt = null;
        public SqlConnection con = new SqlConnection("Data Source=88.208.220.41;Initial Catalog=Taxi;User ID=tcloud321;Password=tcloud321!;Trusted_Connection=False;");
        SqlCommand cmd;
        SqlDataAdapter adpter;
        SqlDataReader dr;
        public bool connect(bool b)
        {

            switch (b)
            {
                case true:
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Open();
                    break;
                case false:
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                    break;
            }





            return b;
        }
        public string getValue(string sel)
        {

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //--------
               cmd = new SqlCommand(sel.Trim(), con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {

                        query = dr[0].ToString();
                    }
                }
                else
                {
                    query = "0";
                }


                //--------
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                query = "0 ";
            }
            return query;
        }
        public void viewall(string sel, DataGridView dgv)
        {
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

               adpter = new SqlDataAdapter(sel, con);


                DataSet dss = new DataSet();

               adpter.Fill(dss);
                //  dgv.ReadOnly = true;


                dataTable = new DataTable();
               adpter.Fill(dataTable);
                bindingSource = new BindingSource();
                bindingSource.DataSource = dataTable;

                dgv.DataSource = bindingSource;

                // if you want to hide Identity column
                dgv.Columns[0].Visible = false;
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }




            }

            catch (Exception ec)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                MessageBox.Show(ec.Message);

            }
        }

        public void Dt_Col(DataTable dt_ColChooser)
        {
            dt_ColChooser.Columns.Add("ColumnName");
            dt_ColChooser.Columns.Add("ColumnWidth");
            dt_ColChooser.Columns.Add("IsVisable");
            dt_ColChooser.Columns.Add("ColOrder");
              

            //
        }
        public void Dt_AddCol(DataTable dt_ColChooser, Classes.ColChooser cc)
        {
            
                
                DataRow dr = dt_ColChooser.NewRow();
                dr["ColumnName"] = cc.Colname;
                dr["ColumnWidth"] = cc.colWidth;
                dr["IsVisable"] = cc.IsVisable;
            dr["ColOrder"] = cc.ColOrder;

                dt_ColChooser.Rows.Add(dr);
                dt_ColChooser.AcceptChanges();
            
        }
        public void Dt_RemoveCol(DataTable dt_ColChooser, Classes.ColChooser cc)
        {
            // dt_ColChooser.Columns.Add("ColumnName");
            //  dt_ColChooser.Columns.Add("ColumnWidth");

            DataRow dr = dt_ColChooser.NewRow();

            dr["ColumnName"] = cc.Colname;
          //  dr["ColumnWidth"] = cc.colWidth;

            dt_ColChooser.Rows.Remove(dr);


            dt_ColChooser.AcceptChanges();



             dr = dt_ColChooser.NewRow();

            dr["ColumnName"] = cc.Colname;
            dr["ColumnWidth"] = cc.colWidth;
            dr["IsVisable"] = true;
            dt_ColChooser.Rows.Add(dr);

        }
        public bool Action(Classes.ColChooser cc)
        {
            bool f = false;
            try
            {
                



                    DataTable dt = new DataTable();
                    cmd = new SqlCommand("sp_ColumnChooser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                
                    cmd.Parameters.AddWithValue("@ColChooser", cc.columne);
                    cmd.Parameters.AddWithValue("@ColChoosername",null);

                cmd.Parameters.AddWithValue("@query", 1);


                adpter = new SqlDataAdapter(cmd);
                            adpter.Fill(dt);

                            f = true;
                          


              
            }
            catch (Exception)
            {
                f = false;
                
            }
            finally
            {
               
            }
            return f;
        }

        public bool en_save(string sel)
        {
            bool sav = false;
            try
            {
                if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            //--------
           cmd = new SqlCommand(sel, con);
            if (cmd.ExecuteNonQuery() > 0)
            {
                sav = true;
            }
            else
            {
                sav = false;

            }


            //--------
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                sav = false;
                MessageBox.Show(ex.Message, "#Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return sav;
        }
        public bool IsAvailable(string sel)
        {
            bool ischck = false;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                //--------
               cmd = new SqlCommand(sel, con);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {


                        ischck = true;
                    }
                }
                else
                {

                    ischck = false;
                }


                //--------
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                ischck = false;
            }
            return ischck;
        }

        public DataTable dT_get(string sel)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
               adpter = new SqlDataAdapter(sel.Trim(), con);
                //adptr.SelectCommand.CommandType = CommandType.StoredProcedure;
               adpter.Fill(dt);

            }
            catch (Exception)
            {
                // alert(ex.Message, "Error: " + sel, SystemIcons.Error);
            }
            return dt;
            // return dt;
        }

    }
}
