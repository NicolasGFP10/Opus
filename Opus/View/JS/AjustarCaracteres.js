document.addEventListener("DOMContentLoaded", function () {

    // TELEFONE
    document.querySelectorAll(".telefone").forEach(function (campo) {

        campo.addEventListener("input", function () {

            let valor = campo.value.replace(/\D/g, "");

            valor = valor.substring(0, 11);

            if (valor.length > 10)
                campo.value = valor.replace(/^(\d{2})(\d{5})(\d{4}).*/, "($1) $2-$3");

            else if (valor.length > 6)
                campo.value = valor.replace(/^(\d{2})(\d{4})(\d+).*/, "($1) $2-$3");

            else if (valor.length > 2)
                campo.value = valor.replace(/^(\d{2})(\d+)/, "($1) $2");

            else
                campo.value = valor.replace(/^(\d*)/, "($1");
        });

    });

    // CPF
    document.querySelectorAll(".cpf").forEach(function (campo) {

        campo.addEventListener("input", function () {

            let valor = campo.value.replace(/\D/g, "");

            valor = valor.substring(0, 11);

            campo.value = valor.replace(
                /^(\d{3})(\d{3})(\d{3})(\d{2}).*/,
                "$1.$2.$3-$4"
            );

        });

    });

    // CNPJ
    document.querySelectorAll(".cnpj").forEach(function (campo) {

        campo.addEventListener("input", function () {

            let valor = campo.value.replace(/\D/g, "");

            // Limita a 14 dígitos
            valor = valor.substring(0, 14);

            if (valor.length <= 2)
                campo.value = valor;

            else if (valor.length <= 5)
                campo.value = valor.replace(/^(\d{2})(\d+)/, "$1.$2");

            else if (valor.length <= 8)
                campo.value = valor.replace(/^(\d{2})(\d{3})(\d+)/, "$1.$2.$3");

            else if (valor.length <= 12)
                campo.value = valor.replace(/^(\d{2})(\d{3})(\d{3})(\d+)/, "$1.$2.$3/$4");

            else
                campo.value = valor.replace(
                    /^(\d{2})(\d{3})(\d{3})(\d{4})(\d{2}).*/,
                    "$1.$2.$3/$4-$5"
                );

        });

    });

});