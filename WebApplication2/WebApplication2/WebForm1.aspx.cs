using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ReportViewer1.Reset();
            DataTable dt = getData(DateTime.Parse(TextBox1.Text),DateTime.Parse(TextBox2.Text));
            ReportDataSource ds = new ReportDataSource("DataSet1", dt);
            ReportParameter[] rparam = new ReportParameter[]
            {
                new ReportParameter("fromDate",TextBox1.Text),
                new ReportParameter("todate",TextBox2.Text)
            };
            ReportViewer1.LocalReport.DataSources.Add(ds);
            ReportViewer1.LocalReport.ReportPath = "monthlystatement.rdlc";
            ReportViewer1.LocalReport.SetParameters(rparam);
            //ReportViewer1.ReportRefresh();
        }

        private DataTable getData(DateTime fdate,DateTime todate)
        {
            DataTable dt = new DataTable();
            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["MMS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {

                SqlCommand cmd = new SqlCommand("spMonthlyStatement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@sDate", SqlDbType.DateTime).Value = fdate;
                cmd.Parameters.Add("@endDate", SqlDbType.DateTime).Value = todate;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

            }
            return dt;
        }
    }
}