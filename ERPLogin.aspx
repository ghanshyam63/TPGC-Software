<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERPLogin.aspx.cs" Inherits="ERPLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="Login/css/style.css" rel="stylesheet" type="text/css" />
    <style>
        body
        {
            font-family: 'Lato' , Calibri, Arial, sans-serif;
            background: #1886b9 url(Login/images/showimage.png);
            font-weight: 300;
            font-size: 15px;
            color: #333;
            -webkit-font-smoothing: antialiased;
            overflow-y: scroll;
            overflow-x: hidden;
            background-repeat: no-repeat;
        }
    </style>
</head>
<body>
    <div class="container">
        <!-- Codrops top bar -->
        <!--/ Codrops top bar -->
        <section class="main">
				<form class="form-2" runat="server">
					<h2 style="color:#1886b9"><span class="log-in" ><div align="center" style="padding-bottom:10px;font-size:20px"  >Tilak P.G College Master Software</div> </span></h2>
                    <hr />
                   <asp:Panel ID="pnlLogin" runat="server" DefaultButton="btnLogin">

                    <table width="100%">
                    <tr>
                    <td>
					<p class="float">
						<label for="login"><i class="icon-user"></i>Username</label>
                          <asp:TextBox ID="txtUserName" runat="server"  name="login" placeholder="Username"></asp:TextBox>

				</p>
                    <div class="clr"></div>
                    
	  <p class="float">
						<label for="password"><i class="icon-lock"></i>Password</label>
						<asp:TextBox ID="txtPassWord" runat="server" TextMode="Password" placeholder="Password" class="showpassword"></asp:TextBox>
			<br />
			</p>
                  
				
                  </td>
                  <td>
                   <div align="left" >
                   
                <img src="Login/images/user-login_icon.png"  alt="login" width="100" height="100"></div>
                  </td>
</tr>
<tr>
<td colspan="2">
  <p class="clearfix"> 
                   
			<asp:LinkButton ID="btnLogin" runat="server" Text="Login" OnClick="btnlogin_Click" CssClass="log-twitter" />    
					
				  </p>

</td>
</tr>
</table>
</asp:Panel>
				</form>​​
               
			</section>
    </div>
   
</body>
</html>
