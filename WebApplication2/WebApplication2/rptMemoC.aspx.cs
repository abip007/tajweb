using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class rptMemoC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int rid = Convert.ToInt32(Session["rid"]);
                ReportViewer1.Reset();
                DataTable dt = getData(rid);
                ReportDataSource ds = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.DataSources.Add(ds);
                ReportViewer1.LocalReport.ReportPath = "report/rptMemo.rdlc";

            }

        }
        private DataTable getData(int rid)
        {
            DataTable dt = new DataTable();
            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["MMS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {

                SqlCommand cmd = new SqlCommand("[spMemo]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@rid", SqlDbType.Int).Value = rid;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

            }
            return dt;
        }
    }
}