<%@ page title="" language="C#" masterpagefile="~/View/Site.Master" autoeventwireup="true" codebehind="Entrar.aspx.cs" inherits="Opus.View.Telas.Usuario.Entrar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <h2>Entrar</h2>
    </center>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">E-mail</label>
        <asp:TextBox ID="tbxEmail" type="email" class="form-control" runat="server"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">Senha</label>
        <asp:TextBox ID="tbxSenha" type="password" class="form-control" runat="server"></asp:TextBox>
    </div>

    <center><asp:Button runat="server" ID="btnEnviar" type="button" OnClick="btnEnviar_Click" class="btn cor-roxa" Text="Entrar"/></center><br />

    <center><asp:LinkButton href="Cadastrar.aspx" class="link-neutro" runat="server">Não possuí uma conta? Clique aqui!</asp:LinkButton></center>

</asp:Content>
