<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;

public class Handler : IHttpHandler
{

    string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["PegaConnection"].ToString();
    public void ProcessRequest(HttpContext context)
    {
        try
        {

            string imageid = context.Request.QueryString["ImID"];


            SqlConnection connection = new SqlConnection(strcon);
            connection.Open();
            SqlCommand command = new SqlCommand("select PImage from Inv_Product_Image where ProductId='" + imageid + "'", connection);
            SqlDataReader dr = command.ExecuteReader();
            dr.Read();
            context.Response.BinaryWrite((Byte[])dr[0]);
            connection.Close();
            context.Response.End();

        }
        catch
        {

        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}