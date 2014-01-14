<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TracerHub.Index" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tracer Hub</title>
</head>
<body>
    <h1>Tracer Hub: Realtime Remote Diagnostics</h1>
    <p>Learn more at <a href="https://github.com/clariuslabs/tracerhub">our GitHub repository</a>.</p>
    <p>Download the console client as a <a href="http://clarius.io/Tracer.7z">7zip (~230kb)</a> or <a href="http://clarius.io/Tracer.zip">Zip (~290kb)</a> archive.</p>
    <p>Hub URL: <%= ConfigurationManager.AppSettings["HubUrl"] %></p>
</body>
</html>
