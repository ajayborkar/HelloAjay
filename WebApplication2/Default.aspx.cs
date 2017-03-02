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
    public partial class WebForm1 : System.Web.UI.Page
    {

        private SqlConnection conn = new SqlConnection("Data Source=ajay-pc;Integrated Security=true;Initial Catalog=test1");
        int updateID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvbind();
            }

        }

        protected void gvbind()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select * from tbl1", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            conn.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                GridView1.DataSource = ds;
                GridView1.DataBind();
                int columncount = GridView1.Rows[0].Cells.Count;
                GridView1.Rows[0].Cells.Clear();
                GridView1.Rows[0].Cells.Add(new TableCell());
                GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                GridView1.Rows[0].Cells[0].Text = "No Records Found";
            }

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            Label lbldeleteid = (Label)row.FindControl("lblID");
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete FROM tbl1 where id='" + Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString()) + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            gvbind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GridView1.EditIndex = e.NewEditIndex;
            gvbind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            gvbind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            gvbind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int userid = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            Label lblID = (Label)row.FindControl("lblID");
            //TextBox txtname=(TextBox)gr.cell[].control[];
            TextBox textName = (TextBox)row.FindControl("txtName");
            TextBox textadd = (TextBox)row.FindControl("txtAddress");
            TextBox textc = (TextBox)row.FindControl("txtCountry");
            //TextBox textadd = (TextBox)row.FindControl("txtadd");
            //TextBox textc = (TextBox)row.FindControl("txtc");
            GridView1.EditIndex = -1;
            conn.Open();
            //SqlCommand cmd = new SqlCommand("SELECT * FROM detail", conn);
            SqlCommand cmd = new SqlCommand("update tbl1 set name='" + textName.Text + "',address='" + textadd.Text + "',country='" + textc.Text + "'where id='" + userid + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            gvbind();
            //GridView1.DataBind();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int userid = Convert.ToInt32(GridView1.DataKeys[e.].Value.ToString());
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int userid = Convert.ToInt32(GridView1.DataKeys[e.NewSelectedIndex ].Value.ToString());
            GridViewRow row = (GridViewRow)GridView1.Rows[e.NewSelectedIndex];

            Label textName = (Label )row.FindControl("lblName");
            Label textadd = (Label)row.FindControl("lblAddress");
            Label textc = (Label)row.FindControl("lblCountry");

            TextBox11.Text = textName.Text;
            updateID = userid;
            TextBox22.Text = textadd.Text;
            TextBox33.Text = textc.Text;

            TextBox11.Focus();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            //SqlCommand cmd = new SqlCommand("SELECT * FROM detail", conn);
            SqlCommand cmd = new SqlCommand("update tbl1 set name='" + TextBox11.Text + "',address='" + TextBox22.Text + "',country='"
                + TextBox33.Text + "'where id='" + updateID + "'", conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            TextBox11.Text = "";
            updateID = 0;
            TextBox22.Text = "";
            TextBox33.Text = "";

            GridView1.SelectedIndex = -1;

            gvbind();
        }
    }
}