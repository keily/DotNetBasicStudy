<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="BasicLearn.Javascript.Part1.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <p>ItemValue:<%=this.ItemValue%></p>
    <p>ItemValue:<%=this.ParamsValue%></p>
    <form id="form1" action="<%=Request.RawUrl %>" method="post">
    <div>
        <input type="text" name="name" value="123" />
        <input type="submit" value="提交" />
    </div>
    </form>
</body>
</html>
