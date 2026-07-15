<%@ page title="" language="C#" masterpagefile="~/View/Site.Master" autoeventwireup="true" codebehind="Cadastrar.aspx.cs" inherits="Opus.View.Telas.Usuario.Cadastrar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <h2>Cadastre-se</h2>
    </center>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">Nome completo</label>
        <asp:TextBox ID="tbxNome" type="text" class="form-control" runat="server"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">E-mail</label>
        <asp:TextBox ID="tbxEmail" type="email" class="form-control" runat="server"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">Telefone</label>
        <asp:TextBox ID="tbxTelefone" class="form-control" runat="server" CssClass="form-control telefone"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">CPF</label>
        <asp:TextBox ID="tbxCPF" runat="server" CssClass="form-control cpf"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">Senha</label>
        <asp:TextBox ID="tbxSenha" type="password" class="form-control" runat="server"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">Foto de perfil</label>
        <asp:FileUpload ID="fuImagem" runat="server" CssClass="form-control" />
    </div>

    <center>
        <asp:Button runat="server" ID="btnEnviar" type="button" class="btn cor-roxa" OnClick="CadastrarUsuario" Text="Cadastrar" />
    </center>
    <br />

    <center><asp:LinkButton href="Entrar.aspx" class="link-neutro" runat="server">Já possuí uma conta? Clique aqui!</asp:LinkButton></center>

    <script src="../../JS/AjustarCaracteres.js"></script>

</asp:Content>
