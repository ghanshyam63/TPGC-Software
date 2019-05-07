using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ERPLogin : System.Web.UI.Page
{
    UserMaster ObjUserMaster = new UserMaster();
    SystemParameter ObjSysParam = new SystemParameter();
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
    protected void btnlogin_Click(object sender, EventArgs e)
    {
        DataTable dtUser = ObjUserMaster.GetUserMasterByUserIdPass(txtUserName.Text.Trim().ToString(), txtPassWord.Text.Trim().ToString());
        if (dtUser.Rows.Count != 0)
        {

            Session["UserId"] = txtUserName.Text.Trim();
            Session["CompId"] = dtUser.Rows[0]["Company_Id"].ToString();
            Session["RoleId"] = dtUser.Rows[0]["Role_Id"].ToString();
            Session["HeaderText"] = "ERP";
           
            ObjSysParam.SetPageSize();
            DataTable dtMessage1 = ObjSysParam.GetArabicMessage();
            Session["MessageDt"] = dtMessage1;
            Response.Redirect("~/MasterSetup/Home.aspx");
        }
        else
        {
            DisplayMessage("Invaild UserName and Password");
            txtUserName.Text="";
            txtUserName.Focus();

        
        }
       
    }
    public void DisplayMessage(string str)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('" + str + "');", true);
    }
}
