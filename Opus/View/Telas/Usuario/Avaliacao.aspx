<%@ page title="Avaliações - Opus" language="C#" masterpagefile="~/View/Site.Master" autoeventwireup="true" codebehind="Avaliacao.aspx.cs" inherits="Opus.View.Telas.Usuario.Avaliacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <h2>Avaliação</h2>
    </center>

    <div class="d-grid gap-2 d-md-flex justify-content-md-end" style="padding-top: 20px; padding-right: 30px;">

        <asp:Label ID="lblTexto" runat="server" Text="Gerar código de Avaliação" Visible="false"></asp:Label>

        <asp:Label ID="lblCodigo" runat="server" Text="" CssClass="fonte-vermelha" Visible="false"></asp:Label>

        <asp:Button ID="btnGerar" runat="server" Text="Gerar" CssClass="btn cor-roxa btn-dark" Visible="false" OnClick="btnGerar_Click" />

    </div>

    <label class="form-label">
        Código de Avaliação
    </label>

    <asp:TextBox
        ID="tbxCodigoAvaliacao"
        runat="server"
        MaxLength="6"
        type="number"
        CssClass="form-control">
    </asp:TextBox>

    <asp:Label
        ID="lblErroCodigo"
        runat="server"
        CssClass="text-danger"
        Visible="false"></asp:Label>
    <br />

    <center>
        <asp:Button
            ID="btnAvaliar"
            runat="server"
            Text="Realizar avaliação"
            CssClass="btn cor-roxa btn-dark"
            OnClick="btnAvaliar_Click" />
    </center>

    <!-- Modal -->
    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Avalie o Serviço</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <label class="form-label">
                        Título da avaliação
                    </label>

                    <div class="mb-3">
                        <asp:TextBox ID="tbxTitulo" class="form-control" runat="server" CssClass="form-control telefone"></asp:TextBox>
                    </div>

                    <label class="form-label">
                        Descreva o serviço
                    </label>

                    <div class="mb-3">
                        <asp:TextBox ID="tbxDescricao" class="form-control" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>

                    <label class="form-label">
                        Nota de 0 a 5 para o serviço
                    </label>
                    <br />
                    <center>
                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rb1" runat="server"
                                GroupName="Avaliacao" />
                            <label class="form-check-label" for="rb1">1</label>
                        </div>

                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rb2" runat="server"
                                GroupName="Avaliacao" />
                            <label class="form-check-label" for="rb2">2</label>
                        </div>

                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rb3" runat="server"
                                GroupName="Avaliacao" />
                            <label class="form-check-label" for="rb3">3</label>
                        </div>

                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rb4" runat="server"
                                GroupName="Avaliacao" />
                            <label class="form-check-label" for="rb4">4</label>
                        </div>

                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rb5" runat="server"
                                GroupName="Avaliacao" />
                            <label class="form-check-label" for="rb5">5</label>
                        </div>
                    </center>
                    <br />
                    <label class="form-label">
                        Fotos do serviço (até 5 fotos)
                    </label>

                    <asp:FileUpload
                        ID="fuFotos"
                        runat="server"
                        AllowMultiple="true"
                        CssClass="form-control"
                        accept=".jpg,.jpeg,.png,.webp" />

                </div>
                <div class="modal-footer">

                    <button type="button" class="btn btn-dark" data-bs-dismiss="modal" style="width: 200px;">Fechar</button>

                    <asp:Button runat="server" ID="btnEnviar" type="button" class="btn btn-dark cor-roxa" Text="Enviar avaliação" Width="200px" />
                    </center>

                    <br />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
